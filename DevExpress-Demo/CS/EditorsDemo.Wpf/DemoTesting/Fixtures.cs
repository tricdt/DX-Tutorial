using DevExpress.Xpf.DemoBase.DemoTesting;
using System.Threading;
using System.Windows.Threading;
using System;
using DevExpress.Xpf.Core.Native;
using DevExpress.Xpf.Editors;
using DevExpress.Xpf.Editors.Helpers;
using System.Windows;
using System.Globalization;
using DevExpress.Xpf.Core;
using DevExpress.Xpf.DemoBase;
using System.Linq;

namespace EditorsDemo.Tests {
    public class EditorsCheckAllDemosFixture : CheckAllDemosFixture {
    }
    public class EditorsDemoModulesAccessor : DemoModulesAccessor<EditorsDemoModule> {
        public EditorsDemoModulesAccessor(BaseDemoTestingFixture fixture)
            : base(fixture) {
        }
    }
    public abstract class BaseEditorsDemoTestingFixture : BaseDemoTestingFixture {
        protected EditorsDemoModulesAccessor ModuleAccessor { get; private set; }
        public BaseEditorsDemoTestingFixture() {
            ModuleAccessor = GetModulesAccessor();
        }
        protected abstract EditorsDemoModulesAccessor GetModulesAccessor();
    }
}
