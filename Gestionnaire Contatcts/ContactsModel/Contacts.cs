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
        public long numeroTelephone { get; set; }
        public DateTime? DateNaissance { get; set; }
        public string Company { get; set; }
        public DateTime DateDeNaissance { get; set; }
        public string Courriel { get; set; }
        public string Profession { get; set; }
        public Addresse Addresse { get; set; }

        public Contacts(int id = 0, string nom = "", long numeroTelephone = 0, DateTime? dateNaissance = null, string company = "", DateTime? dateDeNaissance = null, string courriel = "", string profession = "", Addresse addresse = null)
        {
            Id = id;
            Nom = nom;
            this.numeroTelephone = numeroTelephone;
            DateNaissance = dateNaissance;
            Company = company;
            DateDeNaissance =(DateTime)dateDeNaissance;
            Courriel = courriel;
            Profession = profession;
            Addresse = addresse;
        }
    }
}
