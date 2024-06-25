using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProductsDemo.Modules {
    public class SeriesTypeDescriptor {
        public SeriesTypeDescriptor(Type seriesType, Type diagramType, string displayText) {
            this.SeriesType = seriesType;
            this.DiagramType = diagramType;
            this.DisplayText = displayText;
        }
        public Type SeriesType { get; set; }
        public Type DiagramType { get; set; }
        public string DisplayText { get; set; }
    }
    public enum ChartOrientation {
        GenerateSeriesFromColumns,
        GenerateSeriesFromRows
    }

    public class OnlyItemWrapper : ItemWrapper {
        public OnlyItemWrapper(ChartOrientation chartOrientation) {
            this.ChartOrientation = chartOrientation;
        }
        public override string Text {
            get {
                if(ChartOrientation == ChartOrientation.GenerateSeriesFromColumns)
                    return "Series from columns";
                return "Series from rows";
            }
        }
        public ChartOrientation ChartOrientation {
            get;
            private set;
        }
    }

    public class ItemWrapper {
        string text;
        protected ItemWrapper() {
        }
        public ItemWrapper(string text) {
            this.text = text;
        }
        public override string ToString() {
            return Text;
        }
        public virtual string Text{
            get { return text; }
        }
        public static ItemWrapper Create(string str) {
            return new ItemWrapper(str);
        }
    }
}
