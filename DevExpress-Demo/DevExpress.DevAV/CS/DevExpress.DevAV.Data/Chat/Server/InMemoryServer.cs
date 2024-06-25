#region Copyright (c) 2000-2022 Developer Express Inc.
/*
{*******************************************************************}
{                                                                   }
{       Developer Express .NET Component Library                    }
{                                                                   }
{                                                                   }
{       Copyright (c) 2000-2022 Developer Express Inc.              }
{       ALL RIGHTS RESERVED                                         }
{                                                                   }
{   The entire contents of this file is protected by U.S. and       }
{   International Copyright Laws. Unauthorized reproduction,        }
{   reverse-engineering, and distribution of all or any portion of  }
{   the code contained in this file is strictly prohibited and may  }
{   result in severe civil and criminal penalties and will be       }
{   prosecuted to the maximum extent possible under the law.        }
{                                                                   }
{   RESTRICTIONS                                                    }
{                                                                   }
{   THIS SOURCE CODE AND ALL RESULTING INTERMEDIATE FILES           }
{   ARE CONFIDENTIAL AND PROPRIETARY TRADE                          }
{   SECRETS OF DEVELOPER EXPRESS INC. THE REGISTERED DEVELOPER IS   }
{   LICENSED TO DISTRIBUTE THE PRODUCT AND ALL ACCOMPANYING .NET    }
{   CONTROLS AS PART OF AN EXECUTABLE PROGRAM ONLY.                 }
{                                                                   }
{   THE SOURCE CODE CONTAINED WITHIN THIS FILE AND ALL RELATED      }
{   FILES OR ANY PORTION OF ITS CONTENTS SHALL AT NO TIME BE        }
{   COPIED, TRANSFERRED, SOLD, DISTRIBUTED, OR OTHERWISE MADE       }
{   AVAILABLE TO OTHER INDIVIDUALS WITHOUT EXPRESS WRITTEN CONSENT  }
{   AND PERMISSION FROM DEVELOPER EXPRESS INC.                      }
{                                                                   }
{   CONSULT THE END USER LICENSE AGREEMENT FOR INFORMATION ON       }
{   ADDITIONAL RESTRICTIONS.                                        }
{                                                                   }
{*******************************************************************}
*/
#endregion Copyright (c) 2000-2022 Developer Express Inc.

