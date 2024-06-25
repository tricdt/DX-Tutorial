using DevExpress.DemoData.Models;
using DevExpress.Utils;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data;
using System.Linq;
using System.Reflection;
using System.Windows;

namespace GridDemo {
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
        static readonly string[] CreditCardNumbers = {
                                                        "4485 5203 4487 8460",
                                                        "4929 8448 6659 9351",
                                                        "4539 1920 8815 9808",
                                                        "4916 5573 0352 8925",
                                                        "4349 1774 2510 4328",
                                                        "5126 0553 5816 2164",
                                                        "5402 9639 5883 2075",
                                                        "5522 0549 0780 3824",
                                                        "5327 0535 5504 5628",
                                                        "5467 8397 9624 7043",
                                                        "4202 8362 8974 5932",
                                                        "4532 7478 9667 0846",
                                                        "4556 5699 5656 1108",
                                                        "4024 0071 7281 2579",
                                                        "4929 5135 1421 5294",
                                                        "5248 7638 5153 8318",
                                                        "5408 5017 0496 8761",
                                                        "5242 5923 9945 0348",
                                                        "5560 8327 0369 4684",
                                                        "5319 2540 0145 7629"
                                                     };
        static readonly Random CreditCardNumberRandom = new Random();

        static Random rnd = new Random(DateTime.Now.Millisecond);

        static DataGenerator() {
            List<string> allFirstNames = new List<string>(FirstNames_Male);
            allFirstNames.AddRange(FirstNames_Female);
            FirstNames = allFirstNames.ToArray();
            List<CategoryData> categoryDataList = OrderDataGenerator.CategoryDataList;
            ProductData = OrderDataGenerator.ExtractProductDataList(categoryDataList).ToArray();
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
        public static decimal? GetNullableDecimal() {
            if(GetIsNull())
                return null;
            return GetDecimal();
        }
        public static decimal GetSalary() {
            return Salary[rnd.Next(0, Salary.Length)];
        }
        public static string GetCreditCardNumber() {
            return CreditCardNumbers[CreditCardNumberRandom.Next(CreditCardNumbers.Length)];
        }
        public static string GetFacebookAddress(string firstName, string lastName) {
            return string.Format("https://www.facebook.com/{0}{1}", firstName.ToLower(), lastName.ToLower());
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
            return new Point(Math.Round(rnd.NextDouble() * 10, 2), Math.Round(rnd.NextDouble() * 10,2));
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
        public static List<object> GetObjects(Type objectType, int count, params string[] skipProperties) {
            List<object> result = new List<object>();
            while(result.Count <= count)
                result.Add(GetNewObject(objectType, skipProperties));
            return result;
        }

        public static object GetNewObject(Type objectType, params string[] skipProperties) {
            object newObject = Activator.CreateInstance(objectType);
            PropertyInfo[] typeInfo = objectType.GetProperties(BindingFlags.Instance | BindingFlags.Public).Where(x => !skipProperties.Contains(x.Name)).ToArray();
            ProductData productData = GetProductData();
            Gender currentGender = GetGender();
            Tuple<string, string> addrAdnCity = GetAddressAndCity();
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
            return NWindContext.Create().Employees.Select(e => e.Title).Distinct().ToList();
        }
        public static List<string> ExtractEmployers() {
            return NWindContext.Create().Customers.Select(c => c.CompanyName).Distinct().ToList();
        }
        public static List<Tuple<string, string>> ExtractAddressAndCity() {
            return NWindContext.Create().Customers
                .Select(c => new { c.Address, c.City })
                .Distinct()
                .ToList()
                .Select(p => Tuple.Create(p.Address, p.City))
                .ToList();
        }
    }
}
