using ContactsModel;
using Microsoft.SqlServer.Server;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Runtime.InteropServices;

namespace ContactsDALLib
{
    public class ContactsServices
    {
        static readonly string ChaineConnexion = @"Data Source=3G9MFW2\SQLEXPRESS;Initial Catalog=GestionContacts;Integrated Security=true; Connect Timeout=30";

        //Recupere Les Contacts dans la DB
        public List<Contacts> lesContacts()
        {
            List<Contacts> lst = new List<Contacts>();
            using (SqlConnection conn = new SqlConnection(ChaineConnexion))
            {
                conn.Open();
                using (SqlCommand cmd = conn.CreateCommand())
                {
                    cmd.CommandText = "select Contacts.id, nom,prenom,numeroTelephone,Fax,Company,DateDeNaissance,Courriel,Profession,NoAppt,NomRue,CodePostal,Ville,Pays,Addreess.id " +
                                      "from Contacts " +
                                      "left join Addreess on Contacts.id_Address = Addreess.id ";
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            Contacts contact = new Contacts();
                            contact.Id = reader.GetInt32(0);
                            contact.Nom = reader.GetString(1);
                            contact.Prenom = reader.GetString(2);
                            contact.numeroTelephone = reader.GetString(3);
                            
                            if (reader.GetValue(4) != DBNull.Value)
                            {
                                contact.Fax = reader.GetString(4);
                            }
                            else
                            {
                                contact.Fax = null;
                            }

                            if (reader.GetValue(5) != DBNull.Value)
                            {
                                contact.Company = reader.GetString(5);
                            }
                            else
                            {
                                contact.Company = null;
                            }
                            contact.DateNaissance = reader.GetDateTime(6);
                            contact.Courriel = reader.GetString(7);

                            if (reader.GetValue(8) != DBNull.Value)
                            {
                                contact.Profession = reader.GetString(8);
                            }
                            else
                            {
                                contact.Profession = null;
                            }

                            if(reader.GetValue(9) != DBNull.Value)
                            {
                                contact.Addresse.NumAppt = reader.GetInt32(9);
                            }
                            else
                            {
                                contact.Addresse.NumAppt = null;
                            }

                            if (reader.GetValue(10) != DBNull.Value)
                            {
                                contact.Addresse.Address = reader.GetString(10);
                            }
                            else
                            {
                                contact.Addresse.Address = null;
                            }

                            if (reader.GetValue(11) != DBNull.Value)
                            {
                                contact.Addresse.CodePostal = reader.GetString(11);
                            }
                            else
                            {
                                contact.Addresse.CodePostal = null;
                            }
                           
                            if (reader.GetValue(12) != DBNull.Value)
                            {
                                contact.Addresse.Ville = reader.GetString(12);
                            }
                            else
                            {
                                contact.Addresse.Ville = null;
                            }
                           
                            if (reader.GetValue(13) != DBNull.Value)
                            {
                                contact.Addresse.Pays = reader.GetString(13);
                            }
                            else
                            {
                                contact.Addresse.Pays = null;
                            }
                            

                            if(reader.GetValue(14) != DBNull.Value)
                            {
                                contact.Addresse.Id = reader.GetInt32(14);
                            }
                            else
                            {
                                contact.Addresse.Id = null;
                            }
                            lst.Add(contact);
                        }
                    }
                }
            }
            return lst;
        }

        //Requtes database
        //Ajout De Nouveau Contact
        public  void AjoutNouveauContact(Contacts contacts)
        {
            
            using (SqlConnection conn = new SqlConnection(ChaineConnexion))
            {
                conn.Open();
                //conn.State;
                using (SqlCommand cmd = conn.CreateCommand())
                {
                    

                    cmd.CommandText = "insert into Contacts (nom,prenom,numeroTelephone,Fax,Company,DateDeNaissance,Courriel,Profession,id_Address) " +
                                      "values (@Name, @Prenom,@Num, @fax,@company, @dateNaissance, @courriel, @profession, @Id_Address)";
                    
                    //les champs obligatoires
                    cmd.Parameters.AddWithValue("Name", contacts.Nom);
                    cmd.Parameters.AddWithValue("Prenom", contacts.Prenom);
                    cmd.Parameters.AddWithValue("Num", contacts.numeroTelephone);
                    cmd.Parameters.AddWithValue("dateNaissance",contacts.DateNaissance);
                    cmd.Parameters.AddWithValue("courriel", contacts.Courriel);

                    //les champs nullables
                    if (contacts.Fax == null)
                    {
                        cmd.Parameters.AddWithValue("fax", DBNull.Value);
                    }
                    else
                    {
                        cmd.Parameters.AddWithValue("fax",contacts.Fax);
                    }
                    
                    if (contacts.Company is null)
                    {
                        cmd.Parameters.AddWithValue("company", DBNull.Value);
                    }
                    else
                    {
                        cmd.Parameters.AddWithValue("company", contacts.Company);
                    }
                    
                    if (contacts.Profession == null)
                    {
                        cmd.Parameters.AddWithValue("profession", DBNull.Value);
                    }
                    else
                    {
                        cmd.Parameters.AddWithValue("profession",contacts.Profession);
                    }

                    //id_Address : Ecrire une requete qui permet de getter l'id
                    if (contacts.Addresse is null)
                    {
                        cmd.Parameters.AddWithValue("Id_Address",DBNull.Value);
                    }
                    else
                    {
                        object id_add = null;
                        using (SqlCommand cmd1 = conn.CreateCommand())
                        {
                            //on peut mieux approfondir en testant si cette contact habite avec un autre contact
                            cmd1.CommandText = "insert into Addreess(NoAppt,NomRue,CodePostal,Ville,Pays) output inserted.id values(@noAppt,@nomRue,@codePostal,@ville,@pays) ";
                            if (contacts.Addresse.NumAppt is null || contacts.Addresse.NumAppt == 0)
                            {
                                cmd1.Parameters.AddWithValue("noAppt", DBNull.Value);
                            }
                            else
                            {
                                cmd1.Parameters.AddWithValue("noAppt",contacts.Addresse.NumAppt);
                            }
                            if (contacts.Addresse.Address == null)
                            {
                                cmd1.Parameters.AddWithValue("nomRue", DBNull.Value);
                            }
                            else
                            {
                                cmd1.Parameters.AddWithValue("nomRue", contacts.Addresse.Address);
                            }
                            if (contacts.Addresse.Ville is null)
                            {
                                cmd1.Parameters.AddWithValue("ville", DBNull.Value);
                            }
                            else
                            {
                                cmd1.Parameters.AddWithValue("ville", contacts.Addresse.Ville);
                            }
                            if (contacts.Addresse.CodePostal == null)
                            {
                                cmd1.Parameters.AddWithValue("codePostal", DBNull.Value);
                            }
                            else
                            {
                                cmd1.Parameters.AddWithValue("codePostal", contacts.Addresse.CodePostal);
                            }
                            cmd1.Parameters.AddWithValue("pays", contacts.Addresse.Pays);

                            id_add = cmd1.ExecuteScalar();

                        }
                        cmd.Parameters.AddWithValue("Id_Address", (int)id_add);
                    }

                    cmd.ExecuteNonQuery();
                }

            }
        }

        
        //supprimer un contact
        public void SupprimerContact(Contacts contacts)
        {
            using (SqlConnection conn = new SqlConnection(ChaineConnexion))
            {
                conn.Open();
                using (SqlCommand cmd = conn.CreateCommand())
                {
                    cmd.CommandText = "delete from Contacts where Contacts.id = @id";
                    cmd.Parameters.Add(new SqlParameter("id",contacts.Id));
                    cmd.ExecuteNonQuery();
                }
            }
        }

        //Edition Pas encore termine
        public void EditContact(Contacts contacts)
        {
            using (SqlConnection conn = new SqlConnection(ChaineConnexion))
            {
                conn.Open();
                //conn.State;
                using (SqlCommand cmd = conn.CreateCommand())
                {
                    cmd.CommandText = "update  Contacts set nom = @Name ,prenom = @Prenom ,numeroTelephone=@Num,Fax =@fax,Company=@company," +
                                      "DateDeNaissance=@dateNaissance,Courriel= @courriel,Profession=@profession where Contacts.id = @Id";

                    //les champs obligatoires
                    cmd.Parameters.AddWithValue("Name", contacts.Nom);
                    cmd.Parameters.AddWithValue("Prenom", contacts.Prenom);
                    cmd.Parameters.AddWithValue("Num", contacts.numeroTelephone);
                    cmd.Parameters.AddWithValue("dateNaissance", contacts.DateNaissance);
                    cmd.Parameters.AddWithValue("courriel", contacts.Courriel);
                    cmd.Parameters.AddWithValue("Id", contacts.Id);
                    //les champs nullables
                    if (contacts.Fax == null)
                    {
                        cmd.Parameters.AddWithValue("fax", DBNull.Value);
                    }
                    else
                    {
                        cmd.Parameters.AddWithValue("fax", contacts.Fax);
                    }

                    if (contacts.Company is null)
                    {
                        cmd.Parameters.AddWithValue("company", DBNull.Value);
                    }
                    else
                    {
                        cmd.Parameters.AddWithValue("company", contacts.Company);
                    }

                    if (contacts.Profession == null)
                    {
                        cmd.Parameters.AddWithValue("profession", DBNull.Value);
                        
                    }
                    else
                    {
                        cmd.Parameters.AddWithValue("profession", contacts.Profession);

                    }

                    //id_Address : Ecrire une requete qui permet de getter l'id
                    if (contacts.Addresse is null)
                    {
                        cmd.Parameters.AddWithValue("Id_Address", DBNull.Value);
                    }
                    else
                    {
                        using (SqlCommand cmd1 = conn.CreateCommand())
                        {
                            
                            cmd1.CommandText = "update  Addreess set NoAppt = @noAppt, NomRue = @nomRue, CodePostal = @codePostal, Ville = @ville ,Pays = @pays where id= @Id_Address ";
                            cmd1.Parameters.AddWithValue("Id_Address",contacts.Addresse.Id);

                            if (contacts.Addresse.NumAppt is null)
                            {
                                cmd1.Parameters.AddWithValue("noAppt", DBNull.Value);
                            }
                            else
                            {
                                cmd1.Parameters.AddWithValue("noAppt", contacts.Addresse.NumAppt);
                            }
                            if (contacts.Addresse.Address == null)
                            {
                                cmd1.Parameters.AddWithValue("nomRue", DBNull.Value);
                            }
                            else
                            {
                                cmd1.Parameters.AddWithValue("nomRue", contacts.Addresse.Address);
                            }
                            if (contacts.Addresse.Ville is null)
                            {
                                cmd1.Parameters.AddWithValue("ville", DBNull.Value);
                            }
                            else
                            {
                                cmd1.Parameters.AddWithValue("ville", contacts.Addresse.Ville);
                            }
                            if (contacts.Addresse.CodePostal == null)
                            {
                                cmd1.Parameters.AddWithValue("codePostal", DBNull.Value);
                            }
                            else
                            {
                                cmd1.Parameters.AddWithValue("codePostal", contacts.Addresse.CodePostal);
                            }
                            if(contacts.Addresse.Pays == null)
                            {
                                cmd1.Parameters.AddWithValue("pays", null);
                            }
                            else
                            {
                                cmd1.Parameters.AddWithValue("pays", contacts.Addresse.Pays);
                            }
                            cmd1.ExecuteNonQuery();
                        }
                        
                    }
                    cmd.ExecuteNonQuery();
                }

            }

        }
        
       
        //Rechercher
        public void RechercherContact(string critereDeRecherchee)
        {

        }

        //Trier
        public List<Contacts> ListeTrie(string methodeTri)
        {
            List<Contacts> lst = new List<Contacts>();
            using (SqlConnection conn = new SqlConnection(ChaineConnexion))
            {
                conn.Open();
                using (SqlCommand cmd = conn.CreateCommand())
                { 
                    if (methodeTri.CompareTo("Prenom Croissant") == 0)
                    {
                        cmd.CommandText = "select Contacts.id, nom,prenom,numeroTelephone,Fax,Company,DateDeNaissance,Courriel,Profession,NoAppt,NomRue,CodePostal,Ville,Pays,Addreess.id " +
                                     "from Contacts " +
                                     "left join Addreess on Contacts.id_Address = Addreess.id " +
                                     "order by prenom asc";
                    }
                    else if (methodeTri.CompareTo("Prenom Decroissant") == 0)
                    {
                        cmd.CommandText = "select Contacts.id, nom,prenom,numeroTelephone,Fax,Company,DateDeNaissance,Courriel,Profession,NoAppt,NomRue,CodePostal,Ville,Pays,Addreess.id " +
                                     "from Contacts " +
                                     "left join Addreess on Contacts.id_Address = Addreess.id " +
                                     "order by prenom desc";
                    }
                    else if (methodeTri.CompareTo("Nom Croissant") == 0)
                    {
                        cmd.CommandText = "select Contacts.id, nom,prenom,numeroTelephone,Fax,Company,DateDeNaissance,Courriel,Profession,NoAppt,NomRue,CodePostal,Ville,Pays,Addreess.id " +
                                     "from Contacts " +
                                     "left join Addreess on Contacts.id_Address = Addreess.id " +
                                     "order by nom asc";

                    }
                    else if(methodeTri.CompareTo("Nom Decroissant") == 0)
                    {
                        cmd.CommandText = "select Contacts.id, nom,prenom,numeroTelephone,Fax,Company,DateDeNaissance,Courriel,Profession,NoAppt,NomRue,CodePostal,Ville,Pays,Addreess.id " +
                                     "from Contacts " +
                                     "left join Addreess on Contacts.id_Address = Addreess.id " +
                                     "order by nom desc";
                    }
                    else
                    {
                        //cas de date d'ajout
                        cmd.CommandText = "select Contacts.id, nom,prenom,numeroTelephone,Fax,Company,DateDeNaissance,Courriel,Profession,NoAppt,NomRue,CodePostal,Ville,Pays,Addreess.id " +
                                            "from Contacts " +
                                            "left join Addreess on Contacts.id_Address = Addreess.id ";
                    }
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            Contacts contact = new Contacts();
                            contact.Id = reader.GetInt32(0);
                            contact.Nom = reader.GetString(1);
                            contact.Prenom = reader.GetString(2);
                            contact.numeroTelephone = reader.GetString(3);

                            if (reader.GetValue(4) != DBNull.Value)
                            {
                                contact.Fax = reader.GetString(4);
                            }
                            else
                            {
                                contact.Fax = null;
                            }

                            if (reader.GetValue(5) != DBNull.Value)
                            {
                                contact.Company = reader.GetString(5);
                            }
                            else
                            {
                                contact.Company = null;
                            }
                            contact.DateNaissance = reader.GetDateTime(6);
                            contact.Courriel = reader.GetString(7);

                            if (reader.GetValue(8) != DBNull.Value)
                            {
                                contact.Profession = reader.GetString(8);
                            }
                            else
                            {
                                contact.Profession = null;
                            }

                            if (reader.GetValue(9) != DBNull.Value)
                            {
                                contact.Addresse.NumAppt = reader.GetInt32(9);
                            }
                            else
                            {
                                contact.Addresse.NumAppt = null;
                            }

                            if (reader.GetValue(10) != DBNull.Value)
                            {
                                contact.Addresse.Address = reader.GetString(10);
                            }
                            else
                            {
                                contact.Addresse.Address = null;
                            }

                            if (reader.GetValue(11) != DBNull.Value)
                            {
                                contact.Addresse.CodePostal = reader.GetString(11);
                            }
                            else
                            {
                                contact.Addresse.CodePostal = null;
                            }

                            if (reader.GetValue(12) != DBNull.Value)
                            {
                                contact.Addresse.Ville = reader.GetString(12);
                            }
                            else
                            {
                                contact.Addresse.Ville = null;
                            }

                            if (reader.GetValue(13) != DBNull.Value)
                            {
                                contact.Addresse.Pays = reader.GetString(13);
                            }
                            else
                            {
                                contact.Addresse.Pays = null;
                            }


                            if (reader.GetValue(14) != DBNull.Value)
                            {
                                contact.Addresse.Id = reader.GetInt32(14);
                            }

                            lst.Add(contact);
                        }
                    }
                }

            }
           
            return lst;
        }

    }

   
}
