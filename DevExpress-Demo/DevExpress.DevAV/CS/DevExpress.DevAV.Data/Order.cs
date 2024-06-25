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
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace DevExpress.DevAV {
	public enum OrderShipMethod {
		[Display(Name = "Ground")]
		Ground,
		[Display(Name = "Air")]
		Air
	}
	public enum ShipmentCourier {
		None,
		[Display(Name = "FedEx")]
		FedEx,
		[Display(Name = "UPS")]
		UPS,
		[Display(Name = "DHL")]
		DHL
	}
	public enum ShipmentStatus {
		[Display(Name = "Awaiting")]
		Awaiting,
		[Display(Name = "Transit")]
		Transit,
		[Display(Name = "Received")]
		Received
	}
	public enum PaymentStatus {
		[Display(Name = "Unpaid")]
		Unpaid,
		[Display(Name = "Paid in full")]
		PaidInFull,
		[Display(Name = "Refund in full")]
		RefundInFull,
		[Display(Name = "")]
		Other
	}
	public class Order : DatabaseObject {
		public Order() {
			OrderItems = new List<OrderItem>();
		}
		public string InvoiceNumber { get; set; }
		public virtual Customer Customer { get; set; }
		public long? CustomerId { get; set; }
		public virtual CustomerStore Store { get; set; }
		public long? StoreId { get; set; }
		public string PONumber { get; set; }
		public virtual Employee Employee { get; set; }
		public long? EmployeeId { get; set; }
		public DateTime OrderDate { get; set; }
		[DataType(DataType.Currency)]
		public decimal SaleAmount { get; set; }
		[DataType(DataType.Currency)]
		public decimal ShippingAmount { get; set; }
		[DataType(DataType.Currency)]
		public decimal TotalAmount { get; set; }
		public DateTime? ShipDate { get; set; }
		public OrderShipMethod ShipMethod { get; set; }
		public string OrderTerms { get; set; }
		public virtual List<OrderItem> OrderItems { get; set; }
		public ShipmentCourier ShipmentCourier { get; set; }
		public string ShipmentCourierId { get; set; }
		public ShipmentStatus ShipmentStatus { get; set; }
		public string Comments { get; set; }
		[DataType(DataType.Currency)]
		public decimal RefundTotal { get; set; }
		[DataType(DataType.Currency)]
		public decimal PaymentTotal { get; set; }
		[NotMapped]
		public PaymentStatus PaymentStatus {
			get {
				if(PaymentTotal == decimal.Zero && RefundTotal == decimal.Zero)
					return DevAV.PaymentStatus.Unpaid;
				if(RefundTotal == TotalAmount)
					return DevAV.PaymentStatus.RefundInFull;
				if(PaymentTotal == TotalAmount)
					return DevAV.PaymentStatus.PaidInFull;
				return DevAV.PaymentStatus.Other;
			}
		}
		[NotMapped]
		public double ActualWeight {
			get {
				var weight = 0.0;
				if(OrderItems != null)
					foreach(var item in OrderItems)
						if(item.Product != null)
							weight += item.Product.Weight * item.ProductUnits;
				return weight;
			}
		}
	}
}
