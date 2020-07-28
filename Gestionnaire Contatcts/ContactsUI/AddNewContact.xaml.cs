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
        public AddNewContact(MainWindow window)
        {
            InitializeComponent();
            this.Top = 80;
            this.Left = 300;
            this.mainWindow = window;

            //Recharger directement les contacts qui sont dans la base de donnee
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
            
            //A checker
            if (GetSaisi(this.TxtBoxDateDeNaissance, this.lblDateDeNaissanceRequis) != null)
            {
                contacts.DateNaissance =Convert.ToDateTime( GetSaisi(this.TxtBoxDateDeNaissance, this.lblDateDeNaissanceRequis));
            }

            //on suppose d'abord aue l'addresse est null
            contacts.Addresse = null;
            //pour l'instant je considere pas l'enregistrement des adrees
            if (contacts.Prenom != null && contacts.Nom != null && contacts.numeroTelephone != null && contacts.Courriel!= null && contacts.DateNaissance != null)
            {



                //on ajoute a la DB le contact ajoute  
                this.mainWindow.ContactactsManger.AjouterContacts(contacts);

                //on recupere toute la liste des personness des Contacts dans La DB
                //clear La liste BoX
                //ON Ajoute la nouvelle liste
                

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
        public string GetSaisi(TextBox txtSaisi ,Label lblRequis )
        {
            string saisi = null;
            if (string.IsNullOrWhiteSpace(txtSaisi.Text) && lblRequis !=  null ) 
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
            this.Hide();
            this.mainWindow.Show();
        }
    }
}
