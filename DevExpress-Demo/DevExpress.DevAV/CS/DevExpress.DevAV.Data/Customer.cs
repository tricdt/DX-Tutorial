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
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Drawing;
using System.Runtime.Serialization;
using DevExpress.DataAnnotations;
using DevExpress.Utils;
using DevExpress.XtraEditors.Controls;
namespace DevExpress.DevAV {
	public enum CustomerStatus {
		Active,
		Suspended
	}
	public partial class Customer : DatabaseObject {
		public Customer() {
			Employees = new List<CustomerEmployee>();
			Orders = new List<Order>();
#if DXCORE3
			_homeOffice = new Address();
			_billingAddress = new Address();
			_homeOffice.PropertyChanged += (s, e) => SetPropertyValue(e.PropertyName, "HomeOffice", (Address)s);
			_billingAddress.PropertyChanged += (s, e) => SetPropertyValue(e.PropertyName, "BillingAddress", (Address)s);
#else
			HomeOffice = new Address();
			BillingAddress = new Address();
#endif
		}
		[Required]
		public string Name { get; set; }
#if DXCORE3
		Address _homeOffice;
		[NotMapped]
		public Address HomeOffice {
			get {
				AddressHelper.UpdateAddress(_homeOffice, HomeOfficeLine, HomeOfficeCity, HomeOfficeState, HomeOfficeZipCode, HomeOfficeLatitude, HomeOfficeLongitude);
				return _homeOffice;
			}
			set { AddressHelper.UpdateAddress(_homeOffice, value.Line, value.City, value.State, value.ZipCode, value.Latitude, value.Longitude); }
		}
		Address _billingAddress;
		[NotMapped]
		public Address BillingAddress {
			get {
				AddressHelper.UpdateAddress(_billingAddress, BillingAddressLine, BillingAddressCity, BillingAddressState, BillingAddressZipCode, BillingAddressLatitude, BillingAddressLongitude);
				return _billingAddress;
			}
			set { AddressHelper.UpdateAddress(_billingAddress, value.Line, value.City, value.State, value.ZipCode, value.Latitude, value.Longitude); }
		}
#else
		public Address HomeOffice { get; set; }
		public Address BillingAddress { get; set; }
#endif
#if ONGENERATEDATABASE || DXCORE3
		[EditorBrowsable(EditorBrowsableState.Never)]
		public string HomeOfficeLine { get; set; }
		[EditorBrowsable(EditorBrowsableState.Never)]
		public string HomeOfficeCity { get; set; }
		[EditorBrowsable(EditorBrowsableState.Never)]
		public string HomeOfficeZipCode { get; set; }
		[EditorBrowsable(EditorBrowsableState.Never)]
		public string BillingAddressLine { get; set; }
		[EditorBrowsable(EditorBrowsableState.Never)]
		public string BillingAddressCity { get; set; }
		[EditorBrowsable(EditorBrowsableState.Never)]
		public string BillingAddressZipCode { get; set; }
#endif
		[EditorBrowsable(EditorBrowsableState.Never)]
		public StateEnum HomeOfficeState { get; set; }
		[EditorBrowsable(EditorBrowsableState.Never)]
		public double HomeOfficeLatitude { get; set; }
		[EditorBrowsable(EditorBrowsableState.Never)]
		public double HomeOfficeLongitude { get; set; }
		[EditorBrowsable(EditorBrowsableState.Never)]
		public StateEnum BillingAddressState { get; set; }
		[EditorBrowsable(EditorBrowsableState.Never)]
		public double BillingAddressLatitude { get; set; }
		[EditorBrowsable(EditorBrowsableState.Never)]
		public double BillingAddressLongitude { get; set; }
		public virtual List<CustomerEmployee> Employees { get; set; }
		[Phone]
		public string Phone { get; set; }
		[Phone]
		public string Fax { get; set; }
		[Url]
		public string Website { get; set; }
		[DataType(DataType.Currency)]
		public decimal AnnualRevenue { get; set; }
		[Display(Name = "Total Stores")]
		public int TotalStores { get; set; }
		[Display(Name = "Total Employees")]
		public int TotalEmployees { get; set; }
		public CustomerStatus Status { get; set; }
		[InverseProperty("Customer")]
		public virtual List<Order> Orders { get; set; }
		[InverseProperty("Customer")]
		public virtual List<Quote> Quotes { get; set; }
		[InverseProperty("Customer")]
		public virtual List<CustomerStore> CustomerStores { get; set; }
		public virtual string Profile { get; set; }
		public byte[] Logo { get; set; }
		Image img = null;
		public Image Image {
			get {
				if(img == null)
					img = CreateImage(Logo);
				return img;
			}
		}
		internal static Image CreateImage(byte[] data) {
			if(data == null)
				return ResourceImageHelper.CreateImageFromResourcesEx("DevExpress.DevAV.Resources.Unknown-user.png", typeof(Employee).Assembly);
			else
				return ByteImageConverter.FromByteArray(data);
		}
	}
}
