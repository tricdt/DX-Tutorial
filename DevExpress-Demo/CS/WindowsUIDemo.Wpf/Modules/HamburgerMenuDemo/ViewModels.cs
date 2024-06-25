using DevExpress.Mvvm;
using DevExpress.Xpf.WindowsUI;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows.Input;
using System.Linq;
using DevExpress.Xpf.Grid;
using DevExpress.Mvvm.DataAnnotations;

namespace WindowsUIDemo {
#region HamburgerMenuItemsModels 
    public abstract class BottomBarItemViewModelBase : BindableBase {
        HamburgerMenuBottomBarItemPlacement _placement = HamburgerMenuBottomBarItemPlacement.Left;
        Uri _icon = null;

        public HamburgerMenuBottomBarItemPlacement Placement {
            get { return _placement; }
            set { SetProperty(ref _placement, value, "Placement"); }
        }
        public Uri Icon {
            get { return _icon; }
            set { SetProperty(ref _icon, value, "Icon"); }
        }

        public BottomBarItemViewModelBase(string icon) {
            Icon = HamburgerItemViewModelBase.GetIconUriFromName(icon);
        }
    }
    public class BottomBarNavigationItemModel : BottomBarItemViewModelBase {
        Type _navigationTargetType = null;
        ICommand _command = null;

        public Type NavigationTargetType {
            get { return _navigationTargetType; }
            set { SetProperty(ref _navigationTargetType, value, "NavigationTargetType"); }
        }
        public ICommand Command {
            get { return _command; }
            set { SetProperty(ref _command, value, "Command"); }
        }
 
        public BottomBarNavigationItemModel(string icon, Type navigationTargetType, ICommand command = null) : base(icon) {
            Command = command;
            NavigationTargetType = navigationTargetType;
        }
    }
    public class BottomBarCheckItemViewModel : BottomBarItemViewModelBase {
        bool? _isChecked = null;

        public bool? IsChecked {
            get { return _isChecked; }
            set { SetProperty(ref _isChecked, value, "IsChecked", OnIsCheckedChanged); }
        }

        Action<bool?> checkedChangedAction;

        public BottomBarCheckItemViewModel(string icon, Action<bool?> checkAction) : base(icon) {
            checkedChangedAction = checkAction;
            IsChecked = false;
        }

        protected virtual void OnIsCheckedChanged() {
            checkedChangedAction(IsChecked);
        }
    }
    public abstract class HamburgerItemViewModelBase : BindableBase {
        Type _navigationTargetType = null;
        object _content = null;
        Uri _icon = null;
        HamburgerMenuItemPlacement _placement = HamburgerMenuItemPlacement.Top;

        public Type NavigationTargetType {
            get { return _navigationTargetType; }
            set { SetProperty(ref _navigationTargetType, value, "NavigationTargetType"); }
        }
        public object Content {
            get { return _content; }
            set { SetProperty(ref _content, value, "Content"); }
        }
        public Uri Icon {
            get { return _icon; }
            set { SetProperty(ref _icon, value, "Icon"); }
        }
        public HamburgerMenuItemPlacement Placement {
            get { return _placement; }
            set { SetProperty(ref _placement, value, "Placement"); }
        }
        public HamburgerItemViewModelBase() {
            Placement = HamburgerMenuItemPlacement.Top;
        }

        internal static Uri GetIconUriFromName(string name) {
            if(string.IsNullOrEmpty(name))
                return null;
            return new Uri(string.Format("pack://application:,,,/WindowsUIDemo;component/Images/Hamburger/{0}.png", name), UriKind.Absolute);
        }
    }
    public abstract class HamburgerSubMenuItemViewModelBase : HamburgerItemViewModelBase {
        HamburgerSubMenuMoreButtonVisibility _moreButtonVisibilityMode = HamburgerSubMenuMoreButtonVisibility.Auto;
        bool _isStandaloneSelectionItemMode = true;

        public HamburgerSubMenuMoreButtonVisibility MoreButtonVisibilityMode {
            get { return _moreButtonVisibilityMode; }
            set { SetProperty(ref _moreButtonVisibilityMode, value, "MoreButtonVisibilityMode"); }
        }
        public bool IsStandaloneSelectionItemMode {
            get { return _isStandaloneSelectionItemMode; }
            set { SetProperty(ref _isStandaloneSelectionItemMode, value, "IsStandaloneSelectionItemMode"); }
        }

        public HamburgerSubMenuItemViewModelBase() : base() {
            MoreButtonVisibilityMode = HamburgerSubMenuMoreButtonVisibility.Auto;
        }
    }
    public class HamburgerSubMenuItemViewModel : HamburgerSubMenuItemViewModelBase {
        ReadOnlyCollection<HamburgerSubItemViewModel> _items;
        public ReadOnlyCollection<HamburgerSubItemViewModel> Items { get { return _items; } }

