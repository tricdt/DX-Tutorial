using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;
using System.Data;
using System.ComponentModel.DataAnnotations;
using System.Reflection;
using System.Windows;
using DevExpress.Utils;
using DevExpress.DemoData.Models;

namespace TreeListDemo {
    public class DataGenerator {
        static Tuple<string, string>[] AddrAdnCity;
        static string[] Groups = new string[] { "Administration", "Inventory", "Manufacturing", "Quality", "Research", "Sales" };
        static string[] Titles;
        static string[] Employers;
        static ProductData[] ProductData;
        static decimal[] Salary = new decimal[] { 5000m, 5500m, 6000m, 6500m, 7000m };
        static string[] FirstNames_Male = new string[] { "Bob", "Michael", "Mike", "Bryan", "Steve", "Alex", "Don", "David", "Jim", "Jo" };
        static string[] FirstNames_Female = new string[] { "Anne", "Sandra", "Samantha" };
        static string[] FirstNames;
        static string[] LastNames = new string[] { "Dodsworth", "Smith", "Miller", "Vargas", "Mares", "Ralls", "Seamans", "Myer", "Moreland", "Walton", "Masters", "Berry", "Hines" };
        static string[] Passwords = new string[] { "password1", "password2", "password3" };
        static char[] Chars = new char[] { 'a', 'b', 'c', 'd', 'A', 'B', 'C', 'D' };
        static string[] Strings = new string[] { "string1", "string2", "string3", "string4" };

        static Random rnd = new Random(DateTime.Now.Millisecond);

        static DataGenerator() {
            List<string> allFirstNames = new List<string>(FirstNames_Male);
            allFirstNames.AddRange(FirstNames_Female);
            FirstNames = allFirstNames.ToArray();
            List<CategoryData> categoryDataList = ExtractCategoryDataList();
            ProductData = ExtractProductDataList(categoryDataList).ToArray();
            Titles = ExtractTitles().ToArray();
            Employers = ExtractEmployers().ToArray();
            AddrAdnCity = ExtractAddressAndCity().ToArray();
        }

        public static string GetFirstName(Gender? gndr = null) {
            if(!gndr.HasValue)
                return FirstNames[rnd.Next(0, FirstNames.Length)];
            Gender value = gndr.Value;
            switch(value) {
                case Gender.Male: return FirstNames_Male[rnd.Next(0, FirstNames_Male.Length)];
                case Gender.Female:
                default:
                    return FirstNames_Female[rnd.Next(0, FirstNames_Female.Length)];
            }
        }
        public static ProductData GetProductData() {
            return ProductData[rnd.Next(0, ProductData.Length)];
        }
        public static string GetLastName() {
            return LastNames[rnd.Next(0, LastNames.Length)];
        }
        public static string GetGroup() {
            return Groups[rnd.Next(0, Groups.Length)];
        }
        public static string GetTitle() {
            return Titles[rnd.Next(0, Titles.Length)];
        }
        public static string GetEmployer() {
            return Employers[rnd.Next(0, Employers.Length)];
        }
        public static string GetPhone() {
            string phone = String.Empty;
            while(phone.Length <= 10)
                phone += rnd.Next(10).ToString();
            return phone;
        }
        public static string GetEmail() {
            return GetString() + "@example.com";
        }
        public static Tuple<string, string> GetAddressAndCity() {
            return AddrAdnCity[rnd.Next(0, AddrAdnCity.Length)];
        }
        public static int? GetNullableInt() {
            if(GetIsNull())
                return null;
            return GetInt();
        }
        public static int GetInt() {
            return rnd.Next(1, 100);
        }
        static int id = 0;
        static int GetTreeID() {
            int currentId = id + 1;
            id++;
            return currentId;
        }
        public static Tuple<int, int> GetID() {
            int id = GetTreeID();
            int parentID = id % 4;
            return new Tuple<int, int>(id, parentID);
        }

