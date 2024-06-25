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

using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Drawing;
using System.Runtime.Serialization;
using DevExpress.DataAnnotations;
using System.Collections.Generic;
namespace DevExpress.DevAV {
	public class CustomerEmployee : DatabaseObject {
		[Required, Display(Name = "First Name")]
		public string FirstName { get; set; }
		[Required, Display(Name = "Last Name")]
		public string LastName { get; set; }
		[Display(Name = "Full Name")]
		public string FullName { get; set; }
		public PersonPrefix Prefix { get; set; }
		[Required, Phone, Display(Name = "Mobile Phone")]
		public string MobilePhone { get; set; }
		[Required, EmailAddress]
		public string Email { get; set; }
		public virtual Picture Picture { get; set; }
		public long? PictureId { get; set; }
		public virtual Customer Customer { get; set; }
		public long? CustomerId { get; set; }
		public virtual CustomerStore CustomerStore { get; set; }
		public long? CustomerStoreId { get; set; }
		public string Position { get; set; }
		public bool IsPurchaseAuthority { get; set; }
		public virtual ICollection<CustomerCommunication> CustomerCommunications { get; set; }
		public Address Address {
			get { return (CustomerStore != null) ? CustomerStore.Address : null; }
		}
		public virtual ICollection<EmployeeTask> EmployeeTasks { get; set; }
		Image _photo = null;
		[NotMapped]
		public Image Photo {
			get {
				if(_photo == null)
					_photo = Picture.CreateImage();
				return _photo;
			}
			set {
				_photo = value;
				Picture = PictureExtension.FromImage(value);
			}
		}
		public override string ToString() {
			return FullName;
		}
	}
}
