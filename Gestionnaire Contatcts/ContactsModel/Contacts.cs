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
       
        public Contacts()
        {
            this.Addresse = new Addresse();
        }
        public override string ToString()
        {
            return this.Prenom + "        " +this.Nom;
        }

        public string AfficherContact()
        {
            string str = "Nom  : "+this.Nom+ "\n\n";
            str += "Prenom : " + this.Prenom+ "\n\n";
            str += "Telephone : " + this.numeroTelephone+"\n\n";
            DateTime date = (DateTime)this.DateNaissance;
            str += "Date De Naissance : " + date.Year+"-"+(date.Month<10?"0":"")+ date.Month + "-"+(date.Day<10?"0":"")+date.Day+ "\n\n";
            str += "Courriel : " + this.Courriel+ "\n\n";
            if(this.Profession == null)
            {
                str += "Profession :  ---  \n\n";
            }
            else
            {
                str += "Profession : " + this.Profession+ "\n\n";
            }
            if (this.Company!=null)
            {
                str += "Company : " + this.Company + "\n\n";
            }
            else
            {
                str += "Company  : ---\n\n";
            }
            if (this.Fax is null)
            {
                str += "Fax : ---\n\n";
            }
            else
            {
                str += "Fax : " + this.Fax + "\n\n";
            }
            //gerer le cas des Address  depuis la DB
            if(this.Addresse == null)
            {
                str += "Adresse  :  --- \n\n";
   
            }
            else
            {
                if(this.Addresse.NumAppt != null || this.Addresse.NumAppt != 0)
                {
                    if (this.Addresse.Address is null)
                    {
                        str += "Adresse : "  + this.Addresse.NumAppt  + " ---\n\n";
                    }
                    else
                    {
                        str += "Adresse : " +"Appt "+this.Addresse.NumAppt +" "+ this.Addresse.Address+ "\n\n";
                    }
                }
                else
                {
                    if(this.Addresse.Address is null)
                    {
                        str += "Adresse : ---\n\n";
                    }
                    else
                    { 
                        str += "Adresse : " + this.Addresse.Address + "\n\n";
                    }
                }
                if (this.Addresse.Pays !=  null)
                {
                    str += "Pays  : " + this.Addresse.Pays + "\n\n";
                }
                else
                {
                    str += "Pays : ---"+"\n\n";
                }
                if(this.Addresse.Ville != null)
                {
                    str += "Ville  : " + this.Addresse.Ville + "\n\n";
                }
                else
                {
                    str += "Ville  : --- \n\n";
                }

                if (this.Addresse.CodePostal != null)
                {
                    str += "Code Postal  : " + this.Addresse.CodePostal + "\n\n";
                }
                else
                {
                    str += "Code Postal  : --- \n\n";
                }
            }
            return str;
        }
    }
}
