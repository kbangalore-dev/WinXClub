using System;
using System.Threading.Tasks;
using System.Collections.Generic;
using AppStudio.DataProviders;
using AppStudio.DataProviders.Core;
using AppStudio.DataProviders.YouTube;
using AppStudio.Uwp.Actions;
using AppStudio.Uwp.Commands;
using AppStudio.Uwp;
using System.Linq;

using WinXClubHD.Navigation;
using WinXClubHD.ViewModels;

namespace WinXClubHD.Sections
{
    public class Season5Section : Section<YouTubeSchema>
    {
		private YouTubeDataProvider _dataProvider;
		
		public Season5Section()
		{
			_dataProvider = new YouTubeDataProvider(new YouTubeOAuthTokens
			{
				ApiKey = "AIzaSyD4IQJU9vG5cZDyBfx1swrAzwgsy-wMnEY"
			});
		}

		public override async Task<IEnumerable<YouTubeSchema>> GetDataAsync(SchemaBase connectedItem = null)
        {
            var config = new YouTubeDataConfig
            {
                QueryType = YouTubeQueryType.Playlist,
                Query = @"PLNDEdDyEUuMqYVZ73GEd-6dsjvgQFyOBe",
            };
            return await _dataProvider.LoadDataAsync(config, MaxRecords);
        }

        public override async Task<IEnumerable<YouTubeSchema>> GetNextPageAsync()
        {
            return await _dataProvider.LoadMoreDataAsync();
        }

        public override bool HasMorePages
        {
            get
            {
                return _dataProvider.HasMoreItems;
            }
        }

        public override ListPageConfig<YouTubeSchema> ListPage
        {
            get 
            {
                return new ListPageConfig<YouTubeSchema>
                {
                    Title = "Season 5",

                    Page = typeof(Pages.Season5ListPage),

                    LayoutBindings = (viewModel, item) =>
                    {
                        viewModel.Title = item.Title.ToSafeString();
                        viewModel.ImageUrl = ItemViewModel.LoadSafeUrl(item.ImageUrl.ToSafeString());
                    },
                    DetailNavigation = (item) =>
                    {
						return NavInfo.FromPage<Pages.Season5DetailPage>(true);
                    }
                };
            }
        }

        public override DetailPageConfig<YouTubeSchema> DetailPage
        {
            get
            {
                var bindings = new List<Action<ItemViewModel, YouTubeSchema>>();
                bindings.Add((viewModel, item) =>
                {
                    viewModel.PageTitle = item.Title.ToSafeString();
                    viewModel.Title = null;
                    viewModel.Description = item.Summary.ToSafeString();
                    viewModel.ImageUrl = ItemViewModel.LoadSafeUrl(null);
                    viewModel.Content = item.EmbedHtmlFragment;
					viewModel.Source = item.ExternalUrl;
                });

                var actions = new List<ActionConfig<YouTubeSchema>>
                {
                    ActionConfig<YouTubeSchema>.Link("Go To Source", (item) => item.ExternalUrl.ToSafeString()),
                };

                return new DetailPageConfig<YouTubeSchema>
                {
                    Title = "Season 5",
                    LayoutBindings = bindings,
                    Actions = actions
                };
            }
        }
    }
}
