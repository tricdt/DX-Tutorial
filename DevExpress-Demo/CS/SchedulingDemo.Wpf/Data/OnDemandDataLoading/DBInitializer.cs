using System;
using System.Collections.Generic;

namespace SchedulingDemo {
    public class DBInitializer {
        public static void Init() {
            new DBInitializer().InitializeDBContext();
        }
        DBInitializer() { }

        SchedulingContext dbContext;
        int maxValueCore = 80;
        int resCount = 10;
        int step = 20;
        Random random = new Random();
        
        void InitializeDBContext() {
            if(SchedulingContext.IsExist())
                return;
            dbContext = new SchedulingContext();
            try {
                dbContext.SetAutoDetectChangesEnabled(false);
                dbContext.ExecuteSql(SQLCreateResourcesTable);
                dbContext.ExecuteSql(SQLCreateAptsTable);
                Generate();
            } finally {
                dbContext.Dispose();
            }
        }
        void Generate() {
            int resId = 1;
            for(int i = 1; i < 6; i++) {
                resId++;
                dbContext.ResourceEntities.Add(new ResourceEntity() { Id = resId, Description = "Room #" + (100 + i), Group = "Main floor" });
            }
            for(int i = 1; i < 5; i++) {
                resId++;
                dbContext.ResourceEntities.Add(new ResourceEntity() { Id = resId, Description = "Room #" + (200 + i), Group = "Second floor" });
            }
            for(int start = 0; start < maxValueCore; start += step) {
                GenerateByStep(start, start + step);
            }
        }
        void GenerateByStep(int start, int end) {
            var appts = new List<AppointmentEntity>();
            for(int day = start; day < end; day++) {
                for(int res = 1; res < resCount + 1; res++) {
                    GenerateApptsForDay(appts, day, res);
                    GenerateApptsForDay(appts, -day - 1, res);
                }
            }
            dbContext.AppointmentEntities.AddRange(appts);
            dbContext.SaveChanges();
        }
        void GenerateApptsForDay(List<AppointmentEntity> appts, int day, int res) {
            DateTime date = DateTime.Today.AddDays(day);
            int shift = day + res;

            bool generate1 = date > DateTime.Today.AddDays(-4) && date < DateTime.Today.AddDays(4) && (res == 1 || res == 2);
            bool generate2 = generate1 || (shift % 7 > 1) || (shift % 7 < -5);
            bool generate3 = generate1 || (generate2 && random.Next(0, 3) == 0);
            
            if(generate1)
                appts.Add(CreateAppt(res, date, 6 + shift % 2, 8 + shift % 2));
            if(generate2)
                appts.Add(CreateAppt(res, date, 10 + shift % 3, 12 + shift % 3));
            if(generate3)
                appts.Add(CreateAppt(res, date, 15 + shift % 5, 18 + shift % 5));
        }
        AppointmentEntity CreateAppt(int res, DateTime date, int start, int end) {
            return new AppointmentEntity() {
                Subject = Names[random.Next(0, Names.Length)],
                ResourceId = res,
                Start = date.AddHours(start),
                QueryStart = date.AddHours(start),
                End = date.AddHours(end),
                QueryEnd = date.AddHours(end),
            };
        }

        const string SQLCreateResourcesTable = @"
            CREATE TABLE ResourceEntities (
                `Id` INTEGER NOT NULL PRIMARY KEY AUTOINCREMENT UNIQUE,
                `Description` TEXT,
                `Group` TEXT
                )";
        const string SQLCreateAptsTable = @"
            CREATE TABLE AppointmentEntities (
                `Id` INTEGER NOT NULL PRIMARY KEY AUTOINCREMENT UNIQUE,
                `AppointmentType` INTEGER,
                `AllDay` INTEGER,
                `Description` TEXT,
                `Location` TEXT,
                `End` REAL,
                `QueryEnd` REAL,
                `Label` INTEGER,
                `Status` INTEGER,
                `RecurrenceInfo` TEXT,
                `ReminderInfo` TEXT,
                `ResourceId` INTEGER,
                `Start` REAL,
                `QueryStart` REAL,
                `Subject` TEXT,
                `TimeZoneId` TEXT
                )";
        static readonly string[] Names = {
            "Andrew Glover", "Mark Oliver", "Taylor Riley", "Addison Davis", "Benjamin Hughes", "Lucas Smith",
            "Robert King", "Laura Callahan", "Miguel Simmons", "Isabella Carter", "Andrew Fuller", "Madeleine Russell",
            "Steven Buchanan", "Nancy Davolio", "Michael Suyama", "Margaret Peacock", "Janet Leverling", "Ariana Alexander",
            "Brad Farkus", "Bart Arnaz", "Arnie Schwartz", "Billy Zimmer", "Samantha Piper", "Maggie Boxter",
            "Terry Bradley", "Greta Sims", "Cindy Stanwick", "Marcus Orbison",
            "Sandy Bright", "Ken Samuelson", "Brett Wade", "Wally Hobbs", "Brad Jameson", "Karen Goodson",
            "Morgan Kennedy", "Violet Bailey", "John Heart", "Arthur Miller", "Robert Reagan",
            "Ed Holmes", "Sammy Hill", "Olivia Peyton", "Jim Packard", "Hannah Brookly", "Harv Mudd",
            "Todd Hoffman", "Kevin Carter","Mary Stern", "Robin Cosworth","Jenny Hobbs", "Dallas Lou"
        };
    }
}
