using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Diagnostics;
using System.Threading;
using System.Threading.Tasks;
using ControlsDemo.Helpers;
using DevExpress.Mvvm;
using DevExpress.Mvvm.POCO;

namespace ControlsDemo.Wizard.WpfTour {
    public class WizardViewModel {
        public WizardViewModel() {
            WelcomePageViewModel = CreateViewModel<WelcomePageViewModel>();
            PlayTunePageViewModel = CreateViewModel<PlayTunePageViewModel>();
            ReadEulaPageViewModel = CreateViewModel<ReadEulaPageViewModel>();
            ConfirmPageViewModel = CreateViewModel<ConfirmPageViewModel>();
            NotSoFastPageViewModel = CreateViewModel<NotSoFastPageViewModel>();
            TimeConsumePageViewModel = CreateViewModel<TimeConsumePageViewModel>();
            RobotPageViewModel = CreateViewModel<RobotPageViewModel>();
            CongratulationsPageViewModel = CreateViewModel<CongratulationsPageViewModel>();

            ConfirmPageViewModel.TimeConsumePage = TimeConsumePageViewModel;
            ConfirmPageViewModel.NotSoFastPage = NotSoFastPageViewModel;

            Items = new ObservableCollection<object> {
                WelcomePageViewModel,
                PlayTunePageViewModel,
                ReadEulaPageViewModel,
                ConfirmPageViewModel,
                NotSoFastPageViewModel,
                TimeConsumePageViewModel,
                RobotPageViewModel,
                CongratulationsPageViewModel,
            };
        }

        public ConfirmPageViewModel ConfirmPageViewModel { get; private set; }
        public CongratulationsPageViewModel CongratulationsPageViewModel { get; private set; }
        public ObservableCollection<object> Items { get; private set; }
        public NotSoFastPageViewModel NotSoFastPageViewModel { get; private set; }
        public PlayTunePageViewModel PlayTunePageViewModel { get; private set; }
        public RobotPageViewModel RobotPageViewModel { get; private set; }
        public ReadEulaPageViewModel ReadEulaPageViewModel { get; private set; }
        public TimeConsumePageViewModel TimeConsumePageViewModel { get; private set; }
        public WelcomePageViewModel WelcomePageViewModel { get; private set; }
        public virtual WizardPageBase SelectedItem { get; set; }

        public static WizardViewModel Create() { return ViewModelSource.Create(() => new WizardViewModel()); }
        public void Cancel(CancelEventArgs e) {
            if(SelectedItem != null) SelectedItem.Cancel(e);
        }

        T CreateViewModel<T>() where T : WizardPageBase, new() {
            var viewModel = ViewModelSource.Create(() => new T());
            viewModel.SetParentViewModel(this);
            return viewModel;
        }
    }
    public abstract class WizardPageBase {
        protected WizardPageBase() {
            CanCancel = CanNext = CanBack = true;
            CanFinish = false;
            ShowNext = ShowBack = ShowCancel = true;
            ShowFinish = false;
        }

        public virtual bool CanBack { get; set; }
        public virtual bool CanCancel { get; set; }
        public virtual bool CanFinish { get; set; }
        public virtual bool CanNext { get; set; }
        public virtual string Description { get; set; }
        public virtual object GoToPage { get; set; }
        public virtual string Header { get { return string.Empty; } }
        public virtual bool ShowBack { get; set; }
        public virtual bool ShowCancel { get; set; }
        public virtual bool ShowFinish { get; set; }
        public virtual bool ShowNext { get; set; }

        public void Cancel(CancelEventArgs e) {
            OnCancel(e);
        }

        protected virtual void OnCancel(CancelEventArgs e) {
            if(this.GetService<IMessageBoxService>().
                ShowMessage("Do you want to exit the Wizard Control Feature Tour?", "Wizard Control", MessageButton.YesNo, MessageIcon.Question) == MessageResult.No)
                e.Cancel = true;
        }
    }
    public class WelcomePageViewModel : WizardPageBase {
        public WelcomePageViewModel() {
            CanBack = false;
            ShowBack = false;
        }

        public override string Header { get { return string.Empty; } }
    }
    public class PlayTunePageViewModel : WizardPageBase {
        public PlayTunePageViewModel() {
            Description = "To make this demo more entertaining, we would like to play a tune for you. Simple choose your favorite track.";
        }
        public override string Header { get { return "Step 2 - Play a tune"; } }
        public virtual string Song { get; set; }

        public bool CanPlay() {
            return !string.IsNullOrEmpty(Song);
        }
        public void Play() {
            string text = @"Sorry, but we don't have that song in our library..." + Environment.NewLine;
            text += @"But we are agree with you that ""{0}"" is an excellent choice.";
            text = string.Format(text, Song);
            this.GetService<IMessageBoxService>().ShowMessage(text, "Wizard Control", MessageButton.OK, MessageIcon.Information);
            this.GetService<IWizardService>().GoForward();
            Messenger.Default.Send(new SongMessage() { Song = Song });
        }
    }
    public class ReadEulaPageViewModel : WizardPageBase, ISupportWizardNextCommand {
        Stopwatch longTextTimer;

