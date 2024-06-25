using DevExpress.DemoData.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using Employee = DevExpress.Xpf.DemoBase.DataClasses.Employee;
using EmployeesWithPhotoData = DevExpress.Xpf.DemoBase.DataClasses.EmployeesWithPhotoData;
using Order = DevExpress.DemoData.Models.Order;

namespace GridDemo {
    public static class EmployeesWithChartSource {
        public static List<EmployeeWithChart> Employees
        {
            get
            {
                var employeesWithChart = new List<EmployeeWithChart>();
                List<Employee> employees = EmployeesWithPhotoData.DataSource;
                Dictionary<int, int> dict = EmployeesWithPhotoData.OrdersRelations;
                NWindContext context = NWindContext.Create();
                List<Order> orders = context.Orders.ToList();
                List<Invoice> invoices = context.Invoices.ToList();
                foreach(Employee empl in employees) {
                    List<ChartPoint> chartPoints = new List<ChartPoint>();
                    foreach(Order order in orders.Where(o => dict[(int)o.OrderID] == empl.Id)) {
                        ChartPoint cp = new ChartPoint();
                        cp.ArgumentMember = order.OrderDate;
                        invoices.Where(invoice => invoice.OrderID == order.OrderID).ToList().ForEach(invoice => cp.ValueMember += Convert.ToInt32(invoice.Quantity * invoice.UnitPrice));
                        chartPoints.Add(cp);
                    }
                    employeesWithChart.Add(new EmployeeWithChart(empl, chartPoints));
                }
                return employeesWithChart;
            }
        }
    }
    public class EmployeeWithChart {
        public EmployeeWithChart(Employee employee, List<ChartPoint> chartSource) {
            ChartSource = chartSource;
            FullName = String.Format("{0} {1}", employee.FirstName, employee.LastName);
            JobTitle = employee.JobTitle;
            CountryRegionName = employee.CountryRegionName;
            BirthDate = employee.BirthDate;
            EmailAddress = employee.EmailAddress;
        }

        public string JobTitle { get; private set; }
        public string CountryRegionName { get; private set; }
        public DateTime BirthDate { get; private set; }
        public string EmailAddress { get; private set; }

        public string FullName { get; private set; }
        public List<ChartPoint> ChartSource { get; private set; }
    }
    public class ChartPoint {
        public DateTime? ArgumentMember { get; internal set; }
        public int ValueMember { get; set; }
    }

}
