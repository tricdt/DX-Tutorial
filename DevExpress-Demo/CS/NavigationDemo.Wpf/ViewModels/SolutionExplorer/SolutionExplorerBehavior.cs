using System;
using System.Diagnostics;
using System.IO;
using System.Text;
using System.Threading;
using System.Windows;
using DevExpress.Mvvm.UI.Interactivity;
using DevExpress.Xpf.Core;
using DevExpress.Xpf.DemoBase.Helpers;
using DevExpress.Xpf.DemoBase.Helpers.Internal;
using DevExpress.Xpf.Grid;

namespace NavigationDemo {

    public class SolutionExplorerBehavior : Behavior<TreeViewControl> {
        public CodeViewPresenter CodeView {
            get { return (CodeViewPresenter)GetValue(CodeViewProperty); }
            set { SetValue(CodeViewProperty, value); }
        }

        public static readonly DependencyProperty CodeViewProperty =
            DependencyProperty.Register("CodeView", typeof(CodeViewPresenter), typeof(SolutionExplorerBehavior), new PropertyMetadata(null));       

        readonly System.Timers.Timer timer = new System.Timers.Timer();
        readonly Stopwatch stopwatch = new Stopwatch();      

        protected override void OnAttached() {
            base.OnAttached();
            AssociatedObject.CurrentItemChanged += AssociatedObject_CurrentItemChanged;
            AssociatedObject.Loaded += AssociatedObject_Loaded;
            timer.Interval = 500;
            timer.Elapsed += Timer_Elapsed;          
        }

        protected override void OnDetaching() {
            AssociatedObject.Loaded -= AssociatedObject_Loaded;
            AssociatedObject.CurrentItemChanged -= AssociatedObject_CurrentItemChanged;        
            base.OnDetaching();
        }

        private void AssociatedObject_Loaded(object sender, RoutedEventArgs e) {
            UpdateCodeText();
        }

        private void AssociatedObject_CurrentItemChanged(object sender, CurrentItemChangedEventArgs e) {         
            stopwatch.Restart();
            SolutionNode old = e.OldItem as SolutionNode;
            UpdateCodeText(old != null ? old.FileName : null);
        }

        private void Timer_Elapsed(object sender, System.Timers.ElapsedEventArgs e) {
            if(stopwatch.ElapsedMilliseconds > 999) {
                timer.Stop();            
                Dispatcher.Invoke(new Action(() => {
                    var solutionNode = AssociatedObject.CurrentItem as SolutionNode;
                    CodeView.UpdateTextPresenterFromCache(solutionNode.FileName);
                    CodeView.Search(solutionNode.SearchString, solutionNode.SearchName ?? solutionNode.Name);
                }));                
               
              
            }
        }


        public void UpdateCodeText(string oldFileName = null) {
            var solutionNode = AssociatedObject.CurrentItem as SolutionNode;
            if(solutionNode != null && CodeView != null && CodeView.IsLoaded) {
                UpdateCache();
                if(oldFileName != solutionNode.FileName) {    
                    timer.Start();
                    return;
                }
                CodeView.Search(solutionNode.SearchString, solutionNode.SearchName ?? solutionNode.Name);
                return;
            }
        }

        void UpdateCache() {
            if(CodeView.CacheCount != 0)
                return;
            foreach(var node in AssociatedObject.Nodes) {
                var solutionNode = node.Content as SolutionNode;
                var uri = new Uri("pack://application:,,,/NavigationDemo;component/ViewModels/SolutionExplorer/CodeFiles/" + solutionNode.FileName, UriKind.Absolute);
                var code = Application.GetResourceStream(uri);
                using(StreamReader reader = new StreamReader(code.Stream)) {
                    StringBuilder str = new StringBuilder();
                    int countLine = 0;
                    while(!reader.EndOfStream) {
                        str.AppendLine(reader.ReadLine());
                        countLine++;
                    }
                    var codeText = new CodeLanguageText(DevExpress.Xpf.DemoBase.Helpers.TextColorizer.CodeLanguage.CS, str.ToString());
                    CodeView.AddCache(solutionNode.FileName, codeText);
                }
            }
        }
    }
}
