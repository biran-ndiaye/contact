using ContactsDALLib;
using ContactsModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace ContactsBLLLib
{
    public class ContactactsManger
    {
        public ContactsServices contactsServices;

        public ContactactsManger()
        {
            this.contactsServices = new ContactsServices();
        }

        //tester l'etat de la connection
        /*public string GetStateConnection()
        {
            return this.contactsServices.EtatConnection();
        }*/

        //Ajout de contact
        public static void AjoutNouveauContact(Contacts contacts)
        {
            ContactsServices.AjoutNouveauContact(contacts);
        }

        //Edition D'un Contact existant
        public void EditerContact(Contacts contacts)
        {
            contactsServices.EditContact(contacts);
        }


        //Suppression de contact
        public void suprrimerContact(Contacts contacts)
        {
            contactsServices.SupprimerContact(contacts);
        }

        //Affichage de tous Les Contacts
        public void Afficher()
        {
            
        }

        //Rechercher D'un Contact selon certains criteres
        public void RechercherContact()
        {

        }

        //Tri Multicritere des Contacts
        public void TriMultiCritere()
        {

        }
    }
}
