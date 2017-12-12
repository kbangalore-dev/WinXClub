//---------------------------------------------------------------------------
//
// <copyright file="Season5ListPage.xaml.cs" company="Microsoft">
//    Copyright (C) 2015 by Microsoft Corporation.  All rights reserved.
// </copyright>
//
// <createdOn>8/11/2016 4:09:04 PM</createdOn>
//
//---------------------------------------------------------------------------

using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Navigation;
using Windows.UI.Xaml;
using AppStudio.DataProviders.YouTube;
using WinXClubHD.Sections;
using WinXClubHD.ViewModels;
using AppStudio.Uwp;

namespace WinXClubHD.Pages
{
    public sealed partial class Season5ListPage : Page
    {
	    public ListViewModel ViewModel { get; set; }
        public Season5ListPage()
        {
			ViewModel = ViewModelFactory.NewList(new Season5Section());

            this.InitializeComponent();
			commandBar.DataContext = ViewModel;
			NavigationCacheMode = NavigationCacheMode.Enabled;
            Microsoft.HockeyApp.HockeyClient.Current.TrackEvent(this.GetType().FullName);
        }

        protected override async void OnNavigatedTo(NavigationEventArgs e)
        {
			ShellPage.Current.ShellControl.SelectItem("00672bc0-6e53-474b-9e25-c2837343b861");
			ShellPage.Current.ShellControl.SetCommandBar(commandBar);
			if (e.NavigationMode == NavigationMode.New)
            {			
				await this.ViewModel.LoadDataAsync();
                this.ScrollToTop();
			}			
            base.OnNavigatedTo(e);
        }

    }
}
