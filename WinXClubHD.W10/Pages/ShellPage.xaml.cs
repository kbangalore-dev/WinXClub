using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;

using Windows.Foundation;
using Windows.UI.Core;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Navigation;
using Windows.UI.ViewManagement;
using Windows.UI.Xaml.Media.Imaging;

using AppStudio.Uwp;
using AppStudio.Uwp.Controls;
using AppStudio.Uwp.Navigation;

using WinXClubHD.Navigation;

namespace WinXClubHD.Pages
{
    public sealed partial class ShellPage : Page
    {
        public static ShellPage Current { get; private set; }

        public ShellControl ShellControl
        {
            get { return shell; }
        }

        public Frame AppFrame
        {
            get { return frame; }
        }

        public ShellPage()
        {
            InitializeComponent();

            this.DataContext = this;
            ShellPage.Current = this;

            this.SizeChanged += OnSizeChanged;
            if (SystemNavigationManager.GetForCurrentView() != null)
            {
                SystemNavigationManager.GetForCurrentView().BackRequested += ((sender, e) =>
                {
                    if (SupportFullScreen && ShellControl.IsFullScreen)
                    {
                        e.Handled = true;
                        ShellControl.ExitFullScreen();
                    }
                    else if (NavigationService.CanGoBack())
                    {
                        NavigationService.GoBack();
                        e.Handled = true;
                    }
                });
				
                NavigationService.Navigated += ((sender, e) =>
                {
                    if (NavigationService.CanGoBack())
                    {
                        SystemNavigationManager.GetForCurrentView().AppViewBackButtonVisibility = AppViewBackButtonVisibility.Visible;
                    }
                    else
                    {
                        SystemNavigationManager.GetForCurrentView().AppViewBackButtonVisibility = AppViewBackButtonVisibility.Collapsed;
                    }
                });
            }
        }

		public bool SupportFullScreen { get; set; }

		#region NavigationItems
        public ObservableCollection<NavigationItem> NavigationItems
        {
            get { return (ObservableCollection<NavigationItem>)GetValue(NavigationItemsProperty); }
            set { SetValue(NavigationItemsProperty, value); }
        }

        public static readonly DependencyProperty NavigationItemsProperty = DependencyProperty.Register("NavigationItems", typeof(ObservableCollection<NavigationItem>), typeof(ShellPage), new PropertyMetadata(new ObservableCollection<NavigationItem>()));
        #endregion

		protected override void OnNavigatedTo(NavigationEventArgs e)
        {
#if DEBUG
            ApplicationView.GetForCurrentView().SetPreferredMinSize(new Size { Width = 320, Height = 500 });
#endif
            NavigationService.Initialize(typeof(ShellPage), AppFrame);
			NavigationService.NavigateToPage<HomePage>(e);

            InitializeNavigationItems();

            Bootstrap.Init();
        }		        
		
		#region Navigation
        private void InitializeNavigationItems()
        {
            NavigationItems.Add(AppNavigation.NodeFromAction(
				"Home",
                "Home",
                (ni) => NavigationService.NavigateToRoot(),
                AppNavigation.IconFromSymbol(Symbol.Home)));
            NavigationItems.Add(AppNavigation.NodeFromAction(
				"ba37b286-7222-4872-9f42-d1831a2ebc9a",
                "Season 1",                
                AppNavigation.ActionFromPage("Season1ListPage"),
				AppNavigation.IconFromGlyph("\ue173")));

            NavigationItems.Add(AppNavigation.NodeFromAction(
				"686a2f73-9d0d-4100-9716-6e327e708bd4",
                "Season 2",                
                AppNavigation.ActionFromPage("Season2ListPage"),
				AppNavigation.IconFromGlyph("\ue173")));

            NavigationItems.Add(AppNavigation.NodeFromAction(
				"0c78e606-15f4-460b-8329-813f942f926c",
                "Season 3",                
                AppNavigation.ActionFromPage("Season3ListPage"),
				AppNavigation.IconFromGlyph("\ue173")));

            NavigationItems.Add(AppNavigation.NodeFromAction(
				"a9e874c9-960f-4cad-99dd-86defd302e98",
                "Season 4",                
                AppNavigation.ActionFromPage("Season4ListPage"),
				AppNavigation.IconFromGlyph("\ue173")));

            NavigationItems.Add(AppNavigation.NodeFromAction(
				"00672bc0-6e53-474b-9e25-c2837343b861",
                "Season 5",                
                AppNavigation.ActionFromPage("Season5ListPage"),
				AppNavigation.IconFromGlyph("\ue173")));

            NavigationItems.Add(AppNavigation.NodeFromAction(
				"a8c96d2e-7f48-43e7-bdfe-ae049c505c12",
                "Season 6",                
                AppNavigation.ActionFromPage("Season6ListPage"),
				AppNavigation.IconFromGlyph("\ue173")));

            NavigationItems.Add(AppNavigation.NodeFromAction(
				"8b5586d7-a280-450c-a648-5e594308ce90",
                "Season 7",                
                AppNavigation.ActionFromPage("Season7ListPage"),
				AppNavigation.IconFromGlyph("\ue173")));

            NavigationItems.Add(NavigationItem.Separator);

            NavigationItems.Add(AppNavigation.NodeFromControl(
				"About",
                "NavigationPaneAbout".StringResource(),
                new AboutPage(),
                AppNavigation.IconFromImage(new Uri("ms-appx:///Assets/about.png"))));
        }        
        #endregion        

		private void OnSizeChanged(object sender, SizeChangedEventArgs e)
        {
            UpdateDisplayMode(e.NewSize.Width);
        }

        private void UpdateDisplayMode(double? width = null)
        {
            if (width == null)
            {
                width = Window.Current.Bounds.Width;
            }
            this.ShellControl.DisplayMode = width > 640 ? SplitViewDisplayMode.CompactOverlay : SplitViewDisplayMode.Overlay;
            this.ShellControl.CommandBarVerticalAlignment = width > 640 ? VerticalAlignment.Top : VerticalAlignment.Bottom;
        }

        private async void OnKeyUp(object sender, KeyRoutedEventArgs e)
        {
            if (e.Key == Windows.System.VirtualKey.F11)
            {
                if (SupportFullScreen)
                {
                    await ShellControl.TryEnterFullScreenAsync();
                }
            }
            else if (e.Key == Windows.System.VirtualKey.Escape)
            {
                if (SupportFullScreen && ShellControl.IsFullScreen)
                {
                    ShellControl.ExitFullScreen();
                }
                else
                {
                    NavigationService.GoBack();
                }
            }
        }
    }
}
