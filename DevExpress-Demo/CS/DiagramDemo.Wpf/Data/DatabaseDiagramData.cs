using DevExpress.Data.Filtering;
using DevExpress.Diagram.Core;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Xml.Serialization;

namespace DevExpress.Diagram.Demos {
    public static class DatabaseData {
        public static DatabaseDefinition GetDatabaseDefinition() {
            using(var stream = DiagramDemoFileHelper.GetDataStream("DatabaseDiagram.xml")) {
                var serializer = new XmlSerializer(typeof(DatabaseDefinition));
                return (DatabaseDefinition)serializer.Deserialize(stream);
            }
        }
    }

    [XmlInclude(typeof(TableDefinition)), XmlInclude(typeof(ConnectionDefinition))]
    [XmlRoot("Database")]
    public class DatabaseDefinition {
        [XmlAttribute(AttributeName = "Name")]
        public string Name { get; set; }
        [XmlArray("Tables"), XmlArrayItem(typeof(TableDefinition))]
        public Collection<TableDefinition> Tables { get; private set; }
        [XmlArray("Connections"), XmlArrayItem(typeof(ConnectionDefinition))]
        public Collection<ConnectionDefinition> Connections { get; private set; }

        public TableDefinition this[string tableName] {
            get {
                if(string.IsNullOrEmpty(tableName))
                    throw new ArgumentException("tableName");
                return Tables.SingleOrDefault(x => string.Equals(x.Name, tableName));
            }
        }

        public DatabaseDefinition()
            : this(Enumerable.Empty<TableDefinition>(), Enumerable.Empty<ConnectionDefinition>()) {
        }
        public DatabaseDefinition(IEnumerable<TableDefinition> tables, IEnumerable<ConnectionDefinition> connections) {
            Tables = new Collection<TableDefinition>(tables.ToList());
            Connections = new Collection<ConnectionDefinition>(connections.ToList());
        }
    }
    [XmlInclude(typeof(ColumnDefinition))]
    public class TableDefinition {
        [XmlArray("Columns")]
        [XmlArrayItem(typeof(ColumnDefinition))]
        public Collection<ColumnDefinition> Columns { get; private set; }
        [XmlAttribute("Name")]
        public string Name { get; set; }
        [XmlAttribute("PositionX")]
        public int PositionX { get; set; }
        [XmlAttribute("PositionY")]
        public int PositionY { get; set; }

        public ColumnDefinition this[string columnName] {
            get {
                if(string.IsNullOrEmpty(columnName))
                    throw new ArgumentException("columnName");
                return Columns.SingleOrDefault(x => string.Equals(x.Name, columnName));
            }
        }
        public TableDefinition()
            : this(Enumerable.Empty<ColumnDefinition>()) {
        }
        public TableDefinition(IEnumerable<ColumnDefinition> columns) {
            Columns = new Collection<ColumnDefinition>(columns.ToList());
        }
    }
    public class ColumnDefinition {
        [XmlAttribute("TableName")]
        public string TableName { get; set; }
        [XmlAttribute("Name")]
        public string Name { get; set; }
        [XmlAttribute("IsPrimaryKey")]
        public bool IsPrimaryKey { get; set; }
        [XmlIgnore]
        public string Id { get { return string.Join(".", TableName, Name); } }
    }
    [XmlInclude(typeof(TableRelation))]
    public class ConnectionDefinition {
        [XmlAttribute("From")]
        public string From { get; set; }
        [XmlAttribute("To")]
        public string To { get; set; }
        [XmlAttribute("FromRelation")]
        public TableRelation FromRelation { get; set; }
        [XmlAttribute("ToRelation")]
        public TableRelation ToRelation { get; set; }

        public ConnectionDefinition(ColumnDefinition from, ColumnDefinition to) {
            From = from.Id;
            To = to.Id;
        }
        public ConnectionDefinition() { }
    }

    public enum TableRelation {
        One,
        Many
    }

    public class DatabaseDefinitionKeySelector : IKeySelector {
        object IKeySelector.GetKey(object obj) {
            if(obj is TableDefinition)
                return ((TableDefinition)obj).Name;
            else if(obj is ColumnDefinition)
                return ((ColumnDefinition)obj).Id;
            return obj;
        }
    }

    public class TableRelationEvaluationOperator : ICustomFunctionOperator {
        string ICustomFunctionOperator.Name { get { return "TableRelation"; } }

        object ICustomFunctionOperator.Evaluate(params object[] operands) {
            switch((TableRelation)operands[0]) {
                case TableRelation.One:
                    return "1";
                case TableRelation.Many:
                    return "*";
            }
            throw new ArgumentException();
        }

        Type ICustomFunctionOperator.ResultType(params Type[] operands) {
            return typeof(string);
        }
    }
}
