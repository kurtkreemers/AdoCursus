using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using AdoGemeenschap;

namespace AdoWpf
{
    /// <summary>
    /// Interaction logic for KlantToevoegen.xaml
    /// </summary>
    public partial class KlantToevoegen : Window
    {
        public KlantToevoegen()
        {
            InitializeComponent();
        }

        private void buttonToevoegen_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                var manager = new KlantenManager();
                labelStatus.Content = manager.NieuweKlant(textBoxNaam.Text).ToString();
            }
            catch (Exception ex )
            {

                labelStatus.Content = ex.Message; 
            }
        }
    }
}
