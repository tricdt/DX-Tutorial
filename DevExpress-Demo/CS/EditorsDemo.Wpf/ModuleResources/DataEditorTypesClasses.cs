using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Windows.Media;
using DevExpress.DemoData.Models;
using DevExpress.Xpf.Editors;

namespace EditorsDemo {
    public class FieldDescription {
        public FieldDescription(string propertyName, string columnName, string editorDisplayName, string templateName, Type columnType, int id, int parentId) {
            this.PropertyName = propertyName;
            this.ColumnName = columnName;
            this.EditorDisplayName = editorDisplayName;
            this.TemplateName = templateName;
            this.ColumnType = columnType;
            this.Id = id;
            this.ParentId = parentId;
        }
        public string PropertyName { get; private set; }
        public string ColumnName { get; private set; }
        public string EditorDisplayName { get; private set; }
        public string TemplateName { get; private set; }
        public Type ColumnType { get; private set; }
        public int Id { get; private set; }
        public int ParentId { get; private set; }
    }
    public class MultiEditorsListPropertyDescriptorSQLite : PropertyDescriptor {
        string propertyName;
        bool isReadOnly;
        MultiEditorsListBaseSQLite list;
        int index;
        public MultiEditorsListPropertyDescriptorSQLite(MultiEditorsListBaseSQLite list, int index, string propertyName, bool isReadOnly)
            : base(propertyName, null) {
            this.propertyName = propertyName;
            this.isReadOnly = isReadOnly;
            this.list = list;
            this.index = index;
        }
        public override bool CanResetValue(object component) {
            return false;
        }
        public override object GetValue(object component) {
            return list.GetPropertyValue((int)component, index);
        }
        public override void SetValue(object component, object val) {
            list.SetPropertyValue((int)component, index, val);
        }
        public override bool IsReadOnly { get { return isReadOnly; } }
        public override string Name { get { return propertyName; } }
        public override Type ComponentType { get { return list.GetType(); } }
        public override Type PropertyType { get { return typeof(object); } }
        public override void ResetValue(object component) {
        }
        public override bool ShouldSerializeValue(object component) { return true; }
    }
    public abstract class MultiEditorsListBaseSQLite : IList, ITypedList, IBindingList {
        #region Field Descriptions
        List<FieldDescription> desc;
        public List<FieldDescription> FieldDescriptions { get { return desc ?? (desc = CreateFieldDescriptions()); } }
        protected virtual List<FieldDescription> CreateFieldDescriptions() {
            return new List<FieldDescription>() {
                new FieldDescription("ProductName", "Name", "TextEdit", "TextEdit", typeof(string), 1, -1),
                new FieldDescription("ProductID", "ID", "TextEdit (with numeric mask)","NumericTextEdit", typeof(int), 0, 1),
                new FieldDescription("EAN13", "Code", "TextEdit (with RegEx mask)" ,"RegExTextEdit", typeof(string), 9, 1),
                new FieldDescription("ProductName", "Name", "AutoSuggestEdit", "AutoSuggestEdit", typeof(string), 111, 13),
                new FieldDescription(string.Empty, "Password", "PasswordBoxEdit", "PasswordBoxEdit", typeof(string), 14, 1),
                new FieldDescription(string.Empty, "Countries", "ButtonEdit", "ButtonEdit", typeof(string), 13, -1),
                new FieldDescription("CategoryID", "Category", "ComboBoxEdit", "ComboBoxEdit", typeof(int), 2, 13),
                new FieldDescription(string.Empty, "Country", "ComboBoxEdit (with AutoComplete)", "AutoCompleteComboBoxEdit", typeof(string), 3, 2),
                new FieldDescription("CategoryID", "Categories", "ComboBoxEdit (Token Mode)", "TokenComboBoxEdit", typeof(string), 26, 2),
                new FieldDescription("CategoryID", "Category", "SearchLookUpEdit", "SearchLookUpEdit", typeof(int), 20, 2),
                new FieldDescription("CategoryID", "Category", "LookUpEdit", "LookUpEdit", typeof(int), 21, 2),
                new FieldDescription(string.Empty, "Notes", "MemoEdit", "MemoEdit", typeof(string), 10, 13),
                new FieldDescription(string.Empty, "Date", "DateEdit", "DateEdit", typeof(DateTime), 11, 13),
                new FieldDescription(string.Empty, "Font", "FontEdit", "FontEdit", typeof(FontFamily), 19, 13),
                new FieldDescription("UnitPrice", "Unit Price", "PopupCalcEdit", "PopupCalcEdit", typeof(double), 15, 13),
                new FieldDescription(string.Empty, "Color", "PopupColorEdit", "PopupColorEdit", typeof(Color), 16, 13),
                
                new FieldDescription(string.Empty, "Orders", "SparklineEdit", "SparklineEdit", typeof(List<int>), 23, 13),
                new FieldDescription(string.Empty, "Picture", "PopupImageEdit", "PopupImageEdit", typeof(ImageSource), 17, 13),
                new FieldDescription("UnitsInStock", "Units In Stock", "TrackBarEdit", "TrackBarEdit", typeof(double), 5, -1),
                new FieldDescription("ReorderLevel", "Reorder Level", "ZoomTrackBarEdit", "ZoomTrackBarEdit", typeof(double), 6, 5),
                new FieldDescription(string.Empty, "Range", "RangeTrackBarEdit", "RangeTrackBarEdit", typeof(TrackBarEditRange), 7, 5),
                new FieldDescription(string.Empty, "Units On Order", "ProgressBarEdit", "ProgressBarEdit", typeof(double), 18, 5),
                new FieldDescription("Discontinued", "Discontinued", "CheckEdit", "CheckEdit", typeof(bool), 8, -1),
                new FieldDescription(string.Empty, "Discontinued", "ToggleSwitchEdit", "ToggleSwitchEdit", typeof(bool), 28, -1),
                new FieldDescription(string.Empty, "Discount", "SpinEdit", "SpinEdit", typeof(decimal), 4, -1),
                new FieldDescription(string.Empty, "Pallete Size", "ListBoxEdit", "ListBoxEdit", typeof(string), 12, -1),
                new FieldDescription(string.Empty, "ID", "BarCodeEdit", "BarCodeEdit", typeof(string), 25, -1),
                new FieldDescription("Rating", "Rating", "RatingEdit", "RatingEdit", typeof(double), 27, -1),
                new FieldDescription("HyperLink", "Hyper Link", "HyperlinkEdit", "HyperlinkEdit", typeof(string), 29, -1),
            };
        }
        #endregion
        List<Product> products;
        public MultiEditorsListBaseSQLite() {
            products = NWindContext.Create().Products.ToList();
            CreateDataTable();
            CreateColumnCollection();
        }

