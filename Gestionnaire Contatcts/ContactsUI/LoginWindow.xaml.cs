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
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;


namespace ContactsUI
{
    /// <summary>
    /// Logique d'interaction pour LoginWindow.xaml
    /// </summary>
    public partial class LoginWindow : Window
    {
        public ContactactsManger contactactsManger;
        
        public LoginWindow()
        {
            InitializeComponent();
            this.contactactsManger = new ContactactsManger();
            this.Top = 80;
            this.Left = 350;
        }

        private void BtnSignIn_Click(object sender, RoutedEventArgs e)
        {
            //ajouter un bouton deconnecter si possible
            string userName = this.TxtBoxUserName.Text.Trim();
            string password = this.PassWordBox.Password;
            Compte compte = new Compte {UserName =userName,Password=password };  
            //verifier si ce compte existe ou pas 
            List<Compte> Users = this.contactactsManger.GetUsers();
            int i = 0;
            bool trouve = false;
            for ( i = 0; i < Users.Count; i++)
            {
                if (Users[i].UserName.CompareTo(userName) == 0 && Users[i].Password.CompareTo(password) == 0)
                {
                    trouve = true;
                    compte.Id = Users[i].Id;
                    break;
                }
            }
            if (trouve)
            {
                this.Hide();
                new MainWindow(compte).Show();
            }
            else
            {
                this.lblMessage.Content = "username ou mot de passe incorrecte, Ressayer!!!";
            }

        }

        private void BtnSenregistrer_Click(object sender, RoutedEventArgs e)
        {
            this.Hide();
            new SEnregistrerWindow().Show();
        }

        private void CheckBox_Checked(object sender, RoutedEventArgs e)
        {
            this.PassWordBox.Password = this.PassWordBox.Password.ToString();
        }
    }
}
