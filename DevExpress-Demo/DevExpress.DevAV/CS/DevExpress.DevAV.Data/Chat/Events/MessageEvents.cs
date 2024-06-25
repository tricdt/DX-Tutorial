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
	using DevExpress.DevAV.Chat.Model;
	public abstract class MessageEvent {
		public MessageEvent(long id) {
			this.Id = id;
		}
		public long Id {
			get;
			private set;
		}
		public virtual void Apply(Message entity) {
		}
	}
	public class MessageDeleted : MessageEvent {
		public MessageDeleted(long id)
			: base(id) {
		}
		public override void Apply(Message entity) {
			entity.SentOrUpdated = System.DateTime.Now;
			entity.Text = string.Empty;
			entity.MarkAsDeleted();
		}
	}
	public class MessageLiked : MessageEvent {
		public MessageLiked(long id)
			: base(id) {
		}
		public override void Apply(Message entity) {
			entity.MarkAsLiked();
		}
	}
	public class MessageTextChanged : MessageEvent {
		public MessageTextChanged(long id, string text)
			: base(id) {
			this.Text = text;
		}
		public string Text {
			get;
			private set;
		}
		public override void Apply(Message entity) {
			entity.SentOrUpdated = System.DateTime.Now;
			entity.Text = Text;
			entity.MarkAsEdited();
		}
	}
}
