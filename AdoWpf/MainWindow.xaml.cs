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
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Data.SqlClient;
using System.Configuration;
using System.Data.Common;
using AdoGemeenschap;

namespace AdoWpf
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void buttonStorten_Click(object sender, RoutedEventArgs e)
        {
            Decimal teStorten;
            if(decimal.TryParse(textBoxTeStorten.Text,out teStorten))
            {
                try
                {
                    var manager = new RekeningenManager();
                    if (manager.Storten(teStorten, textBoxRekengNr.Text))
                    {
                        labelStatus.Content = "OK";
                    }
                    else
                    {
                        labelStatus.Content = "Rekening niet gevonden";
                    }
                }
                catch (Exception ex)
                {
                    
                    labelStatus.Content = ex.Message;
                }
            }
            else
                labelStatus.Content = "Tik een getal bij het storten";
        }

      
    }
}
