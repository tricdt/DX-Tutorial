using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Xml;
using Task = ProductsDemo.Modules.Task;
using DevExpress.DevAV;
using DevExpress.Xpf.Core.Native;
using System.Windows;
using System.ComponentModel;

namespace ProductsDemo.Modules {
    public class DataBaseHelper : DependencyObject {
        static DataBaseHelper instanse = null;
        List<Employee> employees;

        static public DataBaseHelper Instance {
            get {
                if(instanse == null) {
                    instanse = new DataBaseHelper();
                }
                return instanse;
            }
        }
        DataBaseHelper() {
            GetEmployees();
            Contacts = FillContacts();
            FillAppointments();
            Tasks = FillTasks();
        }

        List<Task> FillTasks() {
            List<Task> ret = new List<Task>();
            for(int i = 0; i < TaskGenerator.CustomerCount; i++)
                foreach(string s in CollectionResources.OfficeTasks)
                    ret.Add(TaskGenerator.CreateTask(s, TaskCategory.Work));
            foreach(string s in CollectionResources.HouseTasks)
                ret.Add(TaskGenerator.CreateTask(s, TaskCategory.HouseChores));
            foreach(string s in CollectionResources.ShoppingTasks)
                ret.Add(TaskGenerator.CreateTask(s, TaskCategory.Shopping));
            return ret;
        }
        List<Contact> FillContacts() {
            var result = new List<Contact>();
            employees.ForEach(e => {
                var contact = CreateContact(e);
                FillPersonInformation(contact, e);
                result.Add(contact);
            });
            return result;
        }
        Contact CreateContact(Employee employee) {
            var contact = new Contact();
            contact.Name = new Name();
            contact.Name.MiddleName = string.Empty;
            contact.Email = employee.Email;
            contact.Address = new Address(employee.Address.ToString());
            contact.Phone = employee.MobilePhone;
            contact.Activity = GetContactActivity(employee.Id);
            if(employee.Picture != null)
                contact.Photo = ImageHelper.CreateImageFromStream(new MemoryStream(employee.Picture.Data as byte[]));
            return contact;
        }
        List<int> GetContactActivity(long employeeId) {
            List<int> activity = new List<int>();
            Random rnd = new Random((int)employeeId);
            for(int i = 0; i < 6; i++) {
                activity.Add(rnd.Next(0, 10));
            }
            return activity;
        }
        static void FillPersonInformation(Contact contact, Employee employee) {
            contact.Name.FirstName = employee.FirstName;
            contact.Name.LastName = employee.LastName;
            contact.Prefix = employee.Prefix;
            contact.BirthDate = employee.BirthDate.Value;
        }
        void FillAppointments() {
            Appointments = new ObservableCollection<Appointment>();
            DataTable appointmentsTable = CreateDataTable("Appointments");
            if(appointmentsTable != null && appointmentsTable.Rows.Count > 0) {
                foreach(DataRow row in appointmentsTable.Rows)
                    Appointments.Add(CreateAppointment(row));
            }
        }
        DataTable CreateDataTable(string table) {
            DataSet dataSet = new DataSet();
            try {
                string dataFile = Utils.GetRelativePath("MailDevAv.xml");
                if(dataFile != string.Empty) {
                    FileInfo fi = new FileInfo(dataFile);
                    dataSet.ReadXml(fi.FullName);
                    return dataSet.Tables[table];
                }
            } catch(Exception) { }
            return null;
        }
        Appointment CreateAppointment(DataRow row) {
            Appointment appointment = new Appointment();
            appointment.EventType = (int?)row["EventType"];
            appointment.StartDate = (DateTime?)row["StartDate"];
            appointment.EndDate = (DateTime?)row["EndDate"];
            appointment.AllDay = (bool?)row["AllDay"];
            appointment.Subject = Convert.ToString(row["Subject"]);
            appointment.Location = Convert.ToString(row["Location"]);
            appointment.Description = Convert.ToString(row["Description"]);
            appointment.Status = (int?)row["Status"];
            appointment.Label = (int?)row["Label"];
            appointment.RecurrenceInfo = Convert.ToString(row["RecurrenceInfo"]);
            appointment.ReminderInfo = Convert.ToString(row["ReminderInfo"]);
            appointment.ContactInfo = Convert.ToString(row["ContactInfo"]);
            return appointment;
        }

