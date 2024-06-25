using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace ControlsDemo.GalleryDemo {
    public enum ControlPanelCommand { Print, HorSize, VerSize, RotateLeft, RotateRight, Prior, Next, ZoomToOriginalSize, AutoSize, Play, Stop, ZoomValueChanged }
    public class ControlPanelEventArgs : EventArgs {
        public ControlPanelCommand Command { get; protected set; }
        public ControlPanelEventArgs(ControlPanelCommand command) {
            Command = command;
        }
    }
    public delegate void ControlPanelCommandClickEventHandler(object sender, ControlPanelEventArgs e);

    public partial class ControlPanel : UserControl {
        public ControlPanel() {
            InitializeComponent();
        }
        public double ZoomValue {
            get {
                return ZoomScroll.Value;
            }
            set {
                ZoomScroll.SetZoomValue(value, 0);
            }
        }
        public void SetAndAnimateZoomValue(double value) {
            ZoomScroll.SetZoomValue(value, 0.5);
        }
        public event ControlPanelCommandClickEventHandler CommandClick;

        protected virtual void OnPrintClick(object sender, EventArgs e) {
            if(CommandClick != null)
                CommandClick(this, new ControlPanelEventArgs(ControlPanelCommand.Print));
        }
        protected virtual void OnHorSizeClick(object sender, EventArgs e) {
            if(CommandClick != null)
                CommandClick(this, new ControlPanelEventArgs(ControlPanelCommand.HorSize));
        }
        protected virtual void OnVerSizeClick(object sender, EventArgs e) {
            if(CommandClick != null)
                CommandClick(this, new ControlPanelEventArgs(ControlPanelCommand.VerSize));
        }
        protected virtual void OnRotateLeftClick(object sender, EventArgs e) {
            if(CommandClick != null)
                CommandClick(this, new ControlPanelEventArgs(ControlPanelCommand.RotateLeft));
        }
        protected virtual void OnRotateRightClick(object sender, EventArgs e) {
            if(CommandClick != null)
                CommandClick(this, new ControlPanelEventArgs(ControlPanelCommand.RotateRight));
        }
        protected virtual void OnPrevClick(object sender, EventArgs e) {
            if(CommandClick != null)
                CommandClick(this, new ControlPanelEventArgs(ControlPanelCommand.Prior));
        }
        protected virtual void OnNextClick(object sender, EventArgs e) {
            if(CommandClick != null)
                CommandClick(this, new ControlPanelEventArgs(ControlPanelCommand.Next));
        }
        protected virtual void On1to1Click(object sender, EventArgs e) {
            if(CommandClick != null)
                CommandClick(this, new ControlPanelEventArgs(ControlPanelCommand.ZoomToOriginalSize));
        }
        protected virtual void OnAutoSizeClick(object sender, EventArgs e) {
            if(CommandClick != null)
                CommandClick(this, new ControlPanelEventArgs(ControlPanelCommand.AutoSize));
        }
        protected virtual void OnPlayClick(object sender, EventArgs e) {
            if(CommandClick != null)
                CommandClick(this, new ControlPanelEventArgs(ControlPanelCommand.Play));
        }
        private void ZoomScroll_ValueChanged(object sender, EventArgs e) {
            if(CommandClick != null)
                CommandClick(this, new ControlPanelEventArgs(ControlPanelCommand.ZoomValueChanged));
        }
    }
}