namespace DevExpress.DevAV.Chat {
	using System;
	using System.Collections.Concurrent;
	using System.Collections.Generic;
	using System.Threading;
	using System.Threading.Tasks;
	using DevExpress.DevAV.Chat.Commands;
	using DevExpress.DevAV.Chat.Events;
	public sealed partial class DevAVEmpployeesInMemoryServer : IMessageServer {
		readonly ConcurrentDictionary<string, Task<IChannel>> channels = new ConcurrentDictionary<string, Task<IChannel>>();
		public Task<IChannel> Create(string userName) {
			return channels.GetOrAdd(userName, x => {
				var connectionCompletionSource = new TaskCompletionSource<IChannel>();
				new Channel(this, connectionCompletionSource, x);
				return connectionCompletionSource.Task;
			});
		}
		sealed partial class Channel : IChannel {
			readonly TaskCompletionSource<IChannel> connectionCompletionSource;
			readonly DevAVEmpployeesInMemoryServer service;
			public Channel(DevAVEmpployeesInMemoryServer service,
				TaskCompletionSource<IChannel> connectionCompletionSource, string userName) {
				this.createDb = service.createDb;
				this.UserName = userName;
				this.connectionCompletionSource = connectionCompletionSource;
				this.service = service;
				this.state = string.IsNullOrEmpty(userName) ? State.Invalid : State.Idle;
				if(state != State.Invalid)
					this.heartBeat = new Timer(OnHeartBeat, this, 25, period: 25);
				else
					connectionCompletionSource.SetException(new ArgumentException("userName"));
			}
			void IDisposable.Dispose() {
				OnDispose();
				GC.SuppressFinalize(this);
			}
			void OnDispose() {
				UnsubscribeAll();
				heartBeat.Dispose();
				connectionCompletionSource.TrySetCanceled();
				Task<IChannel> connectionTask;
				service.channels.TryRemove(UserName, out connectionTask);
				CleanUp();
			}
			void UnsubscribeAll() {
				this.onChannelEvent = null;
				this.onContactEvents = null;
				this.onMessageEvents = null;
			}
			void CleanUp() {
				currentEmployee = null;
				currentContactCore = null;
				employeeContacts = null;
				Interlocked.Exchange(ref queuedContactEvents, new ConcurrentQueue<ContactEvent>());
				Interlocked.Exchange(ref queuedMessageEvents, new ConcurrentQueue<MessageEvent>());
			}
			public string UserName {
				get;
				private set;
			}
			Action<ChannelEvent> onChannelEvent;
			public void Subscribe(Action<ChannelEvent> onEvent) {
				this.onChannelEvent += onEvent;
			}
			public void Send(ChannelCommand command) {
				if(command is LogOff) {
					CleanUp();
					state = State.Authorization;
				}
			}
			#region Life Cycle
			enum State {
				Invalid,
				Idle,
				Authorization,
				Running
			}
			readonly Timer heartBeat;
			State state;
			bool IsAuthorizationInProgress {
				get { return state == State.Authorization; }
			}
			static void OnHeartBeat(object state) {
				var channel = (Channel)state;
				switch(channel.state) {
					case State.Idle:
						channel.OnStart();
						break;
					case State.Authorization:
						channel.OnAuthorization();
						break;
					case State.Running:
						channel.OnRunning();
						break;
					case State.Invalid:
						channel.OnDispose();
						break;
				}
			}
			void OnStart() {
				connectionCompletionSource.SetResult(this);
				state = State.Authorization;
			}
			Task<string> accessTokenQuery;
			int authorizationLock = 0;
			readonly string channelSalt = Internal.SecurityHelper.GetSalt();
			void OnAuthorization() {
				if(onChannelEvent == null)
					return;
				if(Interlocked.CompareExchange(ref authorizationLock, 1, 0) == 0) {
					var @event = new CredentialsRequiredEvent(this, channelSalt, OnAccessTokenReceivedAsynchronously, OnAccessTokenReceivedImmediatelly);
					onChannelEvent.Invoke(@event);
					if(Volatile.Read(ref authorizationLock) == 1) {
						accessTokenQuery = @event.GetAccessTokenQuery();
						onChannelEvent.Invoke(new ChannelReadyEvent(this));
					}
				}
			}
			readonly ConcurrentDictionary<string, string> accessTokens = new ConcurrentDictionary<string, string>();
			string lastAuthorizationToken;
			void OnAccessTokenReceivedImmediatelly(string token) {
				EnsureAuthorized(lastAuthorizationToken = token);
				Volatile.Write(ref authorizationLock, 0);
			}
			void OnAccessTokenReceivedAsynchronously() {
				if(accessTokenQuery.IsCompleted) {
					if(!accessTokenQuery.IsCanceled)
						EnsureAuthorized(lastAuthorizationToken = accessTokenQuery.Result);
					else {
						if(!string.IsNullOrEmpty(lastAuthorizationToken))
							EnsureAuthorized(lastAuthorizationToken);
					}
					accessTokenQuery = null;
					Volatile.Write(ref authorizationLock, 0);
				}
				else state = State.Invalid;
			}
			void EnsureAuthorized(string accessToken) {
				string userToken = accessTokens.GetOrAdd(UserName, x => GetPasswordHash(string.Empty, channelSalt));
				if(accessToken == userToken)
					OnAuthorized();
			}
			void OnAuthorized() {
				heartBeat.Change(25, 750);
				currentEmployee = null;
				ResetActivity();
				state = State.Running;
				onChannelEvent?.Invoke(new ChannelReadyEvent(this));
			}
			int runningLock = 0;
			void OnRunning() {
				if(Interlocked.CompareExchange(ref runningLock, 1, 0) == 0) {
					if(employeeContacts != null && onContactEvents != null) {
						EnsureActivity();
						if(currentEmployee != null)
							EnsureUnread();
					}
					EnsureQueuedEvents();
					Volatile.Write(ref runningLock, 0);
				}
			}
			void EnsureQueuedEvents() {
				if(onContactEvents != null) {
					var events = GetEvents(ref queuedContactEvents, x => x.Id);
					if(events != null)
						onContactEvents?.Invoke(events);
				}
				if(onMessageEvents != null) {
					var events = GetEvents(ref queuedMessageEvents, x => x.Id);
					if(events != null)
						onMessageEvents?.Invoke(events);
				}
			}
			static Dictionary<long, TEvent> GetEvents<TEvent>(ref ConcurrentQueue<TEvent> queuedEvents, Func<TEvent, long> getId) {
				var eventsArray = queuedEvents.ToArray();
				if(eventsArray.Length > 0) {
					var events = new Dictionary<long, TEvent>(eventsArray.Length);
					int index = 0;
					for(; index < eventsArray.Length; index++) {
						long id = getId(eventsArray[index]);
						if(events.ContainsKey(id))
							break;
						events.Add(id, eventsArray[index]);
					}
					var queue = new ConcurrentQueue<TEvent>();
					for(; index < eventsArray.Length; index++) {
						queue.Enqueue(eventsArray[index]);
					}
					Interlocked.Exchange(ref queuedEvents, queue);
					return events;
				}
				return null;
			}
			#endregion
		}
		public static string GetPasswordHash(string password, string salt) {
			return Internal.SecurityHelper.GetHash(password, salt);
		}
	}
}
