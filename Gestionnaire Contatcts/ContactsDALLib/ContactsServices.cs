using ContactsModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;

namespace ContactsDALLib
{
    public class ContactsServices
    {
        public readonly string ChaineConnexion = @"Data Source=3G9MFW2\SQLEXPRESS;Initial Catalog=GestionContacts;Integrated Security=true; Connect Timeout=30";


        //Requtes database
        //Ajout De Nouveau Contact
        public void AjoutNouveauContact(Contacts contacts)
        {
            using (SqlConnection conn = new SqlConnection(ChaineConnexion))
            {
                conn.Open();
                //conn.State;
                using (SqlCommand cmd = conn.CreateCommand())
                {
                    //id_Address : Ecrire une requete qui permet de getter l'id

                    cmd.CommandText = "insert into Contacts (nom,prenom,numeroTelephone,Fax,Company)"
                }
            }
        }
        public string EtatConnection()
        {
            string etat;
            using (SqlConnection conn = new SqlConnection(ChaineConnexion))
            {
                conn.Open();
                 etat = conn.State.ToString();
                using (SqlCommand cmd = conn.CreateCommand())
                {
                    //cmd.CommandText = "insert into"
                }
            }
            return etat;
        }

        //supprimer
        //Edition
        //Rechercher
        //Trier

    }
}
