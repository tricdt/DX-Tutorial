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
	using System.Linq;
	using System.Threading.Tasks;
	using DevExpress.DevAV;
	using DevExpress.DevAV.Chat.Commands;
	using DevExpress.DevAV.Chat.Events;
	using DevExpress.DevAV.Chat.Model;
	partial class DevAVEmpployeesInMemoryServer {
		partial class Channel {
			Action<Dictionary<long, ContactEvent>> onContactEvents;
			public void Subscribe(Action<Dictionary<long, ContactEvent>> onEvents) {
				this.onContactEvents += onEvents;
			}
			ConcurrentQueue<ContactEvent> queuedContactEvents = new ConcurrentQueue<ContactEvent>();
			public void Send(ContactCommand command) {
				var addMessage = command as AddMessage;
				if(addMessage != null) {
					var state = EnsureMessageState(command.Contact);
					queuedContactEvents.Enqueue(state.OnNewMessage(addMessage, GetCurrentContact()));
				}
				var readMessages = command as ReadMessages;
				if(readMessages != null) {
					queuedContactEvents.Enqueue(new AllMessagesRead(command.Contact.ID));
				}
				var clearConversation = command as ClearConversation;
				if(clearConversation != null) {
					var state = EnsureMessageState(command.Contact);
					queuedContactEvents.Enqueue(state.OnClear(clearConversation));
				}
			}
			IReadOnlyList<Employee> employeeContacts;
			public async Task<IReadOnlyCollection<Contact>> GetContacts() {
				if(IsAuthorizationInProgress)
					return new Contact[0];
				if(employeeContacts == null)
					employeeContacts = await LoadEmployeeContacts();
				return employeeContacts.Select(CreateContact).ToArray();
			}
			long[] ContactIds {
				get { return employeeContacts.Select(x => x.Id).ToArray(); }
			}
			static Contact CreateContact(Employee employee) {
				var contact = new Contact(employee.Id, employee.FullName, employee.Photo);
				contact.LastOnline = DataGenerator.GetLastYesterdayTime();
				return contact;
			}
			TimeoutState<StatusChanged> inactiveState;
			void EnsureActivity() {
				var events = new List<ContactEvent>();
				if(inactiveState == null) {
					inactiveState = new TimeoutState<StatusChanged>(ContactIds, events, x => new StatusChanged(x));
				}
				else {
					if(inactiveState.Count > 0)
						inactiveState.Ensure(events);
					else {
						var lastOnline = DateTime.Now.AddSeconds(-1);
						inactiveState.Update(ContactIds, events, x => new StatusChanged(x, lastOnline));
					}
				}
				if(events.Count > 0)
					onContactEvents?.Invoke(events.ToDictionary(x => x.Id));
			}
			TimeoutState<UnreadChanged> unreadState;
			void EnsureUnread() {
				var events = new List<ContactEvent>();
				if(unreadState == null) {
					EnsureMessageState(currentEmployee.Id);
					var createUnreadChangedEvent = new Func<long, UnreadChanged>(x => HistoryState.Dequeue(x, messagesState));
					unreadState = new TimeoutState<UnreadChanged>(ContactIds, events, createUnreadChangedEvent);
				}
				else {
					if(unreadState.Count > 0)
						unreadState.Ensure(events);
					else
						unreadState.Update(ContactIds, events);
				}
				if(events.Count > 0)
					onContactEvents?.Invoke(events.ToDictionary(x => x.Id));
			}
			void ResetActivity() {
				inactiveState = null;
			}
		}
	}
}
