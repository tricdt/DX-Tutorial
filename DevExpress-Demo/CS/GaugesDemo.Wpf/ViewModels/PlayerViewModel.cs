using System;
using System.Windows.Threading;
using DevExpress.Xpf.Gauges;

namespace GaugesDemo {
    public class PlayerViewModel {
        public virtual string PlayerText { get; protected set; }
        public virtual DateTime Time { get; protected set; }
        public virtual bool IsReading { get; protected set; }
        public virtual CreepingLineDirection CreepingLineAnimationDirection { get; set; }

        const string rightToLeftRadioText = "RADIO            NOW PLAYING        WAAF FM BOSTON        107.3 MHZ";
        const string leftToRightRadioText = "107.3 MHZ         WAAF FM BOSTON        NOW PLAYING        RADIO";
        const string rightToLeftCDSourceInfo = "CD          NOW PLAYING              ";
        const string leftToRightCDSourceInfo = "               NOW PLAYING              CD";
        const string rightToLeftTrackInfo = "          AT 320 KBPS         MP3/WMA";
        const string leftToRightTrackInfo = "MP3/WMA          AT 320 KBPS         ";

        static string[] rightToLeftTracks = { "THE DARK SIDE OF THE MOON       PINK FLOYD", "SMOKE ON THE WATER       DEEP PURPLE", "BLACK MOUNTAIN SIDE       LED ZEPPELIN", "TRANSILVANIA       IRON MAIDEN", "HARD ROAD       BLACK SABBATH" };
        static string[] leftToRightTracks = { "PINK FLOYD       THE DARK SIDE OF THE MOON", "DEEP PURPLE       SMOKE ON THE WATER", "LED ZEPPELIN       BLACK MOUNTAIN SIDE", "IRON MAIDEN       TRANSILVANIA", "BLACK SABBATH       HARD ROAD" };


        bool isRadioPlaying = true;
        int currentTrack;
        readonly DispatcherTimer timeTimer = new DispatcherTimer();
        readonly DispatcherTimer blinkingTimer = new DispatcherTimer();

        protected PlayerViewModel() {
            timeTimer.Interval = TimeSpan.FromSeconds(1);
            blinkingTimer.Interval = TimeSpan.FromSeconds(4);
            CreepingLineAnimationDirection = CreepingLineDirection.RightToLeft;
            blinkingTimer.Tick += OnBlinkingTimedEvent;
            UpdateTime();
        }
        void OnTimedEvent(object source, EventArgs e) {
            UpdateTime();
        }
        void OnBlinkingTimedEvent(object source, EventArgs e) {
            blinkingTimer.Stop();
            IsReading = false;
            ChangeText();
        }
        void UpdateTime() {
            Time = DateTime.Now;
        }
        public void ChangeSource() {
            IsReading = isRadioPlaying;
            isRadioPlaying = !isRadioPlaying;
            if(isRadioPlaying)
                SetPlayerText(() => leftToRightRadioText, () => rightToLeftRadioText);
            else {
                PlayerText = "READING";
                blinkingTimer.Start();
            }
        }
        void ChangeText() {
            if(blinkingTimer.IsEnabled)
                return;
            if(isRadioPlaying)
                SetPlayerText(() => leftToRightRadioText, () => rightToLeftRadioText);
            else
                SetPlayerText(
                    () => leftToRightTrackInfo + leftToRightTracks[currentTrack] + leftToRightCDSourceInfo,
                    () => rightToLeftCDSourceInfo + rightToLeftTracks[currentTrack] + rightToLeftTrackInfo
                );
        }
        void SetPlayerText(Func<string> leftToRightText, Func<string> rightToLeftText) {
            PlayerText = CreepingLineAnimationDirection.Equals(CreepingLineDirection.LeftToRight) ? leftToRightText() : rightToLeftText();
        }
        protected void OnCreepingLineAnimationDirectionChanged() {
            ChangeText();
        }
        public void SwitchNextTrack() {
            if(currentTrack < leftToRightTracks.Length - 1 && !isRadioPlaying) {
                currentTrack++;
                ChangeText();
            }
        }
        public void SwitchPreviousTrack() {
            if(currentTrack > 0 && !isRadioPlaying) {
                currentTrack--;
                ChangeText();
            }
        }
        public void SwitchFirstTrack() {
            if(currentTrack != 0 && !isRadioPlaying) {
                currentTrack = 0;
                ChangeText();
            }
        }
        public void SwitchLastTrack() {
            if(currentTrack != leftToRightTracks.Length - 1 && !isRadioPlaying) {
                currentTrack = leftToRightTracks.Length - 1;
                ChangeText();
            }
        }
        public void Start() {
            timeTimer.Tick += OnTimedEvent;
            timeTimer.Start();
        }
        public void Stop() {
            timeTimer.Stop();
            timeTimer.Tick -= OnTimedEvent;
            blinkingTimer.Stop();
            blinkingTimer.Tick -= OnBlinkingTimedEvent;
        }
    }
}