        public ReadEulaPageViewModel() {
            CanNext = false;
            Description = "Before proceeding, we want you to read and understand the following text, which is very long.";
        }

        public string Eula { get { return WizardDemoHelper.VeryLongText; } }
        public override string Header { get { return "Step 3 - The Read Some Very Long Text step"; } }
        public virtual bool IsAgreed { get; set; }
        bool ISupportWizardNextCommand.CanGoForward {
            get { return CanNext; }
        }

        public void StartTimer() {
            longTextTimer = Stopwatch.StartNew();
        }
        protected void OnIsAgreedChanged() {
            CanNext = IsAgreed;
        }
        void ISupportWizardNextCommand.OnGoForward(CancelEventArgs e) {
            var elapsed = (int)longTextTimer.Elapsed.TotalSeconds;
            if(elapsed < 60) {
                var result = this.GetService<IMessageBoxService>().
                    ShowMessage(string.Format("Are you sure that {0} seconds was enough time for you to read all that text?", (int)longTextTimer.Elapsed.TotalSeconds), "Wizard Control", MessageButton.YesNo, MessageIcon.Question);
                if(result == MessageResult.No)
                    e.Cancel = true;
            }
        }
    }
    public class ConfirmPageViewModel : WizardPageBase {
        public ConfirmPageViewModel() {
            ShowNoSoFastPage = true;
            GoToPage = ShowNoSoFastPage ? NotSoFastPage : TimeConsumePage;
        }

        public override string Header { get { return "Step 4 - Are You Tired Yet?"; } }
        public virtual object NotSoFastPage { get; set; }
        public virtual bool ShowNoSoFastPage { get; set; }
        public virtual object TimeConsumePage { get; set; }

        protected void OnShowNoSoFastPageChanged() {
            GoToPage = ShowNoSoFastPage ? NotSoFastPage : TimeConsumePage;
        }
    }
    public class NotSoFastPageViewModel : WizardPageBase {
        public override string Header { get { return "Not so Fast!"; } }
    }
    public class TimeConsumePageViewModel : WizardPageBase {
        public override string Header { get { return "Step 5 - Time-consuming Operation"; } }
        public virtual bool IsCompleted { get; set; }
        public int MaximumProgress { get { return 100; } }
        public int MinimumProgress { get { return 0; } }
        public virtual int Progress { get; set; }

        public void Clear() {
            Progress = 0;
            IsCompleted = CanNext = CanBack = false;
        }
        public Task StartProcess() {
            Clear();
            return Task.Factory.StartNew(Process);
        }
        void Process() {
            Thread.Sleep(TimeSpan.FromSeconds(1));
            var command = this.GetAsyncCommand(x => x.StartProcess());
            for(int i = MinimumProgress; i <= MaximumProgress; i++) {
                if(command.IsCancellationRequested)
                    break;
                Progress = i;
                Thread.Sleep(TimeSpan.FromSeconds(0.02));
            }
            IsCompleted = CanNext = CanBack = true;
        }
    }
    public class RobotPageViewModel : WizardPageBase {
        public override string Header { get { return "Step 6 - Are You a Robot?"; } }
        public virtual string Capture { get; set; }
        protected void OnCaptureChanged() {
            Messenger.Default.Send(new IsRobotMessage() { IsRobot = !Equals(Capture.ToLower(), "devexpress123") });
        }
    }
    public class CongratulationsPageViewModel : WizardPageBase {
        public CongratulationsPageViewModel() {
            CanFinish = true;
            CanCancel = false;
            ShowFinish = true;
            ShowNext = ShowCancel = false;
            IsRobot = true;
            UpdateDescription();

            Messenger.Default.Register<IsRobotMessage>(this, OnIsRobotMessage);
            Messenger.Default.Register<SongMessage>(this, OnSongMessage);
        }

        public virtual bool IsRobot { get; set; }
        public virtual string Song { get; set; }

        protected override void OnCancel(CancelEventArgs e) { }
        string GetArtistName(string value) {
            if(string.IsNullOrEmpty(value)) return string.Empty;
            return value.Substring(0, value.IndexOf("-") - 1);
        }
        void OnIsRobotMessage(IsRobotMessage obj) {
            IsRobot = obj.IsRobot;
            UpdateDescription();
        }
        void OnSongMessage(SongMessage obj) {
            Song = obj.Song;
            UpdateDescription();
        }
        void UpdateDescription() {
            if(IsRobot) {
                Description = "We are very sorry, but no robots are allowed to continue this wizard. Please click Finish to exit.";
            } else {
                string artist = GetArtistName(Song);
                Description = string.IsNullOrEmpty(artist) ?
                    "Thank you for completing this Wizard Control Feature Tour! We can now conclusively state that you are very patient, a quick reader and definitely not a robot." :
                    string.Format("Thank you for completing this Wizard Control Feature Tour! We can now conclusively state that you are very patient, definitely not a robot, a quick reader, and a fan of {0} just like we are.", artist);
            }
        }
    }
    public class IsRobotMessage {
        public bool IsRobot { get; set; }
    }
    public class SongMessage {
        public string Song { get; set; }
    }
}
