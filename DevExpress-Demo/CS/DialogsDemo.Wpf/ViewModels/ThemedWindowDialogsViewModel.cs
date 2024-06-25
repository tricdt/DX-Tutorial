using System.Windows;
using System.Windows.Input;
using DevExpress.Mvvm;
using DevExpress.Mvvm.POCO;
using DevExpress.Xpf.Core;
using DevExpress.Xpf.DemoBase.DataClasses;
using DevExpress.Xpf.Grid;

namespace DialogsDemo {
    public class ThemedWindowDialogsViewModel {

        public ThemedWindowDialogsViewModel() {
            CreateContent();
        }

        protected virtual IDialogService DialogService { get { return null; } }

        protected virtual IMessageBoxService MessageBoxService { get { return null;} }

        public virtual object Content { get; set; }        

        public virtual Employee FocusedRow { get; set; }

        public void ShowDialogWindow(MouseButtonEventArgs parameter) {
            var hitObject = (parameter.Source as CardView).CalcHitInfo(parameter.OriginalSource as DependencyObject);            
            if(hitObject.HitTest == CardViewHitTest.Card)
                DialogService.ShowDialog(MessageButton.OK, "Show record", this);
        }

        public void ShowMessage() {
            MessageBoxService.ShowMessage("Would you like to send an e-mail?", "Confirm", MessageButton.OKCancel, MessageIcon.Question);
        }

        protected TChildModel CreateChildModel<TChildModel>() {
            var model = ViewModelSource<TChildModel>.Create();
            model.SetParentViewModel(this);            
            return model;
        }

        protected void CreateContent() {
            Content = CreateChildModel<ThemedWindowDialogContentModel>();
        }
    }

    public class ThemedWindowDialogContentModel {
        public ThemedWindowDialogsViewModel RootViewModel { get { return this.GetParentViewModel<ThemedWindowDialogsViewModel>(); } }        
    }    
}
