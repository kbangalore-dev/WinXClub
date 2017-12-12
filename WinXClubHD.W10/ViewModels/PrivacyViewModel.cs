using System;
using AppStudio.Uwp;

namespace WinXClubHD.ViewModels
{
    public class PrivacyViewModel : ObservableBase
    {
        public Uri Url
        {
            get
            {
                return new Uri(UrlText, UriKind.RelativeOrAbsolute);
            }
        }
        public string UrlText
        {
            get
            {
                return "http://1drv.ms/1jzSlV5";
            }
        }
    }
}

