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
using System.Drawing;
using System.IO;
using DevExpress.Utils;
using System.Runtime.Serialization;
namespace DevExpress.DevAV {
	public enum ProductCategory {
		[Display(Name = "Automation")]
		Automation,
		[Display(Name = "Monitors")]
		Monitors,
		[Display(Name = "Projectors")]
		Projectors,
		[Display(Name = "Televisions")]
		Televisions,
		[Display(Name = "Video Players")]
		VideoPlayers,
	}
	public class Product : DatabaseObject {
		public string Name { get; set; }
		public string Description { get; set; }
		public DateTime ProductionStart { get; set; }
		public bool Available { get; set; }
		public byte[] Image { get; set; }
		public virtual Employee Support { get; set; }
		public long? SupportId { get; set; }
		public virtual Employee Engineer { get; set; }
		public long? EngineerId { get; set; }
		public int? CurrentInventory { get; set; }
		public int Backorder { get; set; }
		public int Manufacturing { get; set; }
		public byte[] Barcode { get; set; }
		public virtual Picture PrimaryImage { get; set; }
		public long? PrimaryImageId { get; set; }
		[DataType(DataType.Currency)]
		public decimal Cost { get; set; }
		[DataType(DataType.Currency)]
		public decimal SalePrice { get; set; }
		[DataType(DataType.Currency)]
		public decimal RetailPrice { get; set; }
		public double Weight { get; set; }
		public double ConsumerRating { get; set; }
		public ProductCategory Category { get; set; }
		[InverseProperty("Product")]
		public virtual List<ProductCatalog> Catalog { get; set; }
		[InverseProperty("Product")]
		public virtual List<OrderItem> OrderItems { get; set; }
		public virtual List<ProductImage> Images { get; set; }
		public virtual ICollection<QuoteItem> QuoteItems { get; set; }
		public Stream Brochure {
			get {
				if(Catalog != null && Catalog.Count > 0)
					return Catalog[0].PdfStream;
				return null;
			}
		}
		Image img;
		public Image ProductImage {
			get {
				if(img == null && PrimaryImage != null)
					img = CreateImage(PrimaryImage.Data);
				return img;
			}
		}
		Image CreateImage(byte[] data) {
			if(data == null)
				return ResourceImageHelper.CreateImageFromResourcesEx("DevExpress.DevAV.Resources.Unknown-user.png", typeof(Employee).Assembly);
			else
				return DevExpress.XtraEditors.Controls.ByteImageConverter.FromByteArray(data);
		}
	}
}
