using ContactsModel;
using System;
using System.Collections.Generic;
using System.IO;
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

namespace ContactsUI
{
    /// <summary>
    /// Logique d'interaction pour EditContact.xaml
    /// </summary>
    public partial class EditContact : Window
    {
        public MainWindow mainWindow;
        public string nomFichier = @"C:\ISI\exercice\4 - C#\d05-e20-juillet-tp-blondel-biran\Gestionnaire Contatcts\LesPays.txt";
        public EditContact(MainWindow mainWindow)
        {
            InitializeComponent();
            this.Top = 80;
            this.Left = 300;

            this.mainWindow = mainWindow;

            //chargement des pays
            string[] lespays = File.ReadAllLines(nomFichier);
            foreach (string pays in lespays)
            {
                this.ComboBoxPays.Items.Add(pays);
            }

            //initialisation des differents champs avec le contact selelctionne
            Contacts contactsSelectionne = (Contacts)this.mainWindow.ListBoxContact.SelectedItem;
            this.TxtBoxPrenom.Text = contactsSelectionne.Prenom;
            this.TxBoxNom.Text = contactsSelectionne.Nom;
            this.TxtBoxCourriel.Text = contactsSelectionne.Courriel;
            DateTime date = (DateTime)contactsSelectionne.DateNaissance;
            this.TxtBoxDateDeNaissance.Text =date.Year+"-"+ (date.Month < 10 ? "0" : "") + date.Month + "-" + (date.Day < 10 ? "0" : "") + date.Day ;
            this.TxtBoxNumeroTelephone.Text = contactsSelectionne.numeroTelephone;

            this.TxtBoxProfession.Text = contactsSelectionne.Profession??"";
            this.TxtBoxCompany.Text = contactsSelectionne.Company??"";
            this.TxtBoxFax.Text = contactsSelectionne.Fax ?? "";

            this.TxtBoxAddresse.Text = contactsSelectionne.Addresse.Address??"";
            this.TxtBoxNumApp.Text =contactsSelectionne.Addresse.NumAppt.ToString()??"";
            this.ComboBoxPays.SelectedItem = contactsSelectionne.Addresse.Pays;
            this.TxtBoxVille.Text = contactsSelectionne.Addresse.Ville??"";
            this.TxtBoxCodePost.Text = contactsSelectionne.Addresse.CodePostal??"";

        }

        private void Button_Click_Enregistrer(object sender, RoutedEventArgs e)
        {
            Contacts contacts = (Contacts)this.mainWindow.ListBoxContact.SelectedItem;
            int selectedIndex = this.mainWindow.ListBoxContact.SelectedIndex;
            contacts.Prenom = GetSaisi(this.TxtBoxPrenom, this.lblPrenomRequi);
            contacts.Nom = GetSaisi(this.TxBoxNom, this.lblNomRequi);
            contacts.numeroTelephone = GetSaisi(this.TxtBoxNumeroTelephone, this.lblNumerTelRequis);
            contacts.Courriel = GetSaisi(this.TxtBoxCourriel, this.lblCourrielRequi);
            contacts.Fax = GetSaisi(this.TxtBoxFax, null);
            contacts.Company = GetSaisi(this.TxtBoxCompany, null);
            contacts.Profession = GetSaisi(this.TxtBoxProfession, null);
            

            //A checker
            if (GetSaisi(this.TxtBoxDateDeNaissance, this.lblDateDeNaissanceRequis) != null)
            {
                contacts.DateNaissance = Convert.ToDateTime(GetSaisi(this.TxtBoxDateDeNaissance, this.lblDateDeNaissanceRequis));
            }

            contacts.Addresse.Address = GetSaisi(this.TxtBoxAddresse, null);
            if (GetSaisi(this.TxtBoxNumApp, null) is null)
            {
                contacts.Addresse.NumAppt = null;
            }
            else
            {
                contacts.Addresse.NumAppt = Convert.ToInt32(GetSaisi(this.TxtBoxNumApp, null));
            }
            contacts.Addresse.Ville = GetSaisi(this.TxtBoxVille, null);
            contacts.Addresse.CodePostal = GetSaisi(this.TxtBoxCodePost, null);
            if(this.ComboBoxPays.SelectedItem != null)
            {
                contacts.Addresse.Pays = this.ComboBoxPays.SelectedItem.ToString();
            }

            //pour l'instant je considere pas l'enregistrement des adrees
            if (contacts.Prenom != null && contacts.Nom != null && contacts.numeroTelephone != null && contacts.Courriel != null && contacts.DateNaissance != null)
            {
                //on met a jour  la DB le contact modifier  
                this.mainWindow.ContactactsManger.ModifierContact(contacts);

                // de mettre a jour la Liste box
                this.mainWindow.mettreAJourListeContactApresTrie(this.mainWindow.ComboBoxTri.SelectedItem.ToString());

                // mettre a jour la liste info box
                this.mainWindow.TxtInfoContact.Text = "";
                this.mainWindow.TxtInfoContact.Text = contacts.AfficherContact();
                this.mainWindow.ListBoxContact.SelectedIndex = selectedIndex;

                // mis a jour des combobox

                //mis a jour des combobox
                this.mainWindow.mettreAjourCombobox(this.mainWindow.comboBoxPays, this.mainWindow.ContactactsManger.GetPays());
                this.mainWindow.mettreAjourCombobox(this.mainWindow.ComboBoxVille, this.mainWindow.ContactactsManger.GetVilles());
                this.mainWindow.mettreAjourCombobox(this.mainWindow.comboBoxProfession, this.mainWindow.ContactactsManger.GetProfessions());
                this.mainWindow.mettreAjourCombobox(this.mainWindow.comboBoxEntreprise, this.mainWindow.ContactactsManger.GetEntreprises());

                //mis a jour textBox s'il y a du text
                this.mainWindow.TextBoxRechercher.Text = "Rechercher...";

                this.Hide();
                this.mainWindow.Show();

            }
        }
        public string GetSaisi(TextBox txtSaisi, Label lblRequis)
        {
            string saisi = null;
            if (string.IsNullOrWhiteSpace(txtSaisi.Text) && lblRequis != null)
            {
                lblRequis.Content = "Champs obligatoire";
            }
            else
            {
                if (string.IsNullOrWhiteSpace(txtSaisi.Text))
                {
                    saisi = null;
                }
                else
                {
                    saisi = txtSaisi.Text.Trim();
                }
            }
            return saisi;
        }
        private void Button_Click_1_Annuler(object sender, RoutedEventArgs e)
        {
            this.mainWindow.TxtInfoContact.Text = "";
            this.mainWindow.BtnDelete.IsEnabled = false;
            this.mainWindow.BtnEdit.IsEnabled = false;
            this.mainWindow.mettreAJourListeContactApresTrie(this.mainWindow.ComboBoxTri.SelectedItem.ToString());
            this.mainWindow.TextBoxRechercher.Text = "Rechercher...";
            this.Hide();
            this.mainWindow.Show();
        }
    }
}
