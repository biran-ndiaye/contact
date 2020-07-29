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

        //Ajout de contact
        public void AjouterContacts(Contacts contacts)
        {
            this.contactsServices.AjoutNouveauContact(contacts);
        }

        //Edition D'un Contact existant
        public void ModifierContact(Contacts contacts)
        {
            this.contactsServices.EditContact(contacts);
        }


        //Suppression de contact
        public void suprrimerContact(Contacts contact)
        {
            this.contactsServices.SupprimerContact(contact);
        }

        //Getter tous Les Contacts
        public List<Contacts> GetContacts()
        {
            return this.contactsServices.lesContacts();
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
