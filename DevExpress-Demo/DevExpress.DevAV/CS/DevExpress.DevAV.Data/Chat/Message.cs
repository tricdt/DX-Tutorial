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

namespace DevExpress.DevAV.Chat.Model {
	using System;
	using System.ComponentModel;
	public class Message {
		[Flags]
		public enum MessageStatus {
			Own = 1,
			Edited = 2,
			Deleted = 4,
			FirstInBlock = 8,
			FirstInReply = 16,
			Liked = 32,
		}
		public Message(Contact owner, string text, DateTime sent) {
			this.Owner = owner;
			this.Text = text;
			this.SentOrUpdated = sent;
		}
		[Browsable(false)]
		public long ID {
			get;
			internal set;
		}
		public Contact Owner {
			get;
			private set;
		}
		public string Text {
			get;
			internal set;
		}
		public DateTime SentOrUpdated {
			get;
			internal set;
		}
		public string StatusText {
			get {
				if(SentOrUpdated == DateTime.MinValue)
					return string.Empty;
				if(IsDeleted)
					return "Deleted " + GetTimeOrDate().ToLowerInvariant();
				if(IsEdited)
					return "Edited " + GetTimeOrDate().ToLowerInvariant();
				return GetTimeOrDate();
			}
		}
		string GetTimeOrDate() {
			var diff = DateTime.Now.Subtract(SentOrUpdated);
			if(diff.TotalMinutes < 1)
				return "Just now";
			if(diff.TotalMinutes < 60)
				return ((int)(0.5 + diff.TotalMinutes)).ToString() + " minutes ago";
			if(diff.TotalMinutes < 90)
				return "1 hour ago";
			if(diff.TotalMinutes < 360)
				return ((int)(diff.TotalMinutes / 60 + 0.5)).ToString() + " hours ago";
			if(diff.TotalMinutes < 1440)
				return SentOrUpdated.ToShortTimeString();
			if(diff.TotalMinutes < 2880)
				return "Yesterday at " + SentOrUpdated.ToShortTimeString();
			return SentOrUpdated.ToShortDateString();
		}
		public override string ToString() {
			return $"From {Owner.UserName}, {StatusText}: '{Text}'";
		}
		[Browsable(false)]
		public bool IsEdited {
			get { return (Status & MessageStatus.Edited) == MessageStatus.Edited; }
		}
		[Browsable(false)]
		public bool IsDeleted {
			get { return (Status & MessageStatus.Deleted) == MessageStatus.Deleted; }
		}
		[Browsable(false)]
		public bool IsLiked {
			get { return (Status & MessageStatus.Liked) == MessageStatus.Liked; }
		}
		[Browsable(false)]
		public bool IsOwnMessage {
			get { return (Status & MessageStatus.Own) == MessageStatus.Own; }
		}
		[Browsable(false)]
		public bool IsFirstMessageOfReply {
			get { return (Status & MessageStatus.FirstInReply) == MessageStatus.FirstInReply; }
		}
		[Browsable(false)]
		public bool IsFirstMessageOfBlock {
			get { return (Status & MessageStatus.FirstInBlock) == MessageStatus.FirstInBlock; }
		}
		#region Internal states
		MessageStatus Status;
		internal void MarkAsEdited() {
			Status |= MessageStatus.Edited;
		}
		internal void MarkAsDeleted() {
			Status |= MessageStatus.Deleted;
		}
		internal void MarkAsOwnMessage(bool value) {
			if(value) Status |= MessageStatus.Own;
		}
		internal void ClearFirstInBlockAndInReply() {
			Status &= ~(MessageStatus.FirstInBlock | MessageStatus.FirstInReply);
		}
		internal void MarkAsFirstInBlock() {
			Status |= MessageStatus.FirstInBlock;
		}
		internal void MarkAsFirstInReply(Message prevMessage) {
			if(prevMessage != null) {
				var diff = SentOrUpdated.Subtract(prevMessage.SentOrUpdated);
				if(diff.TotalSeconds > DevAVEmpployeesInMemoryServer.MessagesGeneratorInterval)
					Status |= MessageStatus.FirstInReply;
			}
		}
		internal void MarkAsLiked() {
			Status |= MessageStatus.Liked;
		}
		#endregion Internal states
	}
}
