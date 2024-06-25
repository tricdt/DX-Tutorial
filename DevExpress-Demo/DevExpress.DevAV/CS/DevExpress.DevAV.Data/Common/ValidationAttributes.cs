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
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Globalization;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Reflection.Emit;
using System.Resources;
using System.Security;
using System.Text;
using System.Text.RegularExpressions;
namespace DevExpress.DataAnnotations {
	public abstract class RegexAttributeBase : DataTypeAttribute {
		protected const RegexOptions DefaultRegexOptions = RegexOptions.Compiled | RegexOptions.ExplicitCapture | RegexOptions.IgnoreCase;
		readonly Regex regex;
		public RegexAttributeBase(string regex, string defaultErrorMessage, DataType dataType)
			: this(new Regex(regex, DefaultRegexOptions), defaultErrorMessage, dataType) {
		}
		public RegexAttributeBase(Regex regex, string defaultErrorMessage, DataType dataType)
			: base(dataType) {
			this.regex = (Regex)regex;
			this.ErrorMessage = defaultErrorMessage;
		}
		public sealed override bool IsValid(object value) {
			if(value == null)
				return true;
			string input = value as string;
			return input != null && regex.Match(input).Length > 0;
		}
	}
	public sealed class ZipCodeAttribute : RegexAttributeBase {
		static Regex regex = new Regex(@"^[0-9][0-9][0-9][0-9][0-9]$", DefaultRegexOptions);
		const string Message = "The {0} field is not a valid ZIP code.";
		public ZipCodeAttribute()
			: base(regex, Message, DataType.Url) {
		}
	}
	public sealed class CreditCardAttribute : DataTypeAttribute {
		const string Message = "The {0} field is not a valid credit card number.";
		public CreditCardAttribute()
			: base(DataType.Custom) {
			this.ErrorMessage = Message;
		}
		public override bool IsValid(object value) {
			if(value == null)
				return true;
			string stringValue = value as string;
			if(stringValue == null)
				return false;
			stringValue = stringValue.Replace("-", "").Replace(" ", "");
			int number = 0;
			bool oddEvenFlag = false;
			foreach(char ch in stringValue.Reverse()) {
				if(ch < '0' || ch > '9')
					return false;
				int digitValue = (ch - '0') * (oddEvenFlag ? 2 : 1);
				oddEvenFlag = !oddEvenFlag;
				while(digitValue > 0) {
					number += digitValue % 10;
					digitValue = digitValue / 10;
				}
			}
			return (number % 10) == 0;
		}
	}
}
