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
    /// Interaction logic for AddNewContact.xaml
    /// </summary>
    public partial class AddNewContact : Window
    {
        public MainWindow mainWindow;
        //A modifier lors de la compilation chemin acces du fichier dans le prjet  
        public string nomFichier = @"C:\ISI\exercice\4 - C#\d05-e20-juillet-tp-blondel-biran\Gestionnaire Contatcts\LesPays.txt";
        public AddNewContact(MainWindow window)
        {
            InitializeComponent();
            this.Top = 80;
            this.Left = 300;
            this.mainWindow = window;

            //chargement des pays
            string[] lespays = File.ReadAllLines(nomFichier);
            foreach (string pays in lespays)
            {
                this.ComboBoxPays.Items.Add(pays);
            }
        }




        //bouton ajouter
        private void Button_Click_Ajouter(object sender, RoutedEventArgs e)
        {
            Contacts contacts = new Contacts();
            contacts.Prenom = GetSaisi(this.TxtBoxPrenom, this.lblPrenomRequi);
            contacts.Nom = GetSaisi(this.TxBoxNom, this.lblNomRequi);
            contacts.numeroTelephone = GetSaisi(this.TxtBoxNumeroTelephone, this.lblNumerTelRequis);
            contacts.Courriel = GetSaisi(this.TxtBoxCourriel, this.lblCourrielRequi);
            contacts.Fax = GetSaisi(this.TxtBoxFax, null);
            contacts.Company = GetSaisi(this.TxtBoxCompany, null);
            contacts.Profession = GetSaisi(this.TxtBoxProfession, null);
            contacts.compte.Id = this.mainWindow.compte.Id;
            //A checker
            if (GetSaisi(this.TxtBoxDateDeNaissance, this.lblDateDeNaissanceRequis) != null)
            {
                contacts.DateNaissance = Convert.ToDateTime(GetSaisi(this.TxtBoxDateDeNaissance, this.lblDateDeNaissanceRequis));
                if (((DateTime)contacts.DateNaissance).Year < 1800 || (contacts.DateNaissance) > DateTime.Now)
                {
                    this.lblDateDeNaissanceRequis.Content = "Date Saisie invalide";
                    contacts.DateNaissance = null;
                }

            }

            //cas des Address ( pas optimisee)
            contacts.Addresse.Address = GetSaisi(this.TxtBoxAddresse, null);
            contacts.Addresse.NumAppt = Convert.ToInt32(GetSaisi(this.TxtBoxNumApp, null));
            contacts.Addresse.Ville = GetSaisi(this.TxtBoxVille, null);
            contacts.Addresse.CodePostal = GetSaisi(this.TxtBoxCodePost, null);
            contacts.Addresse.Pays = this.ComboBoxPays.SelectedItem.ToString();

            //pour l'instant je considere pas l'enregistrement des adrees
            if (contacts.Prenom != null && contacts.Nom != null && contacts.numeroTelephone != null && contacts.Courriel != null && contacts.DateNaissance != null)
            {
                //on ajoute a la DB le contact ajoute  
                this.mainWindow.ContactactsManger.AjouterContacts(contacts);
                //mis a jour de la liste des contacts
                this.mainWindow.mettreAJourListeContactApresTrie(this.mainWindow.ComboBoxTri.SelectedItem.ToString());

                //mis a jour des combobox
                this.mainWindow.mettreAjourCombobox(this.mainWindow.comboBoxPays, this.mainWindow.ContactactsManger.GetPays(this.mainWindow.compte));
                this.mainWindow.mettreAjourCombobox(this.mainWindow.ComboBoxVille, this.mainWindow.ContactactsManger.GetVilles(this.mainWindow.compte));
                this.mainWindow.mettreAjourCombobox(this.mainWindow.comboBoxProfession, this.mainWindow.ContactactsManger.GetProfessions(this.mainWindow.compte));
                this.mainWindow.mettreAjourCombobox(this.mainWindow.comboBoxEntreprise, this.mainWindow.ContactactsManger.GetEntreprises(this.mainWindow.compte));

                //mis a jour textBox s'il y a du text
                this.mainWindow.TextBoxRechercher.Text = "Rechercher...";

                this.Hide();
                this.mainWindow.Show();
            }



        }

        //recuperer la chaine saisie pour les valuers obligatoire
        /// <summary>
        ///     fonction qui permet de simplifier le code 
        /// </summary>
        /// <param name="txtSaisi"> text saaisi dans un champs</param>
        /// <param name="champsObligatoire">si c'est obligatoire (true) ou optionnel (false)</param>
        /// <returns></returns>
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

        private void Button_Click_Annuler(object sender, RoutedEventArgs e)
        {
            this.mainWindow.mettreAJourListeContactApresTrie(this.mainWindow.ComboBoxTri.SelectedItem.ToString());
            this.mainWindow.TextBoxRechercher.Text = "Rechercher...";
            this.Hide();
            this.mainWindow.Show();
        }
    }
}