        public static decimal? GetNullableDecimal() {
            if(GetIsNull())
                return null;
            return GetDecimal();
        }
        public static decimal GetSalary() {
            return Salary[rnd.Next(0, Salary.Length)];
        }
        public static decimal GetDecimal() {
            return new decimal(GetDouble());
        }
        public static double? GetNullableDouble() {
            if(GetIsNull())
                return null;
            return GetDouble();
        }
        public static double GetDouble() {
            return Math.Round(rnd.NextDouble() * 100, 2);
        }
        static bool GetIsNull() {
            return rnd.Next(0, 10) < 3;
        }
        static string GetSSN() {
            string ssn = String.Empty;
            while(ssn.Length <= 11) {
                if(ssn.Length == 3 || ssn.Length == 6)
                    ssn += "-";
                else
                    ssn += rnd.Next(10).ToString();
            }
            return ssn;
        }
        public static Point GetPoint() {
            return new Point(Math.Round(rnd.NextDouble() * 10, 2), Math.Round(rnd.NextDouble() * 10, 2));
        }
        public static string GetMultiLineString() {
            return "Line 1.\nLine 2.";
        }
        public static int GetAge() {
            return rnd.Next(30, 50);
        }
        public static string GetString() {
            return Strings[rnd.Next(0, Strings.Length)];
        }
        public static string GetPassword() {
            return Passwords[rnd.Next(0, Passwords.Length)];
        }
        public static bool? GetNullableBool() {
            if(GetIsNull())
                return null;
            return GetBool();
        }
        public static bool GetBool() {
            return rnd.Next(2) == 1;
        }
        public static char? GetNullableChar() {
            if(GetIsNull())
                return null;
            return GetChar();
        }
        public static char GetChar() {
            return Chars[rnd.Next(0, Chars.Length)];
        }
        public static object GetNullableEnumValue(Type enumType) {
            if(GetIsNull())
                return null;
            return GetEnumValue(enumType);
        }
        public static object GetEnumValue(Type enumType) {
            Array values = EnumExtensions.GetValues(enumType);
            return values.GetValue(rnd.Next(0, values.Length));
        }
        public static Gender GetGender() {
            return (Gender)GetEnumValue(typeof(Gender));
        }
        public static DateTime? GetNullableDateTime() {
            if(GetIsNull())
                return null;
            return GetDateTime();
        }
        public static DateTime GetDateTime() {
            return DateTime.Now - TimeSpan.FromDays(rnd.Next(60));
        }
        public static DateTime GetHireDate() {
            return DateTime.Now - TimeSpan.FromDays(rnd.Next(60, 365 * 5));
        }
        public static DateTime GetBirthDate() {
            return DateTime.Now - TimeSpan.FromDays(GetAge() * rnd.Next(300, 400));
        }
        public static List<object> GetObjects(Type objectType, int count) {
            id = 0;
            List<object> result = new List<object>();
            while(result.Count <= count)
                result.Add(GetNewObject(objectType));
            return result;
        }

