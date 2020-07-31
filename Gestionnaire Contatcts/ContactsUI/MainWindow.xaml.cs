using ContactsBLLLib;
using ContactsModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Forms;
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
        public ContactactsManger ContactactsManger { get; set; }
        public List<Contacts> listContacts;
        public string[] critereDeTri;
        

        public MainWindow()
        {
            InitializeComponent();
            this.Top = 80;
            this.Left = 300;
            this.ContactactsManger = new ContactactsManger();

            //desactive les boutons Edit et delete si aucun Conatct n'est selectionne
            this.BtnDelete.IsEnabled = false;
            this.BtnEdit.IsEnabled = false;

            //Recuperer tous les Contacts dans une liste
            listContacts = this.ContactactsManger.GetContacts();


            
            //initialisations du combobox TriMulticritere
            this.critereDeTri = new string[]{ "Prenom Croissant","Prenom Decroissant", "Nom Croissant","Nom Decroissant","Date d'ajout"};

            foreach (string s in critereDeTri)
            {
                this.ComboBoxTri.Items.Add(s);
            }

          
        }

       
        //essayer de desactiver le clavier pour le champs de Information contact
        private void TextBox_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            this.TextBoxRechercher.Text = "";
        }

        

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            this.Hide();
            new AddNewContact(this).Show();
        }

       
        private void ListBoxContact_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            //on active les btns Edit et delete
            this.BtnDelete.IsEnabled = true;
            this.BtnEdit.IsEnabled = true;
            if (this.ListBoxContact.SelectedIndex != -1)
            {
                this.TxtInfoContact.Text = ((Contacts)this.ListBoxContact.SelectedItem).AfficherContact();  
            }
        }

        //supprimer contact
        private void BtnDelete_Click(object sender, RoutedEventArgs e)
        {
            string message = $"Voulez Vous supprimer ce supprimer  ({(Contacts)this.ListBoxContact.SelectedItem})?? ";
            string caption = "Error in input";
            MessageBoxButtons buttons = MessageBoxButtons.YesNo;
            DialogResult result;
            result = System.Windows.Forms.MessageBox.Show(message, caption, buttons);
            if (result == System.Windows.Forms.DialogResult.Yes)
            {
                Contacts contactASupprimer = (Contacts)this.ListBoxContact.SelectedItem;
                this.ListBoxContact.SelectedIndex = -1;
                this.ContactactsManger.suprrimerContact(contactASupprimer);
                if (this.ListBoxContact.Items.Count != 0)
                {
                    this.mettreAJourListeContactApresTrie(this.ComboBoxTri.SelectedItem.ToString());
                }
                this.TxtInfoContact.Text = "";
            } 
        }

        //Bouton modifier
        private void BtnEdit_Click(object sender, RoutedEventArgs e)
        {
            this.Hide();
            new EditContact(this).Show();
        }

        private void ComboBoxTri_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            this.mettreAJourListeContactApresTrie(this.ComboBoxTri.SelectedItem.ToString());
        }

        //fonction mettre a jour liste Contact
        public void mettreAJourListeContactApresTrie(string CritereDeTri)
        {
            //effacer le contenu de lstBox
            this.ListBoxContact.Items.Clear();

            //on trie le contact
            //afficher le nouveau contenu
            foreach (Contacts c in this.ContactactsManger.GetListeTriMultiCritere(CritereDeTri))
            {
                this.ListBoxContact.Items.Add(c);
            }
        }
    }
}
