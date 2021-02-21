using System;
using System.Net;
using Windows.Storage;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using TrustLightsCS;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace TrustLights
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class SettingsPage
    {
        public SettingsPage()
        {
            this.InitializeComponent();
        }

        private async void CredentialsSaveButtonClick(object sender, RoutedEventArgs e)
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
                    Title = "Settings Saved",
                    Content = "Successfully saved your settings.",
                    CloseButtonText = "Ok"
                };
                await settingsSavedDialog.ShowAsync();
                MainPage.Hub = TrustHub.GetHub(TextBoxMacAddress.Text, TextBoxEmailAddress.Text, TextBoxPassword.Password);
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

        private void FrameworkElement_OnLoaded(object sender, RoutedEventArgs e)
        {
            var localSettings = ApplicationData.Current.LocalSettings;
            TextBoxMacAddress.Text = localSettings.Values["MacAddress"] as string ?? string.Empty;
            TextBoxEmailAddress.Text = localSettings.Values["EmailAddress"] as string ?? string.Empty;
            TextBoxPassword.Password = localSettings.Values["Password"] as string ?? string.Empty;
        }

        private async void LogoutButtonClick(object sender, RoutedEventArgs e)
        {
            var localSettings = ApplicationData.Current.LocalSettings;
            TextBoxMacAddress.Text
                = TextBoxEmailAddress.Text
                    = TextBoxPassword.Password
                        = string.Empty;
            localSettings.Values["MacAddress"] = TextBoxMacAddress.Text;
            localSettings.Values["EmailAddress"] = TextBoxEmailAddress.Text;
            localSettings.Values["Password"] = TextBoxPassword.Password;
            MainPage.Hub = null;

            var loggedOutDialog = new ContentDialog()
            {
                Title = "Logged out",
                Content = "Successfully logged out",
                CloseButtonText = "Ok"
            };
            await loggedOutDialog.ShowAsync();
        }
    }
}
