using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using System.Windows.Input;
using AppStudio.Uwp;
using AppStudio.Uwp.Actions;
using AppStudio.Uwp.Navigation;
using AppStudio.Uwp.Commands;
using AppStudio.DataProviders;

using AppStudio.DataProviders.YouTube;
using AppStudio.DataProviders.LocalStorage;
using WinXClubHD.Sections;


namespace WinXClubHD.ViewModels
{
    public class MainViewModel : PageViewModelBase
    {
        public ListViewModel Season1 { get; private set; }
        public ListViewModel Season2 { get; private set; }
        public ListViewModel Season3 { get; private set; }
        public ListViewModel Season4 { get; private set; }
        public ListViewModel Season5 { get; private set; }
        public ListViewModel Season6 { get; private set; }
        public ListViewModel Season7 { get; private set; }
		public AdvertisingViewModel SectionAd { get; set; }

        public MainViewModel(int visibleItems) : base()
        {
            Title = "WinX Club HD";
			this.SectionAd = new AdvertisingViewModel();
            Season1 = ViewModelFactory.NewList(new Season1Section(), visibleItems);
            Season2 = ViewModelFactory.NewList(new Season2Section(), visibleItems);
            Season3 = ViewModelFactory.NewList(new Season3Section(), visibleItems);
            Season4 = ViewModelFactory.NewList(new Season4Section(), visibleItems);
            Season5 = ViewModelFactory.NewList(new Season5Section(), visibleItems);
            Season6 = ViewModelFactory.NewList(new Season6Section(), visibleItems);
            Season7 = ViewModelFactory.NewList(new Season7Section(), visibleItems);

            if (GetViewModels().Any(vm => !vm.HasLocalData))
            {
                Actions.Add(new ActionInfo
                {
                    Command = RefreshCommand,
                    Style = ActionKnownStyles.Refresh,
                    Name = "RefreshButton",
                    ActionType = ActionType.Primary
                });
            }
        }

		#region Commands
		public ICommand RefreshCommand
        {
            get
            {
                return new RelayCommand(async () =>
                {
                    var refreshDataTasks = GetViewModels()
                        .Where(vm => !vm.HasLocalData).Select(vm => vm.LoadDataAsync(true));

                    await Task.WhenAll(refreshDataTasks);
					LastUpdated = GetViewModels().OrderBy(vm => vm.LastUpdated, OrderType.Descending).FirstOrDefault()?.LastUpdated;
                    OnPropertyChanged("LastUpdated");
                });
            }
        }
		#endregion

        public async Task LoadDataAsync()
        {
            var loadDataTasks = GetViewModels().Select(vm => vm.LoadDataAsync());

            await Task.WhenAll(loadDataTasks);
			LastUpdated = GetViewModels().OrderBy(vm => vm.LastUpdated, OrderType.Descending).FirstOrDefault()?.LastUpdated;
            OnPropertyChanged("LastUpdated");
        }

        private IEnumerable<ListViewModel> GetViewModels()
        {
            yield return Season1;
            yield return Season2;
            yield return Season3;
            yield return Season4;
            yield return Season5;
            yield return Season6;
            yield return Season7;
        }
    }
}
