using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Media.Animation;
using Windows.UI.Xaml.Navigation;
using TrustLightsCS;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace TrustLights
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class GroupsPage
    {
        private TrustDeviceGroup[] _groups;
        public GroupsPage()
        {
            InitializeComponent();
        }

        private void GroupsListView_OnLoaded(object sender, RoutedEventArgs e)
        {
            var groups = MainPage.Hub.Groups;
            _groups = groups;
            GroupsListView.ItemsSource = groups;
        }

        private void GroupsListView_OnSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            GroupPage.CurrentGroup = _groups[GroupsListView.SelectedIndex];
            MainPage.Self.NavView_Navigate("Group", new EntranceNavigationTransitionInfo());
        }
    }
}
