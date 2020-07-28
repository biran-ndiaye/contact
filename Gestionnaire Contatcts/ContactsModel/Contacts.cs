using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ContactsModel
{
    //Executer d'abord le ficher Sql pour creer une DB
    public class Contacts
    {
        //Attribut et methodes pour un Contacts
        public int Id { get; set; }
        public string Nom { get; set; }
        public string Prenom { get; set; }
        public string numeroTelephone { get; set; }
        public DateTime? DateNaissance { get; set; }
        public string Company { get; set; }
        public string Courriel { get; set; }
        public string Profession { get; set; }
        public Addresse Addresse { get; set; }
        public string Fax { get; set; }
        public Contacts(int id = 0, string nom = "", string numeroTelephone = "", string company = "", string courriel = "", string profession = "", Addresse addresse = null,string prenom = "")
        {
            Id = id;
            Nom = nom;
            Prenom = prenom;
            this.numeroTelephone = numeroTelephone;
            DateNaissance = DateTime.Now;
            Company = company;
            Courriel = courriel;
            Profession = profession;
            Addresse = addresse;
        }

        public override string ToString()
        {
            return this.Prenom + "  " +this.Nom;
        }
    }
}
