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
        public List<Contacts> GetListeRechercher(string critereDeRechercheePays, string critereDeRechercheVille , string critereDeRechercheProfessions , string critereDeRechercheEntreprise ,string TextSaisi,string MethodeDeTri )
        {
            return this.contactsServices.RechercherContact(critereDeRechercheePays, critereDeRechercheVille, critereDeRechercheProfessions, critereDeRechercheEntreprise, TextSaisi, MethodeDeTri);
        }

        //Tri Multicritere des Contacts
        public List<Contacts> GetListeTriMultiCritere(string methodeTri)
        {
            return this.contactsServices.ListeTrie(methodeTri);
        }

        //Get different pays
        public  List<string> GetPays()
        {
            return this.contactsServices.lesPays();
        }
        //Get Different ville
        public List<string> GetVilles()
        {
            return this.contactsServices.lesVilles();
        }
        public List<string> GetProfessions()
        {
            return this.contactsServices.LesProfessions();
        }
        public List<string> GetEntreprises()
        {
            return this.contactsServices.lesEntreprises();
        }
    }
}
