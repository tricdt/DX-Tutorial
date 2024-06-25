using System;
using System.Collections.Generic;
using System.IO;
using System.Threading;
using System.Xml.Serialization;
using DevExpress.DemoData.Helpers;

namespace DevExpress.RealtorWorld.Xpf.DataModel {
    public static class DataHelper {
        static readonly object dataFilesLock = new object();
        static readonly Dictionary<string, ReusableStream> dataFiles = new Dictionary<string, ReusableStream>();
        static Stream GetDataFileCore(string name) {
            string filePath = DataFilesHelper.FindFile(name, DataFilesHelper.DataPath);
            FileStream fileStream = new FileStream(filePath, FileMode.Open, FileAccess.Read);
            return fileStream;
        }
        public static ReusableStream GetDataFile(string name) {
            ReusableStream stream;
            bool loadData = false;
            lock(dataFilesLock) {
                if(!dataFiles.TryGetValue(name, out stream)) {
                    stream = new ReusableStream();
                    loadData = true;
                    dataFiles.Add(name, stream);
                }
            }
            stream.Reset();
            if(loadData) {
                stream.Data = GetDataFileCore(name);
            }
            return stream;
        }
    }
    public sealed class ReusableStream : IDisposable {
        Stream data;
        readonly AutoResetEvent mutex;

        public ReusableStream() {
            this.mutex = new AutoResetEvent(true);
        }
        public Stream Data {
            get { return data; }
            set {
                data = value;
                if(data != null && !data.CanSeek)
                    data = new MemoryStream(DevExpress.Xpf.Utils.StreamHelper.CopyAllBytes(data));
            }
        }
        public void Reset() {
            this.mutex.WaitOne();
            if(this.data != null)
                this.data.Seek(0, SeekOrigin.Begin);
        }
        public void Dispose() {
            this.mutex.Set();
        }
    }
}
