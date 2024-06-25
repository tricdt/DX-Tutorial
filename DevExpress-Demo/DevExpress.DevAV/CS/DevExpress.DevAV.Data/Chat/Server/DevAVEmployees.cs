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

namespace DevExpress.DevAV.Chat {
	using System;
	using System.Collections.Generic;
	using System.Linq;
	using System.Threading.Tasks;
	using DevExpress.DevAV;
	using DevExpress.DevAV.Chat.Model;
#if DXCORE3
	using Microsoft.EntityFrameworkCore;
#else
	using System.Data.Entity;
#endif
	partial class DevAVEmpployeesInMemoryServer {
		readonly Func<DevAVDb> createDb;
		[CLSCompliant(false)]
		public DevAVEmpployeesInMemoryServer(Func<DevAVDb> dbFactory) {
			this.createDb = dbFactory ?? new Func<DevAVDb>(() => new DevAVDb());
		}
		partial class Channel {
			readonly Func<DevAVDb> createDb;
			public async Task<UserInfo> GetUserInfo(long id) {
				return new UserInfo(await LoadEmployee(id));
			}
			public async Task<UserInfo> GetUserInfo(string userName) {
				return new UserInfo(await LoadEmployee(userName));
			}
			DevAVDb CreateDevAvDb() {
				return createDb();
			}
			readonly Dictionary<long, Employee> cacheById = new Dictionary<long, Employee>();
			async Task<Employee> LoadEmployee(long id) {
				Employee employee;
				if(!cacheById.TryGetValue(id, out employee)) {
					using(DevAVDb devAvDb = CreateDevAvDb()) {
						await devAvDb.Employees.Where(e => e.Id == id)
							.Include(x => x.Picture)
							.LoadAsync();
						employee = devAvDb.Employees.Local.FirstOrDefault();
					}
					AddToCache(employee, id: id);
				}
				return employee;
			}
			readonly Dictionary<string, Employee> cacheByName = new Dictionary<string, Employee>();
			async Task<Employee> LoadEmployee(string userName) {
				Employee employee;
				if(!cacheByName.TryGetValue(userName, out employee)) {
					using(DevAVDb devAvDb = CreateDevAvDb()) {
						await devAvDb.Employees.Where(e => e.FullName == userName)
							.Include(x => x.Picture)
							.LoadAsync();
						employee = devAvDb.Employees.Local.FirstOrDefault();
					}
					AddToCache(employee, userName: userName);
				}
				return employee;
			}
			void AddToCache(Employee employee, long? id = null, string userName = null) {
				if(id != null || !cacheById.ContainsKey(employee.Id))
					cacheById.Add(id.GetValueOrDefault(employee.Id), employee);
				if(userName != null && cacheByName.ContainsKey(employee.FullName))
					cacheByName.Add(userName ?? employee.FullName, employee);
			}
			async Task<IReadOnlyList<Employee>> LoadEmployeeContacts() {
				using(DevAVDb devAvDb = CreateDevAvDb()) {
					int employeesCount = await devAvDb.Employees.AsNoTracking()
						.CountAsync().ConfigureAwait(false);
					var ids = DataGenerator.GetRandomIds(employeesCount).ToList();
					await devAvDb.Employees.Where(e => ids.Contains(e.Id) && e.FullName != UserName)
						.Include(x => x.Picture)
						.LoadAsync();
					return devAvDb.Employees.Local.ToList();
				}
			}
		}
	}
}
