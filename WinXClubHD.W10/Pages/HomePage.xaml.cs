//---------------------------------------------------------------------------
//
// <copyright file="HomePage.xaml.cs" company="Microsoft">
//    Copyright (C) 2015 by Microsoft Corporation.  All rights reserved.
// </copyright>
//
// <createdOn>8/11/2016 4:09:04 PM</createdOn>
//
//---------------------------------------------------------------------------

using System.Windows.Input;

using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Navigation;
using Windows.UI.Xaml;

using AppStudio.Uwp;
using AppStudio.Uwp.Commands;
using AppStudio.Uwp.Navigation;
using Microsoft.Advertising.WinRT.UI;

using WinXClubHD.ViewModels;

namespace WinXClubHD.Pages
{
    public sealed partial class HomePage : Page
    {
        InterstitialAd MyVideoAd;
        public HomePage()
        {
            // Kiran Adunit data
            var MyAppID = "9nblggh5xvdm";
            var MyAdUnitId = "11670888";

            //test Adunit data
           // var MyAppID = "d25517cb-12d4-4699-8bdc-52040c712cab";
           // var MyAdUnitId = "11389925";


            // instantiate an InterstitialAd
            MyVideoAd = new InterstitialAd();

            // wire up all 4 events, see below for function templates
            MyVideoAd.AdReady += MyVideoAd_AdReady;
            MyVideoAd.ErrorOccurred += MyVideoAd_ErrorOccurred;
            MyVideoAd.Completed += MyVideoAd_Completed;
            MyVideoAd.Cancelled += MyVideoAd_Cancelled;
  

            // pre-fetch an ad 30-60 seconds before you need it
            MyVideoAd.RequestAd(AdType.Video, MyAppID, MyAdUnitId);

            ViewModel = new MainViewModel(12);            
            InitializeComponent();
            NavigationCacheMode = NavigationCacheMode.Required;
			commandBar.DataContext = ViewModel;
			searchBox.SearchCommand = SearchCommand;
			this.SizeChanged += OnSizeChanged;
            Microsoft.HockeyApp.HockeyClient.Current.TrackEvent(this.GetType().FullName);
        }		
        public MainViewModel ViewModel { get; set; }

        protected async override void OnNavigatedTo(NavigationEventArgs e)
        {
            await this.ViewModel.LoadDataAsync();
			//Page cache requires set commandBar in code
			ShellPage.Current.ShellControl.SetCommandBar(commandBar);
            ShellPage.Current.ShellControl.SelectItem("Home");
        }

		private void OnSizeChanged(object sender, SizeChangedEventArgs e)
        {
            searchBox.SearchWidth = e.NewSize.Width > 640 ? 230 : 190;
        }

		public ICommand SearchCommand
        {
            get
            {
                return new RelayCommand<string>(text =>
                {
                    searchBox.Reset();
                    ShellPage.Current.ShellControl.CloseLeftPane();                    
                    NavigationService.NavigateToPage("SearchPage", text, true);
                },
                SearchViewModel.CanSearch);
            }
        }

        void MyVideoAd_AdReady(object sender, object e)
        {
            // code
            var A = MyVideoAd.State;
            MyVideoAd.Show();

        }

        void MyVideoAd_ErrorOccurred(object sender, AdErrorEventArgs e)
        {
            // code
            var A = MyVideoAd.State;
        }

        void MyVideoAd_Completed(object sender, object e)
        {
            // code
            var A = MyVideoAd.State;
        }

        void MyVideoAd_Cancelled(object sender, object e)
        {
            // code
            var A = MyVideoAd.State;
        }

    }
}
