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
#if DXCORE3
	using Microsoft.EntityFrameworkCore;
#else
	using System.Data.Entity;
#endif
	partial class DevAVEmpployeesInMemoryServer {
		partial class Channel {
			Action<Dictionary<long, MessageEvent>> onMessageEvents;
			public void Subscribe(Action<Dictionary<long, MessageEvent>> onEvents) {
				this.onMessageEvents += onEvents;
			}
			ConcurrentQueue<MessageEvent> queuedMessageEvents = new ConcurrentQueue<MessageEvent>();
			public void Send(MessageCommand command) {
				if(command is DeleteMessage)
					queuedMessageEvents.Enqueue(new MessageDeleted(command.MessageId));
				if(command is LikeMessage)
					queuedMessageEvents.Enqueue(new MessageLiked(command.MessageId));
			}
			Employee currentEmployee;
			Contact currentContactCore;
			Contact GetCurrentContact(long? id = null) {
				if(currentContactCore == null || id.HasValue && currentContactCore.ID != id.Value)
					currentContactCore = new Contact(id.GetValueOrDefault(currentEmployee.Id), UserName, currentEmployee.Photo);
				return currentContactCore;
			}
			string GetName(Contact contact) {
				return employeeContacts.First(x => x.Id == contact.ID).FirstName;
			}
			long? primaryContactId;
			long GetPrimaryContactId() {
				return primaryContactId.GetValueOrDefault(employeeContacts[0].Id);
			}
			public async Task<IReadOnlyCollection<Message>> GetHistory(Contact contact) {
				if(IsAuthorizationInProgress)
					return new Message[0];
				if(!primaryContactId.HasValue)
					primaryContactId = contact.ID;
				if(currentEmployee == null)
					currentEmployee = await LoadEmployee(UserName);
				var state = EnsureMessageState(contact);
				return state.GetHistory();
			}
			ConcurrentDictionary<long, HistoryState> messagesState = new ConcurrentDictionary<long, HistoryState>();
			HistoryState EnsureMessageState(Contact contact) {
				return messagesState.GetOrAdd(contact.ID, id => EnsureMessageState(contact, currentEmployee.Id));
			}
			HistoryState EnsureMessageState(Contact contact, long currentId) {
				var currentContact = GetCurrentContact(currentId);
				if(employeeContacts != null) {
					string myName = currentEmployee.FirstName;
					if(contact.ID == GetPrimaryContactId())
						return InitializeMessageStateWithWeather(contact, currentId, currentContact, myName, GetName(contact));
					if(contact.ID == 10 )
						return InitializeMessageStateWithExpedition(contact, currentId, currentContact, myName, GetName(contact));
					if(contact.ID == 13 )
						return InitializeMessageStateWithCV(contact, currentId, currentContact, myName, GetName(contact));
					if(contact.ID == 27 )
						return InitializeMessageStateWithEmpty();
					if(contact.ID == 17 )
						return InitializeMessageStateWithEmpty();
				}
				return InitializeMessageStateWithLoremIpsum(contact, currentId, currentContact);
			}
			void EnsureMessageState(long currentId) {
				foreach(var employeeContact in employeeContacts) {
					if(!messagesState.ContainsKey(employeeContact.Id)) {
						var contact = new Contact(employeeContact.Id, employeeContact.FullName, employeeContact.Photo);
						var state = EnsureMessageState(contact, currentId);
						messagesState.TryAdd(employeeContact.Id, state);
					}
				}
			}
			HistoryState InitializeMessageStateWithEmpty() {
				return new HistoryState(new List<Message>());
			}
			HistoryState InitializeMessageStateWithWeather(Contact contact, long currentId, Contact currentContact, string you, string other) {
				var messages = new List<Message>(25);
				MessagesDataGenerator.Weather(currentId, contact.ID, (id, text, sent) => {
					var owner = (id == contact.ID) ? contact : currentContact;
					var message = new Message(owner, string.Format(text, you, other), sent) { ID = messages.Count };
					message.MarkAsOwnMessage(owner == currentContact);
					messages.Add(message);
				});
				UpdateMessagesInReplies(messages, currentId);
				UpdateLastOnlineAndActivity(contact, messages);
				messages[messages.Count - 4].MarkAsLiked();
				return new HistoryState(messages);
			}
			HistoryState InitializeMessageStateWithExpedition(Contact contact, long currentId, Contact currentContact, string you, string other) {
				var messages = new List<Message>(25);
				MessagesDataGenerator.Expedition(currentId, contact.ID, (id, text, sent) => {
					var owner = (id == contact.ID) ? contact : currentContact;
					var message = new Message(owner, string.Format(text, you, other), sent) { ID = messages.Count };
					message.MarkAsOwnMessage(owner == currentContact);
					messages.Add(message);
				});
				UpdateMessagesInReplies(messages, currentId);
				UpdateLastOnlineAndActivity(contact, messages);
				messages[messages.Count - 5].MarkAsLiked();
				return new HistoryState(messages);
			}
			HistoryState InitializeMessageStateWithCV(Contact contact, long currentId, Contact currentContact, string you, string other) {
				var messages = new List<Message>(25);
				MessagesDataGenerator.CV(currentId, contact.ID, (id, text, sent) => {
					var owner = (id == contact.ID) ? contact : currentContact;
					var message = new Message(owner, string.Format(text, you, other), sent) { ID = messages.Count };
					message.MarkAsOwnMessage(owner == currentContact);
					messages.Add(message);
				});
				UpdateMessagesInReplies(messages, currentId);
				UpdateLastOnlineAndActivity(contact, messages);
				messages[messages.Count - 2].MarkAsLiked();
				return new HistoryState(messages);
			}
			HistoryState InitializeMessageStateWithLoremIpsum(Contact contact, long currentId, Contact currentContact) {
				var messages = new List<Message>(150);
				int readCount = MessagesDataGenerator.LoremIpsum(currentId, contact.ID, (id, text, sent) => {
					var owner = (id == contact.ID) ? contact : currentContact;
					var message = new Message(owner, text, sent) { ID = messages.Count };
					message.MarkAsOwnMessage(owner == currentContact);
					if(!message.IsOwnMessage && DataGenerator.GetCount() > 7)
						message.MarkAsLiked();
					messages.Add(message);
				});
				UpdateMessagesInReplies(messages, currentId);
				UpdateLastOnline(contact, messages);
				return new HistoryState(messages, readCount);
			}
			static void UpdateLastOnline(Contact contact, List<Message> messages) {
				var lastMessageSent = messages.Last(x => x.Owner == contact).SentOrUpdated;
				if(lastMessageSent > contact.LastOnline)
					contact.LastOnline = lastMessageSent.AddMinutes(1);
			}
			static void UpdateLastOnlineAndActivity(Contact contact, List<Message> messages) {
				var lastMessageSent = messages.Last(x => x.Owner == contact).SentOrUpdated;
				if(lastMessageSent > contact.LastOnline)
					contact.LastActivity = contact.LastOnline = lastMessageSent.AddMinutes(1);
			}
			static void UpdateMessagesInReplies(List<Message> messages, long currentId) {
				Message prevMessageContact = null, prevMessageCurrent = null, prevMsg;
				foreach(Message msg in messages) {
					if(msg.Owner.ID == currentId) {
						if((prevMsg = prevMessageCurrent) == null)
							(prevMessageCurrent = msg).MarkAsFirstInBlock();
						prevMessageContact = null;
					}
					else {
						if((prevMsg = prevMessageContact) == null)
							(prevMessageContact = msg).MarkAsFirstInBlock();
						prevMessageCurrent = null;
					}
					msg.MarkAsFirstInReply(prevMsg);
				}
			}
			sealed class HistoryState {
				readonly List<Message> messages;
				int top;
				public HistoryState(List<Message> messages, int? top = null) {
					this.messages = messages;
					this.top = top.GetValueOrDefault(messages.Count);
				}
				public IReadOnlyCollection<Message> GetHistory() {
					return messages.Take(top).ToArray();
				}
				public NewMessages OnClear(ClearConversation @event) {
					messages.Clear();
					top = 0;
					return new NewMessages(@event.Contact.ID);
				}
				public NewMessages OnNewMessage(AddMessage @event, Contact owner) {
					var message = new Message(owner, @event.Message, @event.Sent);
					message.MarkAsOwnMessage(true);
					messages.Insert(top, message);
					message.ID = messages.Count;
					top += 1;
					return new NewMessages(@event.Contact.ID);
				}
				public static UnreadChanged Dequeue(long id, ConcurrentDictionary<long, HistoryState> messagesState) {
					HistoryState state;
					if(messagesState.TryGetValue(id, out state)) {
						int count = state.Dequeue(id);
						if(count > 0)
							return new UnreadChanged(id, count);
					}
					return null;
				}
				int Dequeue(long id) {
					int queuedCount = messages.Count - top;
					if(queuedCount > 0 && DataGenerator.GetCount() > 5) {
						int receivedCount = DataGenerator.GetCount(0, Math.Min(queuedCount, 3));
						for(int i = 0; i < receivedCount; i++) {
							messages[top + i].ClearFirstInBlockAndInReply();
							messages[top + i].SentOrUpdated = DateTime.Now.AddSeconds((i - receivedCount) * DataGenerator.GetCount(0, 60));
						}
						UpdateMessagesInReplies(messages, id);
						top += receivedCount;
						return receivedCount;
					}
					return -1;
				}
			}
		}
	}
}
