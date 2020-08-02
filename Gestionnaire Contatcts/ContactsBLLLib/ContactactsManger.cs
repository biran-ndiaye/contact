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
        //ajout de compte
        public void AjouterCompte(Compte compte)
        {
            this.contactsServices.AjouterCompte(compte);
        }

        //recuperer tous les comptes
        public List<Compte> GetUsers()
        {
            return this.contactsServices.LesComptes();
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
        public List<Contacts> GetContacts(Compte compte)
        {
            return this.contactsServices.lesContacts(compte);
        }

        //Rechercher D'un Contact selon certains criteres
        public List<Contacts> GetListeRechercher(string critereDeRechercheePays, string critereDeRechercheVille , string critereDeRechercheProfessions , string critereDeRechercheEntreprise ,string TextSaisi,string MethodeDeTri,Compte compte )
        {
            return this.contactsServices.RechercherContact(critereDeRechercheePays, critereDeRechercheVille, critereDeRechercheProfessions, critereDeRechercheEntreprise, TextSaisi, MethodeDeTri,compte);
        }

        //Tri Multicritere des Contacts
        public List<Contacts> GetListeTriMultiCritere(string methodeTri,Compte compte)
        {
            return this.contactsServices.ListeTrie(methodeTri,compte);
        }

        //Get different pays
        public  List<string> GetPays(Compte compte)
        {
            return this.contactsServices.lesPays(compte);
        }
        //Get Different ville
        public List<string> GetVilles(Compte compte)
        {
            return this.contactsServices.lesVilles(compte);
        }
        public List<string> GetProfessions(Compte compte)
        {
            return this.contactsServices.LesProfessions(compte);
        }
        public List<string> GetEntreprises(Compte compte)
        {
            return this.contactsServices.lesEntreprises(compte);
        }
    }
}
