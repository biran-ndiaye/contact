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

namespace ContactsUI
{
    /// <summary>
    /// Logique d'interaction pour MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();

            //c'est juste des tests on doit interagir avec un DB
            string[] pays = {"All","Canada","Senegal", "Belgique","US" };
            foreach (string pay in pays)
            {
                this.comboBoxPays.Items.Add(pay);
            }

            String[] critereDeTri = { "Prenom", "Nom", "Date d'ajout","Numero"};
            foreach (string s in critereDeTri)
            {
                this.ComboBoxTri.Items.Add(s);
            }

        }

        private void TextBox_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            this.TextBoxRechercher.Text = "";
        }

        private void ComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }
    }
}
