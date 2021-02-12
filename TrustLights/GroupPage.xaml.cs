using Windows.UI.Xaml;
using TrustLightsCS;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace TrustLights
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class GroupPage
    {
        public static TrustDeviceGroup CurrentGroup;

        public GroupPage()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            var color = new TrustColorRgb(ColorPicker.Color.R, ColorPicker.Color.G, ColorPicker.Color.B);
            CurrentGroup.SetColor(color);
        }

        private void ButtonTurnOn(object sender, RoutedEventArgs e)
        {
            CurrentGroup.TurnOn();
        }

        private void ButtonTurnOff(object sender, RoutedEventArgs e)
        {
            CurrentGroup.TurnOff();
        }
    }
}
