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
	using System.ComponentModel;
	using System.Threading.Tasks;
	public abstract class ChannelEvent {
		protected ChannelEvent(IChannel channel) {
			this.Channel = channel;
		}
		public IChannel Channel {
			get;
			private set;
		}
		public string UserName {
			get { return Channel.UserName; }
		}
	}
	public class CredentialsRequiredEvent : ChannelEvent {
		readonly Action onAccessTokenAsyn;
		readonly Action<string> onAccessTokenImmediate;
		public CredentialsRequiredEvent(IChannel channel, string salt, Action onAccessTokenAsync, Action<string> onAccessTokenImmediate)
			: base(channel) {
			this.Salt = salt;
			this.onAccessTokenAsyn = onAccessTokenAsync;
			this.onAccessTokenImmediate = onAccessTokenImmediate;
		}
		Task<string> accessTokenQuery;
		public string Salt {
			get;
			private set;
		}
		public void SetAccessTokenQuery(Task<string> query) {
			if(accessTokenQuery == null && query != null) {
				this.accessTokenQuery = query;
				if(onAccessTokenImmediate != null && query.IsCompleted && !query.IsCanceled)
					onAccessTokenImmediate(query.Result);
			}
		}
		[EditorBrowsable(EditorBrowsableState.Never)]
		public Task<string> GetAccessTokenQuery() {
			if(accessTokenQuery != null && onAccessTokenAsyn != null) {
				accessTokenQuery.ConfigureAwait(false)
					.GetAwaiter()
					.OnCompleted(onAccessTokenAsyn);
			}
			return accessTokenQuery;
		}
	}
	public class ChannelReadyEvent : ChannelEvent {
		public ChannelReadyEvent(IChannel channel)
			: base(channel) {
		}
	}
}
