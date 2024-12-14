using Sales_Manager.Models.Common;
using Sales_Manager.ViewModels;
using System.Windows;

namespace Sales_Manager.Modules.Common
{
    internal static class GlobalApi
    {
        internal static MainWindowViewModel GetCurrentMainWindowViewModel()
        {
            var app = Application.Current as App;
            return app.MainVM;
        }

        internal static ResourceDictionary? LoadLanguageResourceDictionary(MetaData.Languages lang = MetaData.Languages.English)
        {
            try
            {
                Uri dictionary_uri = new($@"/UI/Languages/{lang}.xaml", UriKind.Relative);
                return (ResourceDictionary)Application.LoadComponent(dictionary_uri);
            }
            catch { return null; }
        }
    }
}
