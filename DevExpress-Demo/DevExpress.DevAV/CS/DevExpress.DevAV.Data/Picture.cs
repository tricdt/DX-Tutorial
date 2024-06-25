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

using System.ComponentModel.DataAnnotations.Schema;
using System.Drawing;
using DevExpress.Utils;
using DevExpress.XtraEditors.Controls;
using System.Runtime.Serialization;
using System.Collections.Generic;
namespace DevExpress.DevAV {
	public class Picture : DatabaseObject {
		public byte[] Data { get; set; }
		public virtual ICollection<Employee> Employees { get; set; }
		public virtual ICollection<CustomerEmployee> CustomerEmployees { get; set; }
		public virtual ICollection<Product> Products { get; set; }
		public virtual ICollection<ProductImage> ProductImages { get; set; }
	}
	static class PictureExtension {
		public const string DefaultPic = DefaultUserPic;
		public const string DefaultUserPic = "DevExpress.DevAV.Resources.Unknown-user.png";
		internal static Image CreateImage(this Picture picture, string defaultImage = null) {
			if(picture == null) {
				if(string.IsNullOrEmpty(defaultImage))
					defaultImage = DefaultPic;
				return ResourceImageHelper.CreateImageFromResourcesEx(defaultImage, typeof(Picture).Assembly);
			}
			else return ByteImageConverter.FromByteArray(picture.Data);
		}
		internal static Picture FromImage(Image image) {
			return (image == null) ? null : new Picture()
			{
				Data = ByteImageConverter.ToByteArray(image, image.RawFormat)
			};
		}
	}
}
