namespace EditorsDemo.ModuleResources {
    public class CardInfo {
        public string Name { get; private set; }
        public string Template { get; private set; }
        public CardInfo(string name, string template) {
            Name = name;
            Template = template;
        }
    }
}
