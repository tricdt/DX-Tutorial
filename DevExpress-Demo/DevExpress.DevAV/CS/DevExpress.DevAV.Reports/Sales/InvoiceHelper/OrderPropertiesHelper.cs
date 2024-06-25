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

using DevExpress.Spreadsheet;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Range = DevExpress.Spreadsheet.CellRange;
namespace DevExpress.DevAV.Reports.Spreadsheet {
	public static class OrderPropertiesHelper {
		public static Dictionary<string, Action<Order, CellValue, OrderCollections>> Setters { get { return propertySetters; } }
		static Dictionary<string, Action<Order, CellValue, OrderCollections>> propertySetters = CreatePropertySetters();
		static Dictionary<string, Action<Order, CellValue, OrderCollections>> CreatePropertySetters() {
			var result = new Dictionary<string, Action<Order, CellValue, OrderCollections>>();
			result.Add(CellsHelper.FindLeftCell(CellsKind.Date), (order, value, source) => order.OrderDate = value.DateTimeValue);
			result.Add(CellsHelper.FindLeftCell(CellsKind.InvoiceNumber), (order, value, source) => SetIfNumericValue(value, s => order.InvoiceNumber = s));
			result.Add(CellsHelper.FindLeftCell(CellsKind.PONumber), (order, value, source) => SetIfNumericValue(value, s => order.PONumber = s));
			result.Add(CellsHelper.FindLeftCell(CellsKind.ShipDate), (order, value, source) => order.ShipDate = value.DateTimeValue);
			result.Add(CellsHelper.FindLeftCell(CellsKind.ShipVia), (order, value, source) => { order.ShipmentCourier = (ShipmentCourier)Enum.Parse(typeof(ShipmentCourier), value.TextValue); });
			result.Add(CellsHelper.FindLeftCell(CellsKind.Terms), (order, value, source) => SetIfNumericValue(value, s => order.OrderTerms = string.Format("{0} Days", s)));
			result.Add(CellsHelper.FindLeftCell(CellsKind.CustomerName), (order, value, source) => UpdateCustomerIfNeeded(order, value, source));
			result.Add(CellsHelper.FindLeftCell(CellsKind.CustomerStoreName), (order, value, source) => UpdateCustomerStoreIfNeeded(order, value, source));
			result.Add(CellsHelper.FindLeftCell(CellsKind.EmployeeName), (order, value, source) => UpdateEmployeeIfNeeded(order, value, source));
			result.Add(CellsHelper.FindLeftCell(CellsKind.Comments), (order, value, source) => order.Comments = value.TextValue);
			result.Add(CellsHelper.FindLeftCell(CellsKind.Shipping), (order, value, source) => order.ShippingAmount = (decimal)value.NumericValue);
			return result;
		}
		public static void UpdateCustomerIfNeeded(Order order, CellValue value, OrderCollections source) {
			var newCustomer = source.Customers.First(x => x.Name == value.TextValue);
			if(order.StoreId != newCustomer.Id) {
				order.Customer = newCustomer;
				order.CustomerId = newCustomer.Id;
			}
		}
		public static void UpdateCustomerStoreIfNeeded(Order order, CellValue value, OrderCollections source) {
			var newStore = source.CustomerStores.First(x => x.City == value.TextValue);
			if(order.StoreId != newStore.Id) {
				order.Store = newStore;
				order.StoreId = newStore.Id;
			}
		}
		public static void UpdateEmployeeIfNeeded(Order order, CellValue value, OrderCollections source) {
			var newEmployee = source.Employees.First(x => x.FullName == value.TextValue);
			if(order.EmployeeId != newEmployee.Id) {
				order.Employee = newEmployee;
				order.EmployeeId = newEmployee.Id;
			}
		}
		public static void InitializeOrderItem(Range itemRange, OrderItem orderItem) {
			itemRange[CellsHelper.GetOffset(CellsKind.ProductDescription)].Value = orderItem.Product != null
				? orderItem.Product.Name : CellValue.Empty;
			itemRange[CellsHelper.GetOffset(CellsKind.Quantity)].Value = orderItem.ProductUnits > 0 ? orderItem.ProductUnits : 1;
			itemRange[CellsHelper.GetOffset(CellsKind.UnitPrice)].Value = (double)orderItem.ProductPrice;
			itemRange[CellsHelper.GetOffset(CellsKind.Discount)].Value = (double)orderItem.Discount;
		}
		public static void UpdateProduct(OrderItem orderItem, CellValue productCell, OrderCollections source) {
			var newProduct = source.Products.First(x => x.Name == productCell.TextValue);
			orderItem.Product = newProduct;
			orderItem.ProductId = newProduct.Id;
		}
		public static void UpdateProductUnits(OrderItem orderItem, Range orderItemRange, Worksheet invoice) {
			orderItem.ProductUnits = (int)CellsHelper.GetOrderItemCellValue(CellsKind.Quantity, orderItemRange, invoice).NumericValue;
		}
		public static void UpdateProductPrice(Cell cell, OrderItem orderItem, Range orderItemRange) {
			if(CellsHelper.IsOrderItemProductCell(cell, orderItemRange)) {
				orderItemRange[CellsHelper.GetOffset(CellsKind.UnitPrice)].Value = (double)orderItem.Product.SalePrice;
				orderItem.ProductPrice = orderItem.Product.SalePrice;
			}
		}
		static void SetIfNumericValue(CellValue value, Action<string> setValue) {
			if(value.IsNumeric)
				setValue.Invoke(value.NumericValue.ToString("F0"));
		}
	}
}
