using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using Windows.Storage;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Media.Animation;
using Windows.UI.Xaml.Navigation;
using TrustHub = TrustLightsCS.TrustHub;

namespace TrustLights
{
    public sealed partial class MainPage
    {
        public static MainPage Self;
        public static TrustHub Hub;

        public MainPage()
        {
            InitializeComponent();

            Self = this;
            TryLogin();
        }

        public static bool TryLogin()
        {
            var localSettings = ApplicationData.Current.LocalSettings;
            var mac = localSettings.Values["MacAddress"] as string;
            var email = localSettings.Values["EmailAddress"] as string;
            var password = localSettings.Values["Password"] as string;

            if (mac == null || email == null || password == null)
                return false;
            try
            {
                Hub = TrustHub.GetHub(mac, email, password);
                return true;
            }
            catch (WebException)
            {
                return false;
            }
        }

        private readonly List<(string Tag, Type Page)> _pages = new List<(string Tag, Type Page)>
        {
            ("Groups", typeof(GroupsPage)),
            ("Devices", typeof(DevicesPage)),
            ("Device", typeof(DevicePage)),
            ("Group", typeof(GroupPage))
        };


        private void NavView_Loaded(object sender, RoutedEventArgs e)
        {
            ContentFrame.Navigated += ContentFrameOnNavigated;

            // NavView doesn't load any page by default, so load home page.
            NavView.SelectedItem = NavView.MenuItems[0];
            // If navigation occurs on SelectionChanged, this isn't needed.
            // Because we use ItemInvoked to navigate, we need to call Navigate
            // here to load the Groups page.
            NavView_Navigate("Groups", new EntranceNavigationTransitionInfo());
        }

        public string GetCurrentlySelectedItemTag()
        {
            return ((NavigationViewItem) NavView.SelectedItem).Tag.ToString();
        }

        public void NavView_Navigate(string navItemTag, NavigationTransitionInfo transitionInfo)
        {
            Type page;
            if (navItemTag == "settings")
            {
                page = typeof(SettingsPage);
            } else if (Hub == null)
            {
                page = typeof(LoginPage);
                NavView.Header = GetCurrentlySelectedItemTag();
            }
            else
            {
                var item = _pages.FirstOrDefault(p => p.Tag.Equals(navItemTag));
                page = item.Page;
            }
            // Get the page type before navigation so you can prevent duplicate
            // entries in the backstack.
            var preNavPageType = ContentFrame.CurrentSourcePageType;

            // Only navigate if the selected page isn't currently loaded.
            if (!(page is null) && !Type.Equals(preNavPageType, page))
            {
                ContentFrame.Navigate(page, null, transitionInfo);
            }
        }

        private void ContentFrameOnNavigated(object sender, NavigationEventArgs e)
        {
            NavView.IsBackEnabled = ContentFrame.CanGoBack;

            if (ContentFrame.SourcePageType == typeof(SettingsPage))
            {
                // SettingsItem is not part of NavView.MenuItems, and doesn't have a Tag.
                NavView.SelectedItem = (NavigationViewItem)NavView.SettingsItem;
                NavView.Header = "Settings";
            }
            else if (ContentFrame.SourcePageType != null)
            {
                var item = _pages.FirstOrDefault(p => p.Page == e.SourcePageType);

                try
                {
                    NavView.SelectedItem = NavView.MenuItems
                        .OfType<NavigationViewItem>()
                        .First(n => n.Tag.Equals(item.Tag));

                    NavView.Header =
                        ((NavigationViewItem) NavView.SelectedItem)?.Content?.ToString();
                } catch(InvalidOperationException)
                { }
            }
        }

        private void NavView_OnItemInvoked(NavigationView sender, NavigationViewItemInvokedEventArgs args)
        {
            if (args.IsSettingsInvoked)
            {
                NavView_Navigate("settings", args.RecommendedNavigationTransitionInfo);
            }
            else if (args.InvokedItemContainer != null)
            {
                var navItemTag = args.InvokedItemContainer.Tag.ToString();
                NavView_Navigate(navItemTag, args.RecommendedNavigationTransitionInfo);
            }
        }
    }
}
