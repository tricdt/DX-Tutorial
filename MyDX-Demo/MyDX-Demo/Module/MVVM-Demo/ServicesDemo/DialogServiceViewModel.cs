using DevExpress.Mvvm;
using DevExpress.Mvvm.POCO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace MyDX_Demo.Module.MVVM_Demo.ServicesDemo
{
    public class DialogServiceViewModel
    {
        public void ShowDetail(string serviceName)
        {
            DialogServiceDetailViewModel detailViewModel = DialogServiceDetailViewModel.Create();

            UICommand registerCommand = new UICommand()
            {
                Caption = "Register",
                IsDefault = true,
                Command = new DelegateCommand(() => { }, () => !string.IsNullOrEmpty(detailViewModel.CustomerName))
            };
            UICommand cancelCommand = new UICommand()
            {
                Caption = "Cancel",
                IsCancel = true,
            };
            IDialogService service = this.GetService<IDialogService>(serviceName);
            UICommand result = service.ShowDialog(
                dialogCommands: new[] { registerCommand, cancelCommand },
                title: "Registration Dialog",
                viewModel: detailViewModel
            );
            if (result == registerCommand)
                MessageBox.Show("Registered: " + detailViewModel.CustomerName);
        }
        public void ShowDetailAsync(string serviceName)
        {
            DialogServiceDetailViewModel detailViewModel = DialogServiceDetailViewModel.Create();

            UICommand registerCommand = new UICommand()
            {
                Caption = "Register",
                IsDefault = true,
                Command = new AsyncCommand(Delay, () => !string.IsNullOrEmpty(detailViewModel.CustomerName)),
                AsyncDisplayMode = AsyncDisplayMode.Wait
            };
            UICommand cancelCommand = new UICommand()
            {
                Caption = "Cancel",
                IsCancel = true,
            };

            IDialogService service = this.GetService<IDialogService>(serviceName);
            UICommand result = service.ShowDialog(
                dialogCommands: new[] { registerCommand, cancelCommand },
                title: "Registration Dialog",
                viewModel: detailViewModel
            );

            if (result == registerCommand)
                MessageBox.Show("Registered: " + detailViewModel.CustomerName);
        }

        async Task Delay()
        {
            await Task.Delay(3000);
        }
    }
}
