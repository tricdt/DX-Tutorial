using System;
using System.Data;
using DevExpress.Xpo.DB;

namespace DevExpress.VideoRent.Helpers {
    public class DataStoreHelper {
        public static IDataStore CreateInMemoryDataStore() {
            return new DataSetDataStore(new DataSet(), AutoCreateOption.SchemaOnly);
        }
    }
}
