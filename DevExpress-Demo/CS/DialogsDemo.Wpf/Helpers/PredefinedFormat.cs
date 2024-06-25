namespace DialogsDemo.Helpers
{
    public class PredefinedFormat {
        public PredefinedFormat(string example, string format) {
            Example = example;
            Format = format;
        }
        public string Example { get; }
        public string Format { get; }

        override public string ToString() {
            return $"{Example} [{Format}]";
        }
    }
}
