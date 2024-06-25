using System;
using System.IO;
using System.Reflection;
using System.Windows;
using DevExpress.VideoRent.ViewModel.Helpers;
using DevExpress.Xpf.Core.Serialization;
using System.Text;
using System.Xml;
using DevExpress.Xpf.Ribbon;

namespace DevExpress.VideoRent.Wpf.Helpers {
    public interface ILayoutDataStore {
        void Subscribe(SaveLoadLayoutDataEventHandler onAfterLoad, SaveLoadLayoutDataEventHandler onBeforeSave);
        void Unsubscribe(SaveLoadLayoutDataEventHandler onAfterLoad, SaveLoadLayoutDataEventHandler onBeforeSave);
        void WriteLayoutData(string folderName, byte[] data, bool clearing);
        byte[] ReadLayoutData(string folderName, bool clearing);
    }
    public class ElementLayoutInfo : BindingInfoBase {
        DependencyObject element;
        byte[] initialState = null;
        byte[] savedState = null;

        #region Dependency Properties
        public static readonly DependencyProperty StoreProperty;
        static ElementLayoutInfo() {
            Type ownerType = typeof(ElementLayoutInfo);
            StoreProperty = DependencyProperty.Register("Store", typeof(ILayoutDataStore), ownerType, new PropertyMetadata(null));
        }
        #endregion
        public ElementLayoutInfo() {
            ElementNames.Add(string.Empty);
        }
        public string ElementName { get { return ElementNames[0]; } set { ElementNames[0] = value; } }
        public StoreLayoutMode StoreLayoutMode { get; set; }
        public ILayoutDataStore Store { get { return (ILayoutDataStore)GetValue(StoreProperty); } set { SetValue(StoreProperty, value); } }
        public string StoreFolder { get; set; }

        protected override void Activate(int elementNameIndex, DependencyObject element) {
            StoreLayoutMode = StoreLayoutMode.UI;
            this.element = element;
            if(SubscribeToLoadedReturnIsLoaded(this.element, (s, e) => OnElementLoaded()))
                OnElementLoaded();
            Store.Subscribe(OnStoreAfterLoad, OnStoreBeforeSave);
        }
        void OnElementLoaded() {
            if(initialState != null) return;
            initialState = SerializeElement();
            if(savedState != null) {
                DeserializeElement(savedState);
                savedState = null;
            }
        }
        void DeserializeElement(byte[] dataArray) {
            if(initialState != null) {
                MemoryStream data = new MemoryStream(dataArray);
                DeserializeFromStream(this.element, data);
            } else {
                savedState = dataArray;
            }
        }
        byte[] SerializeElement() {
            MemoryStream data = new MemoryStream();
            SerializeToStream(this.element, data);
            return data.ToArray();
        }
        void OnStoreAfterLoad(object sender, SaveLoadLayoutDataEventArgs e) {
            if(this.element == null) return;
            DXSerializer.SetStoreLayoutMode(this.element, StoreLayoutMode);
            byte[] dataArray = Store.ReadLayoutData(StoreFolder, e.Clearing);
            if(dataArray != null)
                DeserializeElement(dataArray);
            else
                DeserializeElement(initialState);
        }
        void OnStoreBeforeSave(object sender, SaveLoadLayoutDataEventArgs e) {
            if(this.element == null || !IsElementLoaded(this.element)) return;
            DXSerializer.SetStoreLayoutMode(this.element, StoreLayoutMode);
            byte[] data = SerializeElement();
            Store.WriteLayoutData(StoreFolder, data, e.Clearing);
        }
        static bool IsElementLoaded(DependencyObject depObject) {
            FrameworkContentElement fce = depObject as FrameworkContentElement;
            if(fce != null) {
                return fce.IsLoaded;
            }
            FrameworkElement fe = depObject as FrameworkElement;
            if(fe != null) {
                return fe.IsLoaded;
            }
            return true;
        }
        static bool SubscribeToLoadedReturnIsLoaded(DependencyObject d, RoutedEventHandler onLoaded) {
            FrameworkContentElement fce = d as FrameworkContentElement;
            if(fce != null) {
                fce.Loaded += onLoaded;
                return fce.IsLoaded;
            }
            FrameworkElement fe = d as FrameworkElement;
            if(fe != null) {
                fe.Loaded += onLoaded;
                return fe.IsLoaded;
            }
            return true;
        }
        static void SerializeToStream(DependencyObject d, Stream s) {
            Window window = d as Window;
            if(window != null) {
                SerializeWindowToStream(window, s);
                return;
            }
            MethodInfo saveLayoutMethod = d.GetType().GetMethod("SaveLayoutToStream", new Type[] { typeof(Stream) });
            object methodParameter = s;
            if(saveLayoutMethod == null) {
                saveLayoutMethod = d.GetType().GetMethod("WriteToXML", new Type[] { typeof(XmlWriter) });
                methodParameter = XmlWriter.Create(s);
            }
            if(saveLayoutMethod != null) {
                saveLayoutMethod.Invoke(d, new object[] { methodParameter });
                if(methodParameter is XmlWriter) ((XmlWriter)methodParameter).Close();
                return;
            }
            DXSerializer.SerializeSingleObject(d, s, string.Empty);
        }
        static void DeserializeFromStream(DependencyObject d, Stream s) {
            Window window = d as Window;
            if(window != null) {
                DeserializeWindowFromStream(window, s);
                return;
            }
            MethodInfo restoreLayoutMethod = d.GetType().GetMethod("RestoreLayoutFromStream", new Type[] { typeof(Stream) });
            object methodParameter = s;
            if(restoreLayoutMethod == null) {
                restoreLayoutMethod = d.GetType().GetMethod("ReadFromXML", new Type[] { typeof(XmlReader) });
                methodParameter = XmlReader.Create(s);
            }
            if(restoreLayoutMethod != null) {
                restoreLayoutMethod.Invoke(d, new object[] { methodParameter });
                if(methodParameter is XmlReader) ((XmlReader)methodParameter).Close();
                return;
            }
            DXSerializer.DeserializeSingleObject(d, s, string.Empty);
        }
        static void SerializeWindowToStream(Window window, Stream s) {
            string data = string.Format("{0}:{1}:{2}", window.Width, window.Height, window.WindowState);
            byte[] bytes = Encoding.UTF8.GetBytes(data);
            s.Write(bytes, 0, bytes.Length);
        }
        static void DeserializeWindowFromStream(Window window, Stream s) {
            byte[] bytes = new byte[s.Length];
            s.Read(bytes, 0, bytes.Length);
            string data = Encoding.UTF8.GetString(bytes);
            string[] parts = data.Split(':');
            if(parts.Length != 3) return;
            window.WindowState = (WindowState)Enum.Parse(typeof(WindowState), parts[2]);
            if(window.WindowState != WindowState.Maximized) {
                window.Width = double.Parse(parts[0]);
                window.Height = double.Parse(parts[1]);
            }
        }
    }
}