        public HamburgerSubMenuItemViewModel(object content, string icon, IList<HamburgerSubItemViewModel> items) {
            Content = content;
            Icon = GetIconUriFromName(icon);
            _items = new ReadOnlyCollection<HamburgerSubItemViewModel>(items);
        }
    }
    public class HamburgerThemeSwitcherItemViewModel : HamburgerSubMenuItemViewModelBase {
        public HamburgerThemeSwitcherItemViewModel(object content, string icon) {
            Content = content;
            Icon = GetIconUriFromName(icon);
        }
    }
    public abstract class HamburgerItemWithCommand : HamburgerItemViewModelBase {
        ICommand _command = null;
        object _commandParameter = null;

        public ICommand Command {
            get { return _command; }
            set { SetProperty(ref _command, value, "Command"); }
        }
        public object CommandParameter {
            get { return _commandParameter; }
            set { SetProperty(ref _commandParameter, value, "CommandParameter"); }
        }
    }

    public class HamburgerSubItemViewModel : HamburgerItemWithCommand {
        object _rightContent = null;
        bool _showInPreview = true;
        object _previewContent = null;
        bool _isSelected = false;

        public object RightContent {
            get { return _rightContent; }
            set { SetProperty(ref _rightContent, value, "RightContent"); }
        }
        public bool ShowInPreview {
            get { return _showInPreview; }
            set { SetProperty(ref _showInPreview, value, "ShowInPreview"); }
        }
        public object PreviewContent {
            get { return _previewContent; }
            set { SetProperty(ref _previewContent, value, "PreviewContent"); }
        }

        public bool IsSelected {
            get { return _isSelected; }
            set { SetProperty(ref _isSelected, value, "IsSelected"); }
        }

        public HamburgerSubItemViewModel(object content, object rightContent, Type navigationTargetType, bool showInPreview, object parentViewModel) {
            Content = content;
            RightContent = rightContent;
            NavigationTargetType = navigationTargetType;
            ShowInPreview = showInPreview;
        }
    }
    public class HamburgerLinkItemViewModel : HamburgerItemViewModelBase {
        Uri _navigationUri = null;

        public Uri NavigationUri {
            get { return _navigationUri; }
            set { SetProperty(ref _navigationUri, value, "NavigationUri"); }
        }

        public HamburgerLinkItemViewModel(object content, Uri navigationUri) {
            Content = content;
            NavigationUri = navigationUri;
        }
    }
    public class HamburgerNavigationItemViewModel : HamburgerItemWithCommand {
        bool _hideMenuWhenSelected = false;
        bool _selectOnClick = true;

        public bool HideMenuWhenSelected {
            get { return _hideMenuWhenSelected; }
            set { SetProperty(ref _hideMenuWhenSelected, value, "HideMenuWhenSelected"); }
        }
        public bool SelectOnClick {
            get { return _selectOnClick; }
            set { SetProperty(ref _selectOnClick, value, "SelectOnClick"); }
        }
 
        public HamburgerNavigationItemViewModel(object content, string icon, Type navigationTargetType, ICommand command = null) {
            Content = content;
            Icon = GetIconUriFromName(icon);
            NavigationTargetType = navigationTargetType;
            Command = command;
        }
    }
    #endregion
#region MainViewModel
    [POCOViewModel]
    public class HamburgerMenuDemoViewModel {
        public virtual Message FocusedRow { get; set; }
        public virtual string MailFilter { get; set; }
        public virtual string Header { get; set; }
        public virtual string DateColumnHeader { get; set; }
        public virtual bool ShowMenuOnEmptySpaceBarClick { get; set; }
        public virtual HamburgerMenuAvailableViewStates AvailableViewStates { get; set; }
        public virtual bool IsFlyoutViewStateEnabled { get; set; }
        public virtual bool IsOverlayViewStateEnabled { get; set; }
        public virtual bool IsInlineViewStateEnabled { get; set; }
        public virtual bool ShowRevealHighlightEffect { get; set; }

        public IEnumerable<Message> Data { get { return MailItems.Messages; } }
        public ReadOnlyCollection<HamburgerItemViewModelBase> Items { get; private set; }
        public ReadOnlyCollection<BottomBarItemViewModelBase> BottomBarItems { get; private set; }
        public ObservableCollection<CompactModeFilterItem> CompactFilterItems { get; private set; }

