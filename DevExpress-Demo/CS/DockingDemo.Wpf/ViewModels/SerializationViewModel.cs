using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Reflection;
using System.Xml.Serialization;
using DevExpress.Mvvm;
using DevExpress.Mvvm.POCO;
using DevExpress.Utils;
using DevExpress.Xpf.DemoBase.Helpers;

namespace DockingDemo.ViewModels {
    public class SerializableViewModel {
        protected SerializableViewModel() { }

        public object Content { get; set; }
        public string DisplayName { get; set; }
        public string Name { get; set; }

        public static SerializableViewModel Create() {
            return ViewModelSource.Create(() => new SerializableViewModel());
        }
        public static SerializableViewModel Create(int id) {
            var vm = ViewModelSource.Create(() => new SerializableViewModel());
            vm.Name = "Model" + id;
            vm.DisplayName = "Model " + id;
            vm.Content = id;
            return vm;
        }
        public static SerializableViewModel Create(SerializationInfo info) {
            var vm = Create();
            vm.ApplySerializationInfo(info);
            return vm;
        }
        public SerializationInfo GetSerializationInfo() {
            return new SerializationInfo() { Content = Content, DisplayName = DisplayName, Name = Name };
        }
        void ApplySerializationInfo(SerializationInfo info) {
            Content = info.Content;
            DisplayName = info.DisplayName;
            Name = info.Name;
        }

        public class SerializationInfo {
            public SerializationInfo() { }

            public object Content { get; set; }
            public string DisplayName { get; set; }
            public string Name { get; set; }
        }
    }
    public class MVVMSerializationViewModel : ViewModelBase {
        private IList<object> _Items;
        Stream layoutStream;
        Stream vmStream;

        public MVVMSerializationViewModel() {
            _Items = GenerateItems(5);
        }

        public IList<object> Items {
            get { return _Items; }
        }
        public virtual IDockLayoutManagerSerializationService SerializationService { get { return null; } }

        public bool CanRestore() {
            return vmStream != null && layoutStream != null;
        }
        public void Restore() {
            vmStream.Position = 0;
            layoutStream.Position = 0;
            RestoreFromStream(vmStream);
            SerializationService.Deserialize(layoutStream);
        }
        public void RestoreSample(int index) {
            var assembly = Assembly.GetExecutingAssembly();
            string vmName = string.Format("views{0}.xml", index + 1);
            using(Stream resourceStream = AssemblyHelper.GetEmbeddedResourceStream(assembly, DemoHelper.GetPath("Layouts/MVVMSerialization/", assembly) + vmName, true)) {
                RestoreFromStream(resourceStream);
            }
            string name = string.Format("savedworkspace{0}.xml", index + 1);
            using(Stream resourceStream = AssemblyHelper.GetEmbeddedResourceStream(assembly, DemoHelper.GetPath("Layouts/MVVMSerialization/", assembly) + name, true)) {
                SerializationService.Deserialize(resourceStream);
            }
        }
        public void Store() {
            if(vmStream == null) vmStream = new MemoryStream();
            if(layoutStream == null) layoutStream = new MemoryStream();
            vmStream.SetLength(0);
            layoutStream.SetLength(0);
            StoreToStream(vmStream);
            SerializationService.Serialize(layoutStream);
        }
        IList<object> GenerateItems(int count) {
            ObservableCollection<object> items = new ObservableCollection<object>();
            for(int i = 0; i < count; i++) {
                items.Add(SerializableViewModel.Create(i));
            }
            return items;
        }
        void RestoreFromStream(Stream stream) {
            ViewModelSerializer serializer = ViewModelSerializer.Deserialize(stream);
            Items.Clear();
            foreach(var info in serializer.Infos) {
                Items.Add(SerializableViewModel.Create(info));
            }
        }
        void StoreToStream(Stream stream) {
            ViewModelSerializer serializer = new ViewModelSerializer();
            foreach(SerializableViewModel item in Items) {
                serializer.Infos.Add(item.GetSerializationInfo());
            }
            ViewModelSerializer.Serialize(stream, serializer);
        }

        [XmlRoot("ViewModels")]
        public class ViewModelSerializer {
            public ViewModelSerializer() {
                Infos = new List<SerializableViewModel.SerializationInfo>();
            }

            public List<SerializableViewModel.SerializationInfo> Infos { get; set; }
            public string Name { get; set; }

            public static ViewModelSerializer Deserialize(Stream stream) {
                ViewModelSerializer res;
                XmlSerializer s = new XmlSerializer(typeof(ViewModelSerializer), new Type[] { typeof(SerializableViewModel.SerializationInfo) });
                res = (ViewModelSerializer)s.Deserialize(stream);
                return res;
            }
            public static void Serialize(Stream stream, ViewModelSerializer obj) {
                XmlSerializer s = new XmlSerializer(typeof(ViewModelSerializer), new Type[] { typeof(SerializableViewModel.SerializationInfo) });
                s.Serialize(stream, obj);
            }
            public static void Serialize(string path, ViewModelSerializer obj) {
                XmlSerializer s = new XmlSerializer(typeof(ViewModelSerializer), new Type[] { typeof(SerializableViewModel.SerializationInfo) });
                using(Stream st = new FileStream(path, FileMode.Create)) {
                    s.Serialize(st, obj);
                }
            }
        }
    }
}