        protected internal PropertyDescriptorCollection ColumnCollection { get; protected set; }
        protected List<Dictionary<string, object>> Table { get; private set; }

        string[] palleteSizes = new string[] { "4x4", "10x10", "16x16", "20x20", "25x25" };

        Random random = new Random();
        protected virtual void CreateDataTable() {
            this.Table = new List<Dictionary<string, object>>();
            for (int i = 0; i < products.Count; i++) {
                Dictionary<string, object> dataRow = new Dictionary<string, object>();
                foreach (FieldDescription description in FieldDescriptions) {
                    if (!string.IsNullOrEmpty(description.PropertyName))
                        dataRow[description.ColumnName] = GetProductPropertyValue(i, description.PropertyName);
                }
                dataRow["Country"] = GetRandomCountry();
                dataRow["Units On Order"] = random.Next(100);
                dataRow["Notes"] = "Notes: " + Environment.NewLine + "notes for Product #" + (i + 1);
                dataRow["Date"] = DateTime.Today.AddYears(-5).AddDays(-random.Next(1000));
                dataRow["Pallete Size"] = palleteSizes[random.Next(palleteSizes.Length)];
                dataRow["Countries"] = GetRandomCountry();
                double selectionStart = random.Next(50);
                dataRow["Range"] = new TrackBarEditRange(selectionStart, selectionStart + random.Next(50));
                dataRow["Color"] = GenerateRandomColor();
                dataRow["Brush"] = new LinearGradientBrush(GenerateRandomColor(), GenerateRandomColor(), random.Next(180));
                dataRow["Orders"] = CreateOrdersList(); ;
                dataRow["Password"] = GenerateRandomPassword();
                dataRow["Discount"] = GenerateRandomDiscount();
                dataRow["Picture"] = GetPhoto(i);
                dataRow["Font"] = GetFont(i);
                dataRow["Data"] = i + 3 + i * i;
                dataRow["Categories"] = GenerateRandomCategories(i);
                dataRow["Rating"] = random.Next(5);
                dataRow["Hyper Link"] = dataRow["Name"];
                Table.Add(dataRow);
            }
        }

        private object GenerateRandomCategories(int i) {
            if (random.Next(8) > 5)
                return null;
            i = i % NWindContext.Create().Categories.Count();
            return new List<object>() { NWindContext.Create().Categories.OrderBy(c => c.CategoryName).Skip(i).First().CategoryID };
        }
        protected object GetProductPropertyValue(int index, string propertyName) {
            Product product = products[index];
            var property = product.GetType().GetProperty(propertyName);
            return property != null ? property.GetValue(product, null) : null;
        }
        List<int> CreateOrdersList() {
            List<int> list = new List<int>();
            for (int i = 0; i < 10; i++)
                list.Add(random.Next(5));
            return list;
        }
        ImageSource GetPhoto(int i) {
            if (random.Next(8) > 5)
                return null;
            i = i % NWindContext.Create().Categories.Count();
            byte[] bytes = NWindContext.Create().Categories.OrderBy(c => c.CategoryName).Skip(i).First().Picture;
            return DevExpress.Xpf.Core.Native.ImageHelper.CreateImageFromStream(new MemoryStream(bytes));
        }
        FontFamily GetFont(int i) {
            IList fontSource = null;
            fontSource = new List<object>(Fonts.SystemFontFamilies);

            return new FontFamily(fontSource[random.Next(fontSource.Count)].ToString());
        }
        Color GenerateRandomColor() {
            byte[] bytes = new byte[3];
            random.NextBytes(bytes);
            return Color.FromArgb(byte.MaxValue, bytes[0], bytes[1], bytes[2]);
        }
        string GenerateRandomPassword() {
            return new string('p', random.Next(3, 15));
        }
        double GenerateRandomDiscount() {
            return random.NextDouble() * 0.1d;
        }
        string GetRandomCountry() {
            return NWindContext.Create().CountriesArray[random.Next(NWindContext.Create().CountriesArray.Length)];
        }


