using System;
using System.Collections.Generic;
using System.Diagnostics;
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
    public sealed partial class DevicesPage
    {
        private TrustDevice[] Modules;
        public DevicesPage()
        {
            this.InitializeComponent();
        }

        private void DevicesListView_OnLoaded(object sender, RoutedEventArgs e)
        {
            var modules = MainPage.Hub.Devices
                .Where(d => d.Data.module != null)
                .Where(d =>
                    d.DeviceType == TrustDeviceType.ZigbeeLight
                    || d.DeviceType == TrustDeviceType.Light
                );
            Modules = modules.ToArray();
            DevicesListView.ItemsSource = modules;
        }

        private void DevicesListView_OnSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            DevicePage.CurrentDevice = Modules[DevicesListView.SelectedIndex];
            MainPage.Self.NavView_Navigate("Device", new EntranceNavigationTransitionInfo());
        }
    }
}