        public static object GetNewObject(Type objectType) {
            object newObject = Activator.CreateInstance(objectType);
            PropertyInfo[] typeInfo = objectType.GetProperties(BindingFlags.Instance | BindingFlags.Public);
            ProductData productData = GetProductData();
            Gender currentGender = GetGender();
            Tuple<string, string> addrAdnCity = GetAddressAndCity();
            Tuple<int, int> id = GetID();
            foreach(PropertyInfo info in typeInfo) {
                if(!info.CanWrite)
                    continue;
                if(info.PropertyType == typeof(Gender)) {
                    info.SetValue(newObject, currentGender, null);
                    continue;
                }
                DataTypeAttribute dType = GetDataTypeAttribute(info);
                if(dType != null) {
                    switch(dType.DataType) {
                        case DataType.MultilineText: info.SetValue(newObject, GetMultiLineString(), null);
                            continue;
                        case DataType.Password: info.SetValue(newObject, GetPassword(), null);
                            continue;
                        case DataType.PhoneNumber: info.SetValue(newObject, GetPhone(), null);
                            continue;
                        case DataType.Currency:
                            if(info.Name == "Salary") {
                                info.SetValue(newObject, GetSalary(), null);
                                continue;
                            }
                            info.SetValue(newObject, GetDecimal(), null);
                            continue;
                    }
                }
                if(info.PropertyType.IsEnum) {
                    info.SetValue(newObject, GetEnumValue(info.PropertyType), null);
                    continue;
                }
                if(IsNullableType(info.PropertyType) && GetNullableType(info.PropertyType).IsEnum) {
                    info.SetValue(newObject, GetNullableEnumValue(GetNullableType(info.PropertyType)), null);
                    continue;
                }
                bool nullable = false;
                if(IsPropertyOfType(info, typeof(int), out nullable)) {
                    if(info.Name == "Age") {
                        info.SetValue(newObject, GetAge(), null);
                        continue;
                    }
                    if(info.Name == "ID") {
                        info.SetValue(newObject, id.Item1, null);
                        continue;
                    }
                    if(info.Name == "ParentID") {
                        info.SetValue(newObject, id.Item2, null);
                        continue;
                    }
                    info.SetValue(newObject, nullable ? GetNullableInt() : GetInt(), null);
                    continue;
                }
                if(info.PropertyType == typeof(string)) {
                    if(info.Name == "FirstName") {
                        info.SetValue(newObject, GetFirstName(currentGender), null);
                        continue;
                    }
                    if(info.Name == "LastName") {
                        info.SetValue(newObject, GetLastName(), null);
                        continue;
                    }
                    if(info.Name == "SSN") {
                        info.SetValue(newObject, GetSSN(), null);
                        continue;
                    }
                    if(info.Name == "Phone" || info.Name == "PhoneNumberProperty") {
                        info.SetValue(newObject, GetPhone(), null);
                        continue;
                    }
                    if(info.Name == "Email") {
                        info.SetValue(newObject, GetEmail(), null);
                        continue;
                    }
                    if(info.Name == "Address") {
                        info.SetValue(newObject, addrAdnCity.Item1, null);
                        continue;
                    }
                    if(info.Name == "City") {
                        info.SetValue(newObject, addrAdnCity.Item2, null);
                        continue;
                    }
                    if(info.Name == "ProductName") {
                        info.SetValue(newObject, productData.Name, null);
                        continue;
                    }
                    if(info.Name == "CustomerName") {
                        info.SetValue(newObject, GetFirstName() + " " + GetLastName(), null);
                        continue;
                    }
                    if(info.Name == "Group") {
                        info.SetValue(newObject, GetGroup(), null);
                        continue;
                    }
                    if(info.Name == "Title") {
                        info.SetValue(newObject, GetTitle(), null);
                        continue;
                    }
                    if(info.Name == "Employer") {
                        info.SetValue(newObject, GetEmployer(), null);
                        continue;
                    }
                    if(info.Name == "PasswordProperty") {
                        info.SetValue(newObject, GetPassword(), null);
                        continue;
                    }
                    if(info.Name == "MultilineTextProperty") {
                        info.SetValue(newObject, GetMultiLineString(), null);
                        continue;
                    }
                    info.SetValue(newObject, GetString(), null);
                    continue;
                }
                if(IsPropertyOfType(info, typeof(DateTime), out nullable)) {
                    if(info.Name == "BirthDate") {
                        info.SetValue(newObject, GetBirthDate(), null);
                        continue;
                    }
                    if(info.Name == "HireDate") {
                        info.SetValue(newObject, GetHireDate(), null);
                        continue;
                    }
                    info.SetValue(newObject, nullable ? GetNullableDateTime() : GetDateTime(), null);
                    continue;
                }
                if(IsPropertyOfType(info, typeof(bool), out nullable)) {
                    info.SetValue(newObject, nullable ? GetNullableBool() : GetBool(), null);
                    continue;
                }
                if(IsPropertyOfType(info, typeof(double), out nullable)) {
                    info.SetValue(newObject, nullable ? GetNullableDouble() : GetDouble(), null);
                    continue;
                }
                if(IsPropertyOfType(info, typeof(decimal), out nullable)) {
                    if(info.Name == "Price") {
                        info.SetValue(newObject, productData.Price, null);
                        continue;
                    }
                    if(info.Name == "Salary") {
                        info.SetValue(newObject, GetSalary(), null);
                        continue;
                    }
                    info.SetValue(newObject, nullable ? GetNullableDecimal() : GetDecimal(), null);
                    continue;
                }
                if(IsPropertyOfType(info, typeof(char), out nullable)) {
                    info.SetValue(newObject, nullable ? GetNullableChar() : GetChar(), null);
                    continue;
                }
                if(info.PropertyType == typeof(Point)) {
                    info.SetValue(newObject, GetPoint(), null);
                    continue;
                }
                if(info.PropertyType == typeof(CategoryData)) {
                    info.SetValue(newObject, productData.Category, null);
                    continue;
                }
            }
            return newObject;
        }
        static bool IsPropertyOfType(PropertyInfo info, Type targetType, out bool nullable) {
            nullable = IsNullableType(info.PropertyType);
            var type = nullable ? GetNullableType(info.PropertyType) : info.PropertyType;
            return object.Equals(type, targetType);
        }
        static bool IsNullableType(Type propertyType) {
            
            return propertyType.IsGenericType && propertyType.GetGenericTypeDefinition().Name.Contains("Nullable");
        }
        static Type GetNullableType(Type propertyType) {
            return propertyType.GetGenericArguments()[0];
        }

