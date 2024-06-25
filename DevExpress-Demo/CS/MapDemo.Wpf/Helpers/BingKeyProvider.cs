namespace MapDemo {
    public static class BingKeyProvider {
        const string key = DevExpress.Map.Native.DXBingKeyVerifier.BingKeyWpfMapDemo;

        public static string BingKey { get { return key; } }
    }
}