        public List<Contact> Contacts { get; private set; }
        public List<Task> Tasks { get; private set; }
        public ObservableCollection<Appointment> Appointments { get; private set; }

        void GetEmployees() {
            employees = IsInDesignMode() ? new List<Employee>() : DevAVHelper.Employees;
        }
        bool IsInDesignMode() {
            return DesignerProperties.GetIsInDesignMode(this);
        }
    }

    public static class DevAVHelper {
        static List<Employee> employees = null;
        static ImageSource unknownUserPicture;

        internal static List<Employee> Employees {
            get {
                if(employees == null) {
                    DevAVDb devAvDb = new DevAVDb();
                    devAvDb.Employees.Load();
                    employees = devAvDb.Employees.Local.ToList();
                }
                return employees;
            }
        }
        internal static ImageSource UnknownUserPicture {
            get {
                if(unknownUserPicture == null) {
                    unknownUserPicture = new BitmapImage(GetAppImageUri("Contacts/Unknown-user", UriKind.RelativeOrAbsolute));
                    return unknownUserPicture;
                }
                return unknownUserPicture;
            }
        }
        public static string GetNameByEmail(string mail, bool isDesignMode = false) {
            if(string.IsNullOrEmpty(mail) || isDesignMode)
                return string.Empty;
            var employee = Employees.FirstOrDefault(p => p.Email == mail);
            return employee == null ? string.Empty : employee.FullName;
        }
        public static ImageSource GetPictureByEmail(string mail, bool isDesignMode = false) {
            if(string.IsNullOrEmpty(mail) || isDesignMode)
                return UnknownUserPicture;
            var employee = Employees.FirstOrDefault(p => p.Email == mail);
            if(employee != null && employee.Picture != null && employee.Picture.Data != null)
                return ImageHelper.CreateImageFromStream(new MemoryStream(employee.Picture.Data as byte[]));
            return UnknownUserPicture;
        }
        static Uri GetAppImageUri(string path, UriKind uriKind = UriKind.Relative) {
            return new Uri(string.Format("/DevExpress.ProductsDemo.Xpf;component/Images/{0}.png", path), uriKind);
        }
    }
    class TaskGenerator {
        public static int CustomerCount = 10;
        static Random rndGenerator = new Random();
        static List<Contact> customers;
        internal static List<Contact> Customers {
            get {
                if(customers == null) {
                    customers = new List<Contact>();
                    List<Contact> temp = DataBaseHelper.Instance.Contacts;
                    if(temp.Count > CustomerCount) {
                        while(customers.Count < CustomerCount) {
                            Contact contact = GetCustomer(rndGenerator.Next(temp.Count - 1), customers, temp);
                            if(contact != null)
                                customers.Add(contact);
                        }
                    }
                }
                return customers;
            }
        }
        static Contact GetCustomer(int index, List<Contact> customers, List<Contact> contacts) {
            Contact contact = contacts[index];
            foreach(Contact c in customers)
                if(ReferenceEquals(c, contact)) return null;
            return contact;
        }
        public static Task CreateTask(string subject, TaskCategory category) {
            Task task = new Task(subject, category, DateTime.Now.AddHours(-rndGenerator.Next(96)));
            int rndStatus = rndGenerator.Next(10);
            if(task.TimeDiff.TotalHours > 12) {
                if(task.TimeDiff.TotalHours > 80) {
                    task.Status = TaskStatus.Completed;

                } else {
                    task.Status = TaskStatus.InProgress;
                    task.PercentComplete = rndGenerator.Next(9) * 10;
                }
                task.StartDate = task.CreatedDate.AddMinutes(rndGenerator.Next(720)).Date;
            }
            if(rndStatus != 5) task.DueDate = task.CreatedDate.AddHours((90 - rndStatus * 9) + 24).Date;
            if(rndStatus > 8) task.Priority = TaskPriority.High;
            if(rndStatus < 3) task.Priority = TaskPriority.Low;
            if(rndStatus == 6 && task.Status == TaskStatus.InProgress) task.Status = TaskStatus.Deferred;
            if(rndStatus == 4 && task.Status == TaskStatus.InProgress && task.PercentComplete < 40) task.Status = TaskStatus.WaitingOnSomeoneElse;
            if(task.Category == TaskCategory.Work && rndStatus != 7 && Customers.Count > 0)
                task.AssignTo = Customers[rndGenerator.Next(Customers.Count)];
            if(task.Status == TaskStatus.Completed) {
                if(task.StartDate == null) task.StartDate = task.CreatedDate.AddHours(12).Date;
                task.CompletedDate = task.StartDate.Value.AddHours(rndGenerator.Next(48) + 24);
            }
            return task;
        }
    }

