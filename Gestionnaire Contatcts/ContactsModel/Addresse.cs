using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ContactsModel
{
    public class Addresse
    {
        public int? Id { get; set; }
        public int? NumAppt { get; set; }
        public string Address { get; set; }
        public string Ville { get; set; }
        public string Pays { get; set; }
        public string CodePostal { get; set; }

        public Addresse(int? id=null,int?numAppt = null, string address = "", string ville = "", string pays ="",string codePostal="")
        {
            this.Id = id;
            this.NumAppt = numAppt;
            this.Address = address;
            this.Ville = ville;
            this.Pays = pays;
            this.CodePostal = codePostal;
        }
    }
}