        static DataTypeAttribute GetDataTypeAttribute(PropertyInfo info) {
            foreach(object attr in info.GetCustomAttributes(false)) {
                DataTypeAttribute result = attr as DataTypeAttribute;
                if(result != null)
                    return result;
            }
            return null;
        }

        public static List<string> ExtractTitles() {
            List<string> titles = new List<string>();
            var employees = DevExpress.DemoData.NWindDataProvider.Employees;
            foreach(Employee employee in employees) {
                string title = employee.Title;
                if(!titles.Contains(title))
                    titles.Add(title);
            }
            return titles;
        }
        public static List<string> ExtractEmployers() {
            List<string> employers = new List<string>();
            var customers = NWindContext.Create().Customers.ToList();
            foreach(Customer customer in customers) {
                string employer = customer.CompanyName;
                if(!employers.Contains(employer))
                    employers.Add(employer);
            }
            return employers;
        }
        public static List<Tuple<string, string>> ExtractAddressAndCity() {
            List<Tuple<string, string>> addrList = new List<Tuple<string, string>>();
            var customers = NWindContext.Create().Customers.ToList();
            foreach(Customer customer in customers) {
                var addr = Tuple.Create(customer.Address, customer.City);
                if(!addrList.Contains(addr))
                    addrList.Add(addr);
            }
            return addrList;
        }
        public static List<CategoryData> ExtractCategoryDataList() {
            List<CategoryData> categoryData = new List<CategoryData>();
            var categories = NWindContext.Create().Categories.ToList();
            foreach(Category category in categories) {
                categoryData.Add(new CategoryData() {
                    Name = category.CategoryName,
                    Picture = category.Picture
                });
            }
            return categoryData;
        }
        public static List<ProductData> ExtractProductDataList(List<CategoryData> categoriesList) {
            List<ProductData> productData = new List<ProductData>();

            var categoryProducts = NWindContext.Create().CategoryProducts.ToList();
            Random rand = new Random();
            foreach(CategoryProduct cp in categoryProducts) {
                productData.Add(new ProductData() {
                    Category = FindCategory(categoriesList, cp.CategoryName),
                    Name = cp.ProductName,
                    Price = (decimal)rand.Next(20) + (decimal)rand.Next(99) / 100m
                });
            }
            return productData;
        }
        static CategoryData FindCategory(List<CategoryData> categoriesList, string name) {
            foreach(CategoryData category in categoriesList) {
                if(category.Name == name) return category;
            }
            return null;
        }
    }

    public class ProductData {
        public string Name { get; set; }
        public CategoryData Category { get; set; }
        public decimal Price { get; set; }
        public override string ToString() {
            return Name;
        }
    }
    public class CategoryData : IComparable, IComparable<CategoryData> {
        public string Name { get; set; }
        public byte[] Picture { get; set; }
        public override string ToString() {
            return Name;
        }

        #region IComparable Members
        public int CompareTo(object obj) {
            if(obj is CategoryData)
                return CompareTo((CategoryData)obj);
            return -1;
        }
        #endregion
        #region IComparable<CategoryData> Members
        public int CompareTo(CategoryData other) {
            return StringComparer.CurrentCulture.Compare(Name, other.Name);
        }
        #endregion
    }
}
