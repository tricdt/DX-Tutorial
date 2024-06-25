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

namespace DevExpress.DevAV.Chat.Events {
	using System;
	using DevExpress.DevAV.Chat.Model;
	public abstract class ContactEvent {
		public ContactEvent(long id) {
			this.Id = id;
		}
		public long Id {
			get;
			private set;
		}
		public abstract void Apply(Contact contact);
	}
	public class StatusChanged : ContactEvent {
		public StatusChanged(long id)
			: base(id) {
			Status = Contact.Status.Active;
		}
		public StatusChanged(long id, DateTime lastOnline)
			: base(id) {
			Status = Contact.Status.Inactive;
			LastOnline = lastOnline;
		}
		public Contact.Status Status {
			get;
			private set;
		}
		public DateTime LastOnline {
			get;
			private set;
		}
		public override void Apply(Contact contact) {
			contact.StatusCore = Status;
			contact.LastOnline = Status == Contact.Status.Inactive ? LastOnline : DateTime.MinValue;
		}
	}
	public class UnreadChanged : ContactEvent {
		public UnreadChanged(long id, int unreadCount)
			: base(id) {
			UnreadCount = unreadCount;
		}
		public int UnreadCount {
			get;
			private set;
		}
		public override void Apply(Contact contact) {
			contact.UnreadCount += UnreadCount;
			contact.LastActivity = DateTime.Now;
		}
	}
	public class AllMessagesRead : ContactEvent {
		public AllMessagesRead(long id)
			: base(id) {
		}
		public override void Apply(Contact contact) {
			contact.UnreadCount = 0;
		}
	}
	public class NewMessages : ContactEvent {
		public NewMessages(long id)
			: base(id) {
		}
		public override void Apply(Contact entity) {
		}
	}
}
