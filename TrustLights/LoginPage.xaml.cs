using System;
using System.Net;
using Windows.Storage;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Media.Animation;
using TrustLightsCS;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace TrustLights
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class LoginPage
    {
        public LoginPage()
        {
            InitializeComponent();
        }

        private async void LoginButtonClick(object sender, RoutedEventArgs e)
        {
            try
            {
                // check if the credentials are correct, if they're not, it'll throw an exception
                TrustHttp.LoginUser(TextBoxMacAddress.Text, TextBoxEmailAddress.Text, TextBoxPassword.Password);

                var localSettings = ApplicationData.Current.LocalSettings;
                localSettings.Values["MacAddress"] = TextBoxMacAddress.Text;
                localSettings.Values["EmailAddress"] = TextBoxEmailAddress.Text;
                localSettings.Values["Password"] = TextBoxPassword.Password;

                var settingsSavedDialog = new ContentDialog()
                {
                    Title = "Success",
                    Content = "Successfully logged in.",
                    CloseButtonText = "Ok"
                };
                await settingsSavedDialog.ShowAsync();
                MainPage.Hub = TrustHub.GetHub(TextBoxMacAddress.Text, TextBoxEmailAddress.Text, TextBoxPassword.Password);
                MainPage.Self.NavView_Navigate(MainPage.Self.GetCurrentlySelectedItemTag(), new EntranceNavigationTransitionInfo());
            }
            catch (WebException)
            {
                var settingsSavedDialog = new ContentDialog()
                {
                    Title = "Failed to authenticate",
                    Content = "Your password, email or mac address may be incorrect.",
                    CloseButtonText = "Ok"
                };
                await settingsSavedDialog.ShowAsync();
            }
        }
    }
}
