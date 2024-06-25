using System;
using System.Collections.Generic;

namespace GanttDemo.Examples {
    public class PartsProductionViewModel {
        public PartsProductionViewModel() {
            #region initialization
            var startDate = DateTime.Now.Date.AddDays(3);
            Parts = new List<PartItem> {
                new PartItem() {
                    StartProductionDate = startDate,
                    FinishProductionDate = startDate.AddDays(10),
                    Name = "Part 1",
                    MachineTools = new List<MachineToolComponentLink>() { new MachineToolComponentLink(1) }
                },
                new PartItem() {
                    StartProductionDate = startDate,
                    FinishProductionDate = startDate.AddDays(11),
                    Name = "Part 2",
                    MachineTools = new List<MachineToolComponentLink>() { new MachineToolComponentLink(2) }
                },
                new PartItem() {
                    StartProductionDate = startDate,
                    FinishProductionDate = startDate.AddDays(4),
                    Name = "Part 3",
                    MachineTools = new List<MachineToolComponentLink>() { new MachineToolComponentLink(3) }
                },
                new PartItem() {
                    StartProductionDate = startDate,
                    FinishProductionDate = startDate.AddDays(3),
                    Name = "Part 4",
                    MachineTools = new List<MachineToolComponentLink>() { new MachineToolComponentLink(4) }
                },
                new PartItem() {
                    StartProductionDate = startDate,
                    FinishProductionDate = startDate.AddDays(21),
                    Name = "Part 5",
                    MachineTools = new List<MachineToolComponentLink>() { new MachineToolComponentLink(5) }
                },
                new PartItem() {
                    StartProductionDate = startDate,
                    FinishProductionDate = startDate.AddDays(12),
                    Name = "Part 6",
                    MachineTools = new List<MachineToolComponentLink>() { new MachineToolComponentLink(6) }
                },
                new PartItem() {
                    StartProductionDate = startDate,
                    FinishProductionDate = startDate.AddDays(10),
                    Name = "Part 7",
                    MachineTools = new List<MachineToolComponentLink>() { new MachineToolComponentLink(7) }
                },
                new PartItem() {
                    StartProductionDate = startDate,
                    FinishProductionDate = startDate.AddDays(12),
                    Name = "Part 8",
                    MachineTools = new List<MachineToolComponentLink>() { new MachineToolComponentLink(8) }
                },
                new PartItem() {
                    StartProductionDate = startDate,
                    FinishProductionDate = startDate.AddDays(20),
                    Name = "Part 9",
                    MachineTools = new List<MachineToolComponentLink>() { new MachineToolComponentLink(9) }
                },
                new PartItem() {
                    StartProductionDate = startDate,
                    FinishProductionDate = startDate.AddDays(4),
                    Name = "Part 10",
                    MachineTools = new List<MachineToolComponentLink>() { new MachineToolComponentLink(10) }
                },
            };

            MachineTools = new List<MachineToolItem> {
                new MachineToolItem { Name = "Machine tool 1", Id = 1, WorkshopName = "Workshop 1" },
                new MachineToolItem { Name = "Machine tool 2", Id = 2, WorkshopName = "Workshop 1" },
                new MachineToolItem { Name = "Machine tool 3", Id = 3, WorkshopName = "Workshop 1" },
                new MachineToolItem { Name = "Machine tool 1", Id = 4, WorkshopName = "Workshop 2" },
                new MachineToolItem { Name = "Machine tool 2", Id = 5, WorkshopName = "Workshop 2" },
                new MachineToolItem { Name = "Machine tool 3", Id = 6, WorkshopName = "Workshop 2" },
                new MachineToolItem { Name = "Machine tool 4", Id = 7, WorkshopName = "Workshop 2" },
                new MachineToolItem { Name = "Machine tool 1", Id = 8, WorkshopName = "Workshop 3" },
                new MachineToolItem { Name = "Machine tool 2", Id = 9, WorkshopName = "Workshop 3" },
                new MachineToolItem { Name = "Machine tool 3", Id = 10, WorkshopName = "Workshop 3" }
            };
            #endregion
        }

        public IEnumerable<PartItem> Parts { get; private set; }
        public IEnumerable<MachineToolItem> MachineTools { get; private set; }
    }

    public class MachineToolItem {
        public string Name { get; set; }
        public string WorkshopName { get; set; }
        public int Id { get; set; }
        public string DisplayName { get { return WorkshopName + " / " + Name; } }
    }

    public class MachineToolComponentLink {
        public MachineToolComponentLink() : this(0) { }
        public MachineToolComponentLink(int id) {
            this.MachineToolId = id;
        }
        public int MachineToolId { get; set; }
    }

    public class PartItem {
        public string Name { get; set; }
        public DateTime StartProductionDate { get; set; }
        public DateTime FinishProductionDate { get; set; }
        public List<MachineToolComponentLink> MachineTools { get; set; }
    }
}
