using System.Windows;
using Vuokratoimisto_projekti.Classes;

namespace Vuokratoimisto_projekti.Windows
{
    /// <summary>
    /// Interaction logic for AsiakasMuuttaminen.xaml
    /// </summary>
    public partial class AsiakasMuuttaminen : Window
    {
        public AsiakasMuuttaminen(Asiakas asiakas)
        {
            InitializeComponent();
            DataContext = asiakas;
        }

        private void Muuta_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = true;
        }

        private void Peruuta_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = false;
        }
    }
}
