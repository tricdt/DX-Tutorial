using DevExpress.DemoData.Models;
using DevExpress.Mvvm;
using DevExpress.Mvvm.POCO;
using DevExpress.Utils.Text;
using DevExpress.Xpf.Scheduling;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

namespace SchedulingDemo {
    public class Patient {
        public static Patient Create() {
            return ViewModelSource.Create(() => new Patient());
        }
        protected Patient() { }

        public virtual int Id { get; set; }
        public virtual string Name { get; set; }
        public virtual DateTime BirthDate { get; set; }
        public virtual string Phone { get; set; }
    }
    public class HospitalDepartment {
        public static HospitalDepartment Create(int id, string name) {
            return ViewModelSource.Create(() => new HospitalDepartment() {
                Id = id,
                Name = name
            });
        }
        protected HospitalDepartment() {
            Doctors = new ObservableCollection<Doctor>();
        }

        public virtual int Id { get; set; }
        public virtual string Name { get; set; }
        public ObservableCollection<Doctor> Doctors { get; private set; }
    }
    public class Doctor {
        public static Doctor Create() {
            return ViewModelSource.Create(() => new Doctor());
        }
        public static Doctor Create(Employee employee) {
            return ViewModelSource.Create(() => new Doctor() {
                Id = (int)employee.EmployeeID,
                Name = employee.FullName,
                Phone = employee.HomePhone,
                Photo = employee.Photo
            });
        }
        protected Doctor() { }

        public virtual int Id { get; set; }
        public virtual string Name { get; set; }
        public virtual string Phone { get; set; }
        public virtual object Photo { get; set; }
        public virtual HospitalDepartment Department { get; set; }
        public virtual bool IsVisible { get; set; }
    }
    public class AppointmentLocation {
        public static AppointmentLocation Create(int id, string caption) {
            return ViewModelSource.Create(() => new AppointmentLocation() {
                Id = id,
                Caption = caption
            });
        }
        protected AppointmentLocation() { }
        public virtual int Id { get; set; }
        public virtual string Caption { get; set; }
    }
    public class PaymentState {
        public static PaymentState Create(int id, string caption, string brushName) {
            return ViewModelSource.Create(() => new PaymentState() {
                Id = id,
                Caption = caption,
                BrushName = brushName,
            });
        }
        protected PaymentState() { }
        public virtual int Id { get; set; }
        public virtual string Caption { get; set; }
        public virtual string BrushName { get; set; }
    }
    public class MedicalAppointment {
        public static MedicalAppointment Create() {
            return ViewModelSource.Create(() => new MedicalAppointment());
        }
        protected MedicalAppointment() { }
        public virtual int Id { get; set; }
        public virtual int Type { get; set; }
        public virtual bool AllDay { get; set; }
        public virtual DateTime StartTime { get; set; }
        public virtual DateTime EndTime { get; set; }
        public virtual int? PatientId { get; set; }
        public virtual int? DoctorId { get; set; }
        public virtual int LocationId { get; set; }
        public virtual int PaymentStateId { get; set; }
        public virtual string Subject { get; set; }
        public virtual string Purpose { get; set; }
        public virtual string Note { get; set; }
        public virtual string RecurrenceInfo { get; set; }
        public virtual string ReminderInfo { get; set; }
        public virtual string TimeZoneId { get; set; }
    }

    public static class ReceptionDeskData {
        public static readonly DateTime BaseDate = DateTime.Today;
        public static ObservableCollection<Patient> Patients { get; set; }
        public static ObservableCollection<HospitalDepartment> Departments { get; set; }
        public static ObservableCollection<Doctor> Doctors { get; set; }
        public static ObservableCollection<AppointmentLocation> AppointmentLocations { get; set; }
        public static ObservableCollection<PaymentState> PaymentStates { get; set; }
        public static ObservableCollection<MedicalAppointment> Appointments { get; set; }
        public static HospitalDepartment DepartmentPrimaryCare { get; set; }
        public static HospitalDepartment DepartmentOphthalmology { get; set; }
        public static HospitalDepartment DepartmentDermatology { get; set; }
        public static HospitalDepartment DepartmentCardiology { get; set; }
        static AppointmentLocation AppointmentLocationClinic { get; set; }
        static AppointmentLocation AppointmentLocationHospital { get; set; }
        static AppointmentLocation AppointmentLocationHospice { get; set; }
        static AppointmentLocation AppointmentLocationHouseCall { get; set; }
        internal static PaymentState PaymentStatePaid { get; private set; }
        internal static PaymentState PaymentStateOverdue { get; private set; }
        internal static PaymentState PaymentStateNotYetBilled { get; private set; }
        static Dictionary<AppointmentLocation, Tuple<TimeSpan, string>[]> DepartmentPrimaryCare_DurationAndPurposes { get; set; }
        static Dictionary<AppointmentLocation, Tuple<TimeSpan, string>[]> DepartmentOphthalmology_DurationAndPurposes { get; set; }
        static Dictionary<AppointmentLocation, Tuple<TimeSpan, string>[]> DepartmentDermatology_DurationAndPurposes { get; set; }
        static Dictionary<AppointmentLocation, Tuple<TimeSpan, string>[]> DepartmentCardiology_DurationAndPurposes { get; set; }

