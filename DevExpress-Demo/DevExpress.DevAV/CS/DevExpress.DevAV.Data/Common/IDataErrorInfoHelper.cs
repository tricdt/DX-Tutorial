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

using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Reflection;
namespace DevExpress.Common {
	public static class IDataErrorInfoHelper {
		public static string GetErrorText(object owner, string propertyName) {
			string[] path = propertyName.Split('.');
			if(path.Length > 1)
				return GetErrorText(owner, path);
			PropertyInfo propertyInfo = owner.GetType().GetProperty(propertyName);
			if (propertyInfo == null) return null;
			object propertyValue = propertyInfo.GetValue(owner, null);
			ValidationContext validationContext = new ValidationContext(owner, null, null) { MemberName = propertyName };
			string[] errors = propertyInfo
				.GetCustomAttributes(false)
				.OfType<ValidationAttribute>()
				.Select(x => x.GetValidationResult(propertyValue, validationContext))
				.Where(x => x != null)
				.Select(x => x.ErrorMessage)
				.Where(x => !string.IsNullOrEmpty(x))
				.ToArray();
			return string.Join(" ", errors);
		}
		static string GetErrorText(object owner, string[] path) {
			string nestedPropertyName = string.Join(".", path.Skip(1));
			string propertyName = path[0];
			PropertyInfo propertyInfo = owner.GetType().GetProperty(propertyName);
			if(propertyInfo == null)
				return null;
			object propertyValue = propertyInfo.GetValue(owner, null);
			IDataErrorInfo nestedDataErrorInfo = propertyValue as IDataErrorInfo;
			return nestedDataErrorInfo == null ? string.Empty : nestedDataErrorInfo[nestedPropertyName];
		}
	}
}
