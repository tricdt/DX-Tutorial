using System;
using System.ComponentModel;
using DevExpress.VideoRent.ViewModel.ViewModelBase;

namespace DevExpress.VideoRent.ViewModel {
    public interface IMovieEditObjectParent : IVRObjectEditObjectParent<Movie> { }
    public sealed class MovieEditObject : VRObjectEditObject<Movie> {
        public static readonly TimeSpan DefaultRunTimeIncrement = new TimeSpan(0, 1, 0);
        public static readonly TimeSpan DefaultRunTimeDecrement = new TimeSpan(0, -1, 0);
        string directors;

        public MovieEditObject(EditableObject parent, Guid movieOid)
            : base(parent, movieOid) {
            Update();
        }
        public new IMovieEditObjectParent VideoRentEditObjectParent { get { return (IMovieEditObjectParent)Parent; } }
        public string Directors {
            get { return directors; }
            set { SetValue<string>("Directors", ref directors, value); }
        }
        public void AddToRunTime(TimeSpan increment) {
            if(VideoRentObject.RunTime != null)
                VideoRentObject.RunTime = ((TimeSpan)VideoRentObject.RunTime).Add(increment);
        }
        protected override void UpdateOverride() {
            base.UpdateOverride();
            UpdateDirectors();
        }
        protected override void SubscribeToChanged() {
            base.SubscribeToChanged();
            VideoRentObject.Artists.ListChanged += OnMovieArtistsListChanged;
        }
        protected override void UnsubscribeFromChanged() {
            VideoRentObject.Artists.ListChanged -= OnMovieArtistsListChanged;
            base.UnsubscribeFromChanged();
        }
        void OnMovieArtistsListChanged(object sender, ListChangedEventArgs e) { UpdateDirectors(); }
        void UpdateDirectors() {
            Directors = VideoRentObject == null ? string.Empty : VideoRentObject.Directors;
        }
        #region Commands
        public Action<object> CommandUpRunTime { get { return DoCommandUpRunTime; } }
        void DoCommandUpRunTime(object p) { AddToRunTime(DefaultRunTimeIncrement); }
        public Action<object> CommandDownRunTime { get { return DoCommandDownRunTime; } }
        void DoCommandDownRunTime(object p) { AddToRunTime(DefaultRunTimeDecrement); }
        #endregion
    }
}