        public HamburgerMenuDemoViewModel() {
            ShowMenuOnEmptySpaceBarClick = true;
            IsInlineViewStateEnabled = true;
            IsOverlayViewStateEnabled = true;
            IsFlyoutViewStateEnabled = true;
            ShowRevealHighlightEffect = true;
            CompactFilterItems = new ObservableCollection<CompactModeFilterItem>() {
                    new CompactModeFilterItem() { DisplayValue = "All" },
                    new CompactModeFilterItem() { DisplayValue = "Unread" , EditValue = "[IsUnread] = True"}
            };
            Items = new ReadOnlyCollection<HamburgerItemViewModelBase>(CreateMenuItems());
            BottomBarItems = new ReadOnlyCollection<BottomBarItemViewModelBase>(CreateBottomBarItems());
            if (Data != null && Data.Any())
                FocusedRow = Data.First();
            UpdateMailFilter(MailType.Inbox);           
        }
        IList<BottomBarItemViewModelBase> CreateBottomBarItems() {
            return new List<BottomBarItemViewModelBase>();
        }
        void UpdateMailFilter(MailType mailType) {
            MailFilter = "[MailType] = '" + mailType + "'";
            switch(mailType) {
                case MailType.Deleted:
                    DateColumnHeader = "Deleted";
                    Header = "Trash";
                    break;
                case MailType.Sent:
                    DateColumnHeader = "Sent";
                    Header = "Sent";
                    break;
                case MailType.Draft:
                    DateColumnHeader = "Created";
                    Header = "Drafts";
                    break;
                default:
                    DateColumnHeader = "Received";
                    Header = "Inbox";
                    break;
            }
            if(CompactFilterItems != null) {
                CompactFilterItems.First().EditValue = MailFilter;
                CompactFilterItems[1].EditValue = MailFilter + " And [IsUnread] = True";
            }
            if (Data != null && Data.Any())
                FocusedRow = Data.First(p => p.MailType == mailType);
        }
        int GetMailCountByType(MailType type) {
            return Data.Count(p => p.MailType == type);
        }
        IList<HamburgerItemViewModelBase> CreateMenuItems() {
            var command = new DelegateCommand<MailType>((t) => UpdateMailFilter(t));
            var subItems = new List<HamburgerSubItemViewModel>() {
                new HamburgerSubItemViewModel("Inbox", GetMailCountByType(MailType.Inbox), null, true, this) { Command = command, CommandParameter = MailType.Inbox, IsSelected = true },
                new HamburgerSubItemViewModel("Drafts", GetMailCountByType(MailType.Draft), null, true, this) { Command = command, CommandParameter = MailType.Draft },
                new HamburgerSubItemViewModel("Sent", GetMailCountByType(MailType.Sent), null, true, this) { Command = command, CommandParameter = MailType.Sent  },
                new HamburgerSubItemViewModel("Trash", GetMailCountByType(MailType.Deleted), null, false, this) { Command = command, CommandParameter = MailType.Deleted }
            };
            var items = new List<HamburgerItemViewModelBase>() {
                new HamburgerSubMenuItemViewModel("Folders", "Folders", subItems),
                new HamburgerLinkItemViewModel("Additional Information", new Uri(@"https://documentation.devexpress.com/")) { Placement = HamburgerMenuItemPlacement.Bottom },
                new HamburgerThemeSwitcherItemViewModel("Color Scheme", "ThemeSelector") {
                    IsStandaloneSelectionItemMode = true,
                    MoreButtonVisibilityMode = HamburgerSubMenuMoreButtonVisibility.Hidden,
                },
            };
            return items;
        }
		
		protected void OnIsFlyoutViewStateEnabledChanged(bool oldValue) {
            if(!IsFlyoutViewStateEnabled && IsAvailableViewStateEmpty()) {
                IsFlyoutViewStateEnabled = true;
                return;
            }
            UpdateAvailableViewState();
        }
        protected void OnIsOverlayViewStateEnabledChanged(bool oldValue) {
            if(!IsOverlayViewStateEnabled && IsAvailableViewStateEmpty()) {
                IsOverlayViewStateEnabled = true;
                return;
            }
            UpdateAvailableViewState();
        }
        protected void OnIsInlineViewStateEnabledChanged(bool oldValue) {
            if(!IsInlineViewStateEnabled && IsAvailableViewStateEmpty()) {
                IsInlineViewStateEnabled = true;
                return;
            }
            UpdateAvailableViewState();
        }
        bool IsAvailableViewStateEmpty() {
            return !IsInlineViewStateEnabled && !IsOverlayViewStateEnabled && !IsFlyoutViewStateEnabled;
        }
        void UpdateAvailableViewState() {
            HamburgerMenuAvailableViewStates state = (HamburgerMenuAvailableViewStates)0;
            if(IsInlineViewStateEnabled)
                state = state | HamburgerMenuAvailableViewStates.Inline;
            if(IsOverlayViewStateEnabled)
                state = state | HamburgerMenuAvailableViewStates.Overlay;
            if(IsFlyoutViewStateEnabled)
                state = state | HamburgerMenuAvailableViewStates.Flyout;
            AvailableViewStates = state;
        }
    }
#endregion

}
