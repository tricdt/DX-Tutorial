using DevExpress.Xpf.Core;
using System.Windows;

namespace ControlsDemo {
    public class SplashScreenMainViewModel {
        public virtual PredefinedSplashScreenType SplashScreenType { get; set; }
        public virtual WindowStartupLocation StartupLocation { get; set; }
        public virtual bool TrackOwnerPosition { get; set; }
        public virtual InputBlockMode InputBlock { get; set; }
        public virtual SplashScreenManagerModule Owner { get; set; }
        public virtual bool IsTrackOwnerPositionEnabled { get; set; }
        public virtual int ShowDelay { get; set; }
        public virtual int MinDuration { get; set; }

        public SplashScreenMainViewModel() {
            SplashScreenType = PredefinedSplashScreenType.Fluent;
            StartupLocation = WindowStartupLocation.CenterOwner;
            InputBlock = InputBlockMode.WindowContent;
        }

        protected void OnStartupLocationChanged() {
            if(StartupLocation == WindowStartupLocation.CenterOwner) {
                IsTrackOwnerPositionEnabled = true;
            } else {
                IsTrackOwnerPositionEnabled = false;
                TrackOwnerPosition = false;
            }
        }
    }
}