    public class CollectionResources {
        public static List<string> HouseTasks = new List<string>() {
            "Set-up home theater (surround sound) system",
            "Install 3 overhead lights in bedroom",
            "Change light bulbs in backyard",
            "Install a programmable thermostat",
            "Install ceiling fan in John's bedroom",
            "Install LED lights in kitchen",
            "Check wiring in main electricity panel",
            "Replace master bedroom light switch with dimmer",
            "Install an new electric outlet in garage",
            "Install electric outlet and ethernet drop in closet",
            "Install chandelier in dining room",
            "Hook up DVD Player to TV for kids",
            "Clean the House top to bottom",
            "Light cleaning of the house",
            "Clean the entire house",
            "Clean and organize basement",
            "Pick up clothes for charity event",
            "Ironing, laundry and vacuuming",
            "Take kids to park and play baseball on Sunday",
            "Clean art studio",
            "Bake brownies and send them to neighbors",
            "Assemble Kitchen Cart",
            "Move piano",
            "Clean backyard",
            "Clean out garage",
            "Organize guest bedroom",
            "Clean out closet",
            "Preapre for yard sale",
            "Sorting clothing for give-away",
            "Organize Storage Room"};
        public static List<string> ShoppingTasks = new List<string>() {
            "Shopping at Macy's",
            "Return coffee machine",
            "Purchase plastic trash bins",
            "Shop for dinner ingredients at the store",
            "Buy new utensils for kitchen",
            "Send post card to Timothy",
            "Buy dining table and TV stand online",
            "Buy ingredients for Pasta Bolognese",
            "Size 3 diapers (3 cases)",
            "Order 3 pizzas",
            "Find out where to buy the new tablet",
            "Buy broccoli and tomatoes",
            "Buy bottle of Champagne",
            "Grocery shopping at Market Basket",
            "Find a bike at a store close to me",
            "Return jeans at JCrew",
            "Buy dog food for Fido",
            "Buy rigid foam insulation",
            "Purchase 3 24-packs of bottled Coke",
            "Purchase & deliver flowers to my home"};
        public static List<string> OfficeTasks = new List<string>() {
            "Input bank statement transactions into Excel spreadsheet",
            "Schedule appointments and pay bills",
            "Place new address stickers on envelopes",
            "Set up and arrange appointments",
            "Copy PDF file into Word",
            "Organize business expenses (last 6 months)",
            "Return samples to vendor",
            "Organize receipts and match them up with business expenses and trips ",
            "File papers and receipts",
            "Ship out CDs to customers",
            "Respond to e-mails until noon",
            "Enter expenses into an online accounting system",
            "Conduct inventory of all furniture in office",
            "Arrange travel to conference",
            "Staple flyers to gift bags",
            "File and shred mail",
            "Print copies of brochures",
            "Enter all receipts into an Excel spreadsheet",
            "Research possible vendors",
            "Sort through paper receipts",
            "Re-package products for retail sale",
            "Scan docs, and put in desktop folder",
            "Print registration stickers"};
    }
}
