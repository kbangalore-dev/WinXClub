using System;
using System.Collections.Generic;
using AppStudio.Uwp;
using AppStudio.Uwp.Commands;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using WinXClubHD.Sections;
namespace WinXClubHD.ViewModels
{
    public class SearchViewModel : PageViewModelBase
    {
        public SearchViewModel() : base()
        {
			Title = "WinX Club HD";
            Season1 = ViewModelFactory.NewList(new Season1Section());
            Season2 = ViewModelFactory.NewList(new Season2Section());
            Season3 = ViewModelFactory.NewList(new Season3Section());
            Season4 = ViewModelFactory.NewList(new Season4Section());
            Season5 = ViewModelFactory.NewList(new Season5Section());
            Season6 = ViewModelFactory.NewList(new Season6Section());
            Season7 = ViewModelFactory.NewList(new Season7Section());
                        
        }

        private string _searchText;
        private bool _hasItems = true;

        public string SearchText
        {
            get { return _searchText; }
            set { SetProperty(ref _searchText, value); }
        }

        public bool HasItems
        {
            get { return _hasItems; }
            set { SetProperty(ref _hasItems, value); }
        }

		public ICommand SearchCommand
        {
            get
            {
                return new RelayCommand<string>(
                async (text) =>
                {
                    await SearchDataAsync(text);
                }, SearchViewModel.CanSearch);
            }
        }      
        public ListViewModel Season1 { get; private set; }
        public ListViewModel Season2 { get; private set; }
        public ListViewModel Season3 { get; private set; }
        public ListViewModel Season4 { get; private set; }
        public ListViewModel Season5 { get; private set; }
        public ListViewModel Season6 { get; private set; }
        public ListViewModel Season7 { get; private set; }
        public async Task SearchDataAsync(string text)
        {
            this.HasItems = true;
            SearchText = text;
            var loadDataTasks = GetViewModels()
                                    .Select(vm => vm.SearchDataAsync(text));

            await Task.WhenAll(loadDataTasks);
			this.HasItems = GetViewModels().Any(vm => vm.HasItems);
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
		private void CleanItems()
        {
            foreach (var vm in GetViewModels())
            {
                vm.CleanItems();
            }
        }
		public static bool CanSearch(string text) { return !string.IsNullOrWhiteSpace(text) && text.Length >= 3; }
    }
}