        static readonly Random random;
        static ReceptionDeskData() {
            random = new Random();
            if(ViewModelBase.IsInDesignMode) {
                Patients = new ObservableCollection<Patient>();
                Departments = new ObservableCollection<HospitalDepartment>();
                Doctors = new ObservableCollection<Doctor>();
                AppointmentLocations = new ObservableCollection<AppointmentLocation>();
                PaymentStates = new ObservableCollection<PaymentState>();
                Appointments = new ObservableCollection<MedicalAppointment>();
                return;
            }
            CreatePatients();
            CreateDepartments();
            CreateDoctors();
            CreateAppointmentLocations();
            CreatePaymentStates();
            CreateDurationAndPurposes();
            CreateAppointments();
        }
        static void CreatePatients() {
            string[] PatientNames = { "Andrew Glover", "Mark Oliver", "Taylor Riley", "Addison Davis", "Benjamin Hughes", "Lucas Smith",
                                    "Robert King", "Laura Callahan", "Miguel Simmons", "Isabella Carter", "Andrew Fuller", "Madeleine Russell",
                                    "Steven Buchanan", "Nancy Davolio", "Michael Suyama", "Margaret Peacock", "Janet Leverling", "Ariana Alexander",
                                    "Brad Farkus", "Bart Arnaz", "Arnie Schwartz", "Billy Zimmer", "Samantha Piper", "Maggie Boxter",
                                    "Terry Bradley", "Greta Sims", "Cindy Stanwick", "Marcus Orbison",
                                    "Sandy Bright", "Ken Samuelson", "Brett Wade", "Wally Hobbs", "Brad Jameson", "Karen Goodson",
                                    "Morgan Kennedy", "Violet Bailey", "John Heart", "Arthur Miller", "Robert Reagan",
                                    "Ed Holmes", "Sammy Hill", "Olivia Peyton", "Jim Packard", "Hannah Brookly", "Harv Mudd",
                                    "Todd Hoffman", "Kevin Carter","Mary Stern", "Robin Cosworth","Jenny Hobbs", "Dallas Lou"};
            Patients = new ObservableCollection<Patient>();
            int patientCount = PatientNames.Length;
            int patientId = 1;
            DateTime birthday = new DateTime(1975, 2, 5);
            for(int i = 0; i < patientCount; i++) {
                Patient patient = Patient.Create();
                patient.Id = patientId++;
                patient.Name = PatientNames[i];
                patient.BirthDate = birthday.AddMonths(random.Next(1, 12)).AddYears(random.Next(0, 20));
                patient.Phone = "(" + random.Next(100, 999) + ") " + random.Next(100, 999) + "-" + random.Next(1000, 9999);
                Patients.Add(patient);
            }
        }
        static void CreateDepartments() {
            Departments = new ObservableCollection<HospitalDepartment>();
            DepartmentPrimaryCare = HospitalDepartment.Create(0, "Primary Care");
            DepartmentOphthalmology = HospitalDepartment.Create(1, "Ophthalmology");
            DepartmentDermatology = HospitalDepartment.Create(2, "Dermatology");
            DepartmentCardiology = HospitalDepartment.Create(3, "Cardiology");
            Departments.Add(DepartmentPrimaryCare);
            Departments.Add(DepartmentOphthalmology);
            Departments.Add(DepartmentDermatology);
            Departments.Add(DepartmentCardiology);
        }
        static void CreateDoctors() {
            var d0 = Doctor.Create(EmployeesDataHelper.Employees[0]);
            var d1 = Doctor.Create(EmployeesDataHelper.Employees[1]);
            var d2 = Doctor.Create(EmployeesDataHelper.Employees[2]);
            var d3 = Doctor.Create(EmployeesDataHelper.Employees[3]);
            var d4 = Doctor.Create(EmployeesDataHelper.Employees[4]);
            var d5 = Doctor.Create(EmployeesDataHelper.Employees[5]);
            var d6 = Doctor.Create(EmployeesDataHelper.Employees[6]);
            var d7 = Doctor.Create(EmployeesDataHelper.Employees[7]);
            Doctors = new ObservableCollection<Doctor>() { d0, d1, d2, d3, d4, d5, d6, d7 };
            DepartmentPrimaryCare.Doctors.Add(d0);
            DepartmentPrimaryCare.Doctors.Add(d1);
            DepartmentPrimaryCare.Doctors.Add(d2);
            DepartmentOphthalmology.Doctors.Add(d3);
            DepartmentOphthalmology.Doctors.Add(d4);
            DepartmentDermatology.Doctors.Add(d5);
            DepartmentDermatology.Doctors.Add(d6);
            DepartmentCardiology.Doctors.Add(d6);
            d0.Department = DepartmentPrimaryCare;
            d1.Department = DepartmentPrimaryCare;
            d2.Department = DepartmentPrimaryCare;
            d3.Department = DepartmentOphthalmology;
            d4.Department = DepartmentOphthalmology;
            d5.Department = DepartmentDermatology;
            d6.Department = DepartmentDermatology;
            d7.Department = DepartmentCardiology;
        }
        static void CreateAppointmentLocations() {
            AppointmentLocations = new ObservableCollection<AppointmentLocation>();
            AppointmentLocationClinic = AppointmentLocation.Create(0, "Clinic");
            AppointmentLocationHospital = AppointmentLocation.Create(1, "Hospital");
            AppointmentLocationHospice = AppointmentLocation.Create(2, "Hospice");
            AppointmentLocationHouseCall = AppointmentLocation.Create(3, "House Call");
            AppointmentLocations.Add(AppointmentLocationClinic);
            AppointmentLocations.Add(AppointmentLocationHospital);
            AppointmentLocations.Add(AppointmentLocationHospice);
            AppointmentLocations.Add(AppointmentLocationHouseCall);
        }
        static void CreatePaymentStates() {
            PaymentStates = new ObservableCollection<PaymentState>();
            PaymentStatePaid = PaymentState.Create(0, "Paid", DefaultBrushNames.StatusBusy);
            PaymentStateOverdue = PaymentState.Create(1, "Overdue", DefaultBrushNames.StatusOutOfOffice);
            PaymentStateNotYetBilled = PaymentState.Create(2, "Not Yet Billed", DefaultBrushNames.StatusTentative);
            PaymentStates.Add(PaymentStatePaid);
            PaymentStates.Add(PaymentStateOverdue);
            PaymentStates.Add(PaymentStateNotYetBilled);
        }
        static void CreateDurationAndPurposes() {
            var phoneConsultation = Tuple.Create(TimeSpan.FromMinutes(20), "Phone Consultation");
            var caseReview = Tuple.Create(TimeSpan.FromMinutes(20), "Case Review");
            var consultation = Tuple.Create(TimeSpan.FromMinutes(40), "Consultation");
            var vaccination = Tuple.Create(TimeSpan.FromMinutes(20), "Vaccination");
            var reviewOfTestResults = Tuple.Create(TimeSpan.FromMinutes(30), "Review of Test Results");
            var annualCheckUp = Tuple.Create(TimeSpan.FromMinutes(30), "Annual Check-Up");
            var surgicalReferral = Tuple.Create(TimeSpan.FromMinutes(60), "Surgical Referral");
            var eyeExam = Tuple.Create(TimeSpan.FromMinutes(20), "Eye Exam");
            var glaucomaSurgery = Tuple.Create(TimeSpan.FromMinutes(60), "Glaucoma Surgery");
            var cataractSurgery = Tuple.Create(TimeSpan.FromMinutes(60), "Cataract Surgery");
            var biopsy = Tuple.Create(TimeSpan.FromMinutes(40), "Biopsy");
            var cancerScreening = Tuple.Create(TimeSpan.FromMinutes(60), "Cancer Screening");
            var bypassSurgery = Tuple.Create(TimeSpan.FromMinutes(60), "Bypass Surgery");
            var electrocardiogramTest = Tuple.Create(TimeSpan.FromMinutes(30), "Electrocardiogram Test");
            var pacemakerImplantation = Tuple.Create(TimeSpan.FromMinutes(60), "Pacemaker Implantation");
            
            DepartmentPrimaryCare_DurationAndPurposes = new Dictionary<AppointmentLocation, Tuple<TimeSpan, string>[]>() {
                { AppointmentLocationClinic, new[] { vaccination, reviewOfTestResults, consultation, phoneConsultation, annualCheckUp, surgicalReferral } },
                { AppointmentLocationHospital, new Tuple<TimeSpan, string>[] {  } },
                { AppointmentLocationHospice, new[] { vaccination, annualCheckUp, caseReview, consultation } },
                { AppointmentLocationHouseCall, new[] { annualCheckUp, caseReview, consultation } },
            };
            DepartmentOphthalmology_DurationAndPurposes = new Dictionary<AppointmentLocation, Tuple<TimeSpan, string>[]>() {
                { AppointmentLocationClinic, new[] { eyeExam, consultation, phoneConsultation } },
                { AppointmentLocationHospital, new[] { glaucomaSurgery, cataractSurgery } },
                { AppointmentLocationHospice, new[] { caseReview, consultation } },
                { AppointmentLocationHouseCall, new[] { caseReview, consultation } },
            };
            DepartmentDermatology_DurationAndPurposes = new Dictionary<AppointmentLocation, Tuple<TimeSpan, string>[]>() {
                { AppointmentLocationClinic, new[] { reviewOfTestResults, consultation, phoneConsultation } },
                { AppointmentLocationHospital, new[] { biopsy, cancerScreening } },
                { AppointmentLocationHospice, new[] { consultation, caseReview } },
                { AppointmentLocationHouseCall, new[] { consultation, caseReview } },
            };
            DepartmentCardiology_DurationAndPurposes = new Dictionary<AppointmentLocation, Tuple<TimeSpan, string>[]>() {
                { AppointmentLocationClinic, new[] { consultation, phoneConsultation, electrocardiogramTest } },
                { AppointmentLocationHospital, new[] { bypassSurgery, pacemakerImplantation } },
                { AppointmentLocationHospice, new[] { consultation, caseReview } },
                { AppointmentLocationHouseCall, new[] { consultation, caseReview } },
            };
        }
        static void CreateAppointments() {
            Appointments = new ObservableCollection<MedicalAppointment>();
            int appointmentId = 0;
            int patientIndex = 0;
            DateTime firstDate = BaseDate.AddDays(-30);
            DateTime lastDate = BaseDate.AddDays(30);
            foreach(Doctor doctor in Doctors) {
                DateTime date = firstDate;
                while(date < lastDate) {
                    DateTime startTime = date.AddHours(9);
                    DateTime endTime = date.AddHours(18);
                    DateTime time = startTime.AddMinutes(random.Next(0, 8) * 10);
                    while(time < endTime) {
                        var patient = Patients[patientIndex];
                        var location = GetRandomItem(AppointmentLocations);
                        var durationAndPurpose = GetDurationAndPurpose(doctor, location);
                        if (durationAndPurpose == null)
                            continue;
                        var duration = durationAndPurpose.Item1;
                        var purpose = durationAndPurpose.Item2;
                        var paymentState = GetPaymentState(time, duration);

                        if(time + duration > endTime)
                            break;

                        var apt = MedicalAppointment.Create();
                        apt.Id = appointmentId;
                        apt.StartTime = time;
                        apt.EndTime = time.Add(duration);
                        apt.PaymentStateId = paymentState.Id;
                        apt.Subject = patient.Name;
                        apt.PatientId = patient.Id;
                        apt.DoctorId = doctor.Id;
                        apt.LocationId = location.Id;
                        apt.Purpose = purpose;
                        Appointments.Add(apt);

                        appointmentId++;
                        patientIndex++;
                        if(patientIndex >= Patients.Count - 1)
                            patientIndex = 0;
                        time = time.Add(duration).AddMinutes(random.Next(2, 8) * 10);
                    }
                    date = date.AddDays(1);
                }
            }
        }
        static PaymentState GetPaymentState(DateTime time, TimeSpan duration) {
            if(time + duration >= DateTime.Now)
                return PaymentStateNotYetBilled;
            if(time.Date == DateTime.Now.Date)
                return GetRandomItem(new[] { 0, 0, 1 }) == 0 ? PaymentStatePaid : PaymentStateOverdue;
            return GetRandomItem(new[] { 0, 0, 0, 1 }) == 0 ? PaymentStatePaid : PaymentStateOverdue;
        }
        static Tuple<TimeSpan, string> GetDurationAndPurpose(Doctor doctor, AppointmentLocation type) {
            Dictionary<AppointmentLocation, Tuple<TimeSpan, string>[]> durationAndPurposes = null;
            if(doctor.Department == DepartmentPrimaryCare)
                durationAndPurposes = DepartmentPrimaryCare_DurationAndPurposes;
            else if(doctor.Department == DepartmentOphthalmology)
                durationAndPurposes = DepartmentOphthalmology_DurationAndPurposes;
            else if(doctor.Department == DepartmentDermatology)
                durationAndPurposes = DepartmentDermatology_DurationAndPurposes;
            else if(doctor.Department == DepartmentCardiology)
                durationAndPurposes = DepartmentCardiology_DurationAndPurposes;
            return GetRandomItem(durationAndPurposes[type]);
        }
        static T GetRandomItem<T>(IEnumerable<T> source) {
            if (source.Count() == 0)
                return default(T);
            return source.ElementAt(random.Next(0, source.Count()));
        }
    }
}
