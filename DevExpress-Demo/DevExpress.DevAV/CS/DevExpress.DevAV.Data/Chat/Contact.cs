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
	using System.Collections.Concurrent;
	using System.ComponentModel;
	using System.Drawing;
	public class Contact {
		public enum Status {
			Inactive,
			Active,
		}
		[Browsable(false)]
		public long ID {
			get;
			private set;
		}
		public Contact(long id) {
			this.ID = id;
		}
		public Contact(long id, string userName, Image photo) {
			this.ID = id;
			this.UserName = userName;
			this.Avatar = CreateAvatar(id, photo);
		}
		public string UserName {
			get;
			private set;
		}
		public Image Avatar {
			get;
			internal set;
		}
		public DateTime LastActivity {
			get;
			internal set;
		}
		[Browsable(false)]
		public DateTime LastOnline {
			get;
			internal set;
		}
		public string LastOnlineText {
			get {
				if(!IsInactive)
					return "Online";
				if(LastOnline == DateTime.MinValue)
					return string.Empty;
				var diff = DateTime.Now.Subtract(LastOnline);
				if(diff.TotalMinutes < 1)
					return "Just now";
				if(diff.TotalMinutes < 60)
					return ((int)(0.5 + diff.TotalMinutes)).ToString() + " minutes ago";
				if(diff.TotalMinutes < 90)
					return "1 hour ago";
				if(diff.TotalMinutes < 360)
					return ((int)(diff.TotalMinutes / 60 + 0.5)).ToString() + " hours ago";
				if(diff.TotalMinutes < 1440)
					return LastOnline.ToShortTimeString();
				if(diff.TotalMinutes < 2880)
					return "Yesterday, " + LastOnline.ToShortTimeString();
				return LastOnline.ToShortDateString();
			}
		}
		public int UnreadCount {
			get;
			internal set;
		}
		protected internal Status StatusCore;
		[Browsable(false)]
		public bool HasUnreadMessages {
			get { return UnreadCount > 0; }
		}
		[Browsable(false)]
		public bool IsInactive {
			get { return StatusCore == Status.Inactive; }
		}
		public override string ToString() {
			return IsInactive ? $"{UserName}, {LastOnlineText}" : UserName;
		}
		readonly static ConcurrentDictionary<long, Image> avatars = new ConcurrentDictionary<long, Image>();
		static Image CreateAvatar(long id, Image image) {
			return avatars.GetOrAdd(id, x => Internal.ImageHelper.ClipImage(image));
		}
	}
}