        public void SetPropertyValue(int rowIndex, int columnIndex, object value) {
            if (columnIndex > 0 && columnIndex <= Table.Count) {
                Table[columnIndex - 1][FieldDescriptions[rowIndex].ColumnName] = value;
                RaiseListChanged(rowIndex, columnIndex);
            }
        }

        void RaiseListChanged(int rowIndex, int columnIndex) {
            if (listChangedEventHandler == null)
                return;
            int[] rowIndexes = GetRowIndexes(rowIndex);
            foreach (int index in rowIndexes) {
                listChangedEventHandler(this, new System.ComponentModel.ListChangedEventArgs(System.ComponentModel.ListChangedType.ItemChanged, index));
            }
        }
        int[] GetRowIndexes(int rowIndex) {
            List<int> indexes = new List<int>();
            string propertyName = FieldDescriptions[rowIndex].PropertyName;
            for (int i = 0; i < Count; i++) {
                if (string.Equals(propertyName, FieldDescriptions[i].PropertyName, StringComparison.InvariantCultureIgnoreCase)) {
                    indexes.Add(i);
                }
            }
            return indexes.ToArray();
        }

        protected abstract void CreateColumnCollection();
        public abstract object GetPropertyValue(int rowIndex, int columnIndex);
        #region ITypedList Interface
        PropertyDescriptorCollection ITypedList.GetItemProperties(PropertyDescriptor[] descs) { return ColumnCollection; }
        string ITypedList.GetListName(PropertyDescriptor[] descs) { return ""; }
        #endregion
        #region IList Interface
        public virtual int Count {
            get { return FieldDescriptions.Count; }
        }
        public virtual bool IsSynchronized {
            get { return true; }
        }
        public virtual object SyncRoot {
            get { return true; }
        }
        public virtual bool IsReadOnly {
            get { return false; }
        }
        public virtual bool IsFixedSize {
            get { return true; }
        }
        public virtual IEnumerator GetEnumerator() {
            return null;
        }
        public virtual void CopyTo(System.Array array, int fIndex) {
        }
        public virtual int Add(object val) {
            throw new NotImplementedException();
        }
        public virtual void Clear() {
            throw new NotImplementedException();
        }
        public virtual bool Contains(object val) {
            throw new NotImplementedException();
        }
        public virtual int IndexOf(object val) {
            throw new NotImplementedException();
        }
        public virtual void Insert(int fIndex, object val) {
            throw new NotImplementedException();
        }
        public virtual void Remove(object val) {
            throw new NotImplementedException();
        }
        public virtual void RemoveAt(int fIndex) {
            throw new NotImplementedException();
        }
        object IList.this[int fIndex] {
            get { return fIndex; }
            set { }
        }
        #endregion

        #region IBindingList Members
        void IBindingList.AddIndex(PropertyDescriptor property) {
            throw new NotImplementedException();
        }
        object IBindingList.AddNew() {
            throw new NotImplementedException();
        }
        bool IBindingList.AllowEdit { get { return true; } }
        bool IBindingList.AllowNew { get { return false; } }
        bool IBindingList.AllowRemove { get { return false; } }
        void IBindingList.ApplySort(PropertyDescriptor property, System.ComponentModel.ListSortDirection direction) {
            throw new NotImplementedException();
        }
        int IBindingList.Find(PropertyDescriptor property, object key) {
            throw new NotImplementedException();
        }
        bool IBindingList.IsSorted { get { return false; } }
        event System.ComponentModel.ListChangedEventHandler listChangedEventHandler;
        event System.ComponentModel.ListChangedEventHandler IBindingList.ListChanged {
            add { listChangedEventHandler += value; }
            remove { listChangedEventHandler -= value; }
        }
        void IBindingList.RemoveIndex(PropertyDescriptor property) { throw new NotImplementedException(); }
        void IBindingList.RemoveSort() { throw new NotImplementedException(); }
        System.ComponentModel.ListSortDirection IBindingList.SortDirection { get { throw new NotImplementedException(); } }
        PropertyDescriptor IBindingList.SortProperty { get { return null; } }
        bool IBindingList.SupportsChangeNotification { get { return true; } }
        bool IBindingList.SupportsSearching { get { return false; } }
        bool IBindingList.SupportsSorting { get { return false; } }
        #endregion
    }
}
