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
    /// Logique d'interaction pour SEnregistrerWindow.xaml
    /// </summary>
    public partial class SEnregistrerWindow : Window
    {
        public ContactactsManger contactactsManger;
        public SEnregistrerWindow()
        {
            InitializeComponent();
            this.Top = 80;
            this.Left = 350;
            this.contactactsManger = new ContactactsManger();
        }

        private void BtnAnnuler_Click(object sender, RoutedEventArgs e)
        {
            this.lblMessage.Content = "";
            this.PasswordBox.Password = "";
            this.ConfirmerPassword.Password = "";
            this.Close();
            new LoginWindow().Show();
        }

        private void BtnConfirmer_Click(object sender, RoutedEventArgs e)
        {
            if (this.PasswordBox.Password.CompareTo(this.ConfirmerPassword.Password) == 0)
            {
                if (this.TxtBoxUserName.Text.Trim().Length<50 && this.PasswordBox.Password.Length <30)
                {
                    //verifier si le nom utilisateur est deja utilise
                    bool trouve = false;
                    foreach (Compte c in this.contactactsManger.GetUsers())
                    {
                        if (c.UserName.CompareTo(this.TxtBoxUserName.Text.Trim()) == 0)
                        {
                            trouve = true;
                            break;
                        }
                    }
                    if (!trouve)
                    {
                        if (string.IsNullOrWhiteSpace(this.TxtBoxUserName.Text.Trim()) || string.IsNullOrWhiteSpace(this.PasswordBox.Password))
                        {
                            this.lblMessage.Content = "L'une des deux champs est vide, Remplissez tous les chmaps!!! ";
                        }
                        else
                        {

                            Compte compte = new Compte {UserName=this.TxtBoxUserName.Text.Trim(),Password =this.PasswordBox.Password };
                            this.contactactsManger.AjouterCompte(compte);
                            this.Close();
                            new LoginWindow().Show();
                        }
                    }
                    else
                    {
                        this.lblMessage.Content = "Username deja utilise, essayer un autre!!!";
                    }
                }
                else if(this.TxtBoxUserName.Text.Length > 50)
                {
                    this.lblMessage.Content = "Le username ne doit pas depasser 50 caractere, Ressayer!!!";
                }
                else if (this.PasswordBox.Password.Length > 30)
                {
                    this.lblMessage.Content = "Le mot de pass ne doit pas depasser 30 caracteres, Ressayer!!!";
                }

            }
            else
            {
                this.lblMessage.Content = "les deux mots de pass ne correspondent pas, Ressayer!!!";
                this.PasswordBox.Password = "";
                this.ConfirmerPassword.Password="";
            }
        }

    }
}
