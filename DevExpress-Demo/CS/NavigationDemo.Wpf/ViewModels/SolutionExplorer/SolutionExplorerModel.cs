using System.Collections.Generic;

namespace NavigationDemo {
    public enum TypeNode {
        File,
        Class,
        Property,
        Event,
        Field,
        Method,
        Enum,
        EnumElement
    }

    public class SolutionNode {
        public TypeNode TypeNode { get; set; }

        public string Name { get; set; }

        public string TypeName { get; set; }

        public string FileName { get; set; }

        List<SolutionNode> _children;

        public IEnumerable<SolutionNode> Children { get { return _children; } }

        public string DisplayName { get { return string.IsNullOrEmpty(TypeName) ? Name : string.Format("{0} : {1}", Name, TypeName); } }

        public string SearchString { get; set; }

        public string SearchName { get; set; }


        public void AddChildren(SolutionNode child) {
            if(_children == null)
                _children = new List<SolutionNode>();
            _children.Add(child);
        }
    }
}
