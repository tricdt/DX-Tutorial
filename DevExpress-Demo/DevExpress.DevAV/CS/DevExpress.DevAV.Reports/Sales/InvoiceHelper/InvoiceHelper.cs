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
using System.Linq;
using System.Text;
using DevExpress.Spreadsheet;
using System.IO;
using Range = DevExpress.Spreadsheet.CellRange;
namespace DevExpress.DevAV.Reports.Spreadsheet {
	public class InvoiceHelper {
		EditActions editActions;
		Worksheet invoice;
		Order order;
		OrderCollections source;
		List<OrderItem> actualOrderItems;
		public Worksheet Invoice { get { return invoice; } }
		public InvoiceHelper(IWorkbook workbook, Tuple<OrderCollections, Order> dataSource, EditActions editActions) {
			this.source = dataSource.Item1;
			this.order = dataSource.Item2;
			this.editActions = editActions;
			SetActualOrderItems();
			LoadInvoice(workbook);
			CellsHelper.UpdateEditableCells(Invoice, order, source);
			CellsHelper.UpdateDependentCells(Invoice, order, source);
			if(AllowChangeOrder()) {
				CellsHelper.GenerateEditors(CellsHelper.OrderCells, Invoice);
				CreateCollectionEditors();
			}
			AddOrderItemsToSheet();
		}
		#region Event Handlers
		public void OnPreviewMouseLeftButton(Cell cell) {
			if(!AllowChangeOrder())
				return;
			var invoiceItemsArea = GetOrderItemsArea().Range;
			if(!CellsHelper.IsInvoiceSheetActive(cell) || invoiceItemsArea == null)
				return;
			if(CellsHelper.IsAddItemRange(cell, invoiceItemsArea))
				AddOrderItem();
			if(CellsHelper.IsRemoveItemRange(cell, invoiceItemsArea)) {
				RemoveOrderItem(cell.TopRowIndex - invoiceItemsArea.TopRowIndex);
				UpdateTotalValues();
			}
		}
		public void CellValueChanged(object sender, DevExpress.XtraSpreadsheet.SpreadsheetCellEventArgs e) {
			if(!AllowChangeOrder())
				return;
			string reference = e.Cell.GetReferenceA1();
			var oldCustomerId = order.CustomerId;
			string shiftedRange = CellsHelper.GetActualCellRange(reference, actualOrderItems.Any() ? -actualOrderItems.Count : 0);
			if(OrderPropertiesHelper.Setters.ContainsKey(shiftedRange)) {
				OrderPropertiesHelper.Setters[shiftedRange].Invoke(order, e.Cell.Value, source);
				if(CellsHelper.HasDependentCells(shiftedRange)) {
					if(order.CustomerId != oldCustomerId)
						UpdateCustomerStores();
					CellsHelper.UpdateDependentCells(Invoice, order, source);
					UpdateTotalValues();
				}
			}
			if(IsOrderItemsRegionModified(e.Cell))
				UpdateOrderItem(e.Cell);
		}
		public void SelectionChanged() {
			editActions.ActivateEditor();
		}
		#endregion
		public static Stream GetInvoiceTemplate() {
			return Utils.AssemblyHelper.GetResourceStream(typeof(InvoiceHelper).Assembly, "SalesInvoice.xltx", false);
		}
		void LoadInvoice(IWorkbook workbook) {
			invoice = workbook.Worksheets[CellsHelper.InvoiceWorksheetName];
			if(!AllowChangeOrder())
				invoice.Unprotect(CellsHelper.InvoiceWorksheetPassword);
		}
		#region OrderItems Management
		void SetActualOrderItems() {
			actualOrderItems = order.OrderItems.ToList();
		}
		void AddOrderItem() {
			editActions.CloseEditor.Invoke();
			var orderItem = editActions.CreateOrderItem();
			orderItem.Order = order;
			orderItem.OrderId = order.Id;
			editActions.AddOrderItem(orderItem);
			actualOrderItems.Add(orderItem);
			AddOrderItemToSheet(orderItem);
			if(actualOrderItems.Count == 1)
				Invoice.Rows.Remove(GetOrderItemsArea().Range.TopRowIndex);
		}
		void AddOrderItemToSheet(OrderItem orderItem) {
			var invoiceItemsArea = GetOrderItemsArea();
			int rowIndex = invoiceItemsArea.Range.BottomRowIndex;
			Invoice.Rows.Insert(rowIndex);
			Invoice.Rows[rowIndex].Height = Invoice.Rows[rowIndex - 1].Height;
			Invoice.Rows[rowIndex + 1].Height = Invoice.Rows[rowIndex].Height;
			Range range = invoiceItemsArea.Range;
			Range itemRange = Invoice.Range.FromLTRB(range.LeftColumnIndex, range.BottomRowIndex, range.RightColumnIndex, range.BottomRowIndex);
			if(range.RowCount == 1) {
				Invoice["K24"].FormulaInvariant = "=SUM(K22:K23)";
				invoiceItemsArea.Range = Invoice.Range.FromLTRB(range.LeftColumnIndex, range.TopRowIndex - 1, range.RightColumnIndex, range.BottomRowIndex)
					.GetRangeWithAbsoluteReference();
				if(AllowChangeOrder())
					UpdateOrderItemEditors();
			}
			CellsHelper.CopyOrderItemRange(itemRange);
			OrderPropertiesHelper.InitializeOrderItem(itemRange, orderItem);
		}
		void AddOrderItemsToSheet() {
			actualOrderItems.ForEach(x => AddOrderItemToSheet(x));
			if(actualOrderItems.Any())
				Invoice.Rows.Remove(GetOrderItemsArea().Range.TopRowIndex);
		}
		void UpdateOrderItem(Cell cell) {
			var verticalOffset = GetOrderItemOffset(cell);
			Range orderItemsRange = GetOrderItemsArea().Range;
			var orderItem = actualOrderItems[verticalOffset];
			var orderItemRange = Invoice.Range.FromLTRB(orderItemsRange.LeftColumnIndex, orderItemsRange.TopRowIndex + verticalOffset,
				orderItemsRange.RightColumnIndex, orderItemsRange.TopRowIndex + verticalOffset);
			OrderPropertiesHelper.UpdateProductUnits(orderItem, orderItemRange, Invoice);
			OrderPropertiesHelper.UpdateProduct(orderItem, CellsHelper.GetOrderItemCellValue(CellsKind.ProductDescription, orderItemRange, Invoice), source);
			OrderPropertiesHelper.UpdateProductPrice(cell, orderItem, orderItemRange);
			AsyncUpdateSummaries(orderItem, orderItemRange);
		}
		void RemoveOrderItem(int deletedRowOffset) {
			editActions.CloseEditor.Invoke();
			var item = actualOrderItems[deletedRowOffset];
			var actualRowIndex = GetOrderItemsArea().Range.TopRowIndex + deletedRowOffset;
			actualOrderItems.Remove(item);
			editActions.RemoveOrderItem(item);
			Invoice.Rows.Remove(actualRowIndex);
		}
		void UpdateOrderItemEditors() {
			CellsHelper.RemoveAllEditors("B23:M23", Invoice);
			CellsHelper.GenerateEditors(CellsHelper.OrderItemCells, Invoice);
			CellsHelper.CreateCollectionEditor<Product>(CellsKind.ProductDescription, Invoice, source.Products, x => x.Name);
		}
		#endregion
		#region CustomerStores Management
		void UpdateCustomerStores() {
			source.CustomerStores = editActions.GetCustomerStores(order.CustomerId);
			SetDefaultCustomerStore();
			UpdateCustomerStoresEditor();
		}
		void UpdateCustomerStoresEditor() {
			UpdateCollectionEditors();
		}
		void SetDefaultCustomerStore() {
			var store = source.CustomerStores.First();
			OrderPropertiesHelper.UpdateCustomerStoreIfNeeded(order, store.City, source);
			Invoice.Cells[CellsHelper.FindLeftCell(CellsKind.CustomerStoreName)].Value = store.City;
		}
		#endregion
		void CreateCollectionEditors() {
			CellsHelper.CreateCollectionEditor<Customer>(CellsKind.CustomerName, Invoice, source.Customers, x => x.Name);
			CellsHelper.CreateCollectionEditor<Employee>(CellsKind.EmployeeName, Invoice, source.Employees, x => x.FullName);
			UpdateCollectionEditors();
		}
		void UpdateCollectionEditors() {
			CellsHelper.RemoveEditor(CellsKind.CustomerStoreName, Invoice);
			CellsHelper.CreateCollectionEditor<CustomerStore>(CellsKind.CustomerStoreName, Invoice, source.CustomerStores, x => x.City);
		}
		void AsyncUpdateSummaries(OrderItem orderItem, Range orderItemRange) {
			System.Windows.Threading.Dispatcher.CurrentDispatcher.BeginInvoke((Action)(() => {
				orderItem.Discount = (int)CellsHelper.GetOrderItemCellValue(CellsKind.Discount, orderItemRange, Invoice).NumericValue;
				orderItem.Total = (int)CellsHelper.GetOrderItemCellValue(CellsKind.Total, orderItemRange, Invoice).NumericValue;
				UpdateTotalValues();
			}));
		}
		void UpdateTotalValues() {
			order.SaleAmount = (decimal)CellsHelper.GetOrderCellValue(CellsKind.SubTotal, actualOrderItems, Invoice).NumericValue;
			order.TotalAmount = (decimal)CellsHelper.GetOrderCellValue(CellsKind.TotalDue, actualOrderItems, Invoice).NumericValue;
		}
		#region Utils
		bool AllowChangeOrder() {
			return !(source.IsEmpty && editActions.IsDefaultActions);
		}
		bool IsOrderItemsRegionModified(Cell cell) {
			return cell.Areas.First().IsIntersecting(GetOrderItemsArea().Range);
		}
		DefinedName GetOrderItemsArea() {
			return Invoice.DefinedNames.GetDefinedName("InvoiceItems");
		}
		int GetOrderItemOffset(Cell cell) {
			return cell.TopRowIndex - GetOrderItemsArea().Range.TopRowIndex;
		}
		int GetOrderItemPropertyOffset(Cell cell) {
			return cell.LeftColumnIndex - GetOrderItemsArea().Range.LeftColumnIndex;
		}
		#endregion
	}
	public class OrderCollections {
		public OrderCollections() {
			Customers = Enumerable.Empty<Customer>();
			Products = Enumerable.Empty<Product>();
			Employees = Enumerable.Empty<Employee>();
			CustomerStores = Enumerable.Empty<CustomerStore>();
		}
		public IEnumerable<Customer> Customers { get; set; }
		public IEnumerable<Product> Products { get; set; }
		public IEnumerable<Employee> Employees { get; set; }
		public IEnumerable<CustomerStore> CustomerStores { get; set; }
		public bool IsEmpty { get { return !(Customers.Any() || Products.Any() || Employees.Any() || CustomerStores.Any()); } }
	}
	public class EditActions {
		public EditActions() {
			ActivateEditor = () => { };
			CloseEditor = () => { };
			GetCustomerStores = x => Enumerable.Empty<CustomerStore>();
			CreateOrderItem = () => null;
			AddOrderItem = x => { };
			RemoveOrderItem = x => { };
			IsDefaultActions = true;
		}
		public Action ActivateEditor { get; set; }
		public Action CloseEditor { get; set; }
		public Func<long?, IEnumerable<CustomerStore>> GetCustomerStores { get; set; }
		public Func<OrderItem> CreateOrderItem { get; set; }
		public Action<OrderItem> AddOrderItem { get; set; }
		public Action<OrderItem> RemoveOrderItem { get; set; }
		public bool IsDefaultActions { get; set; }
	}
}
