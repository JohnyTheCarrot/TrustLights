using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using TrustLightsCS;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace TrustLights
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class DevicePage : Page
    {
        public static TrustDevice CurrentDevice;

        public DevicePage()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            var color = new TrustColorRgb(ColorPicker.Color.R, ColorPicker.Color.G, ColorPicker.Color.B);
            CurrentDevice.SetColor(color);
        }
    }
}
