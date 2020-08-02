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

        //Recupere Les Contacts dans la DB du user
        public List<Contacts> lesContacts(Compte compte)
        {
            List<Contacts> lst = new List<Contacts>();
            using (SqlConnection conn = new SqlConnection(ChaineConnexion))
            {
                conn.Open();
                using (SqlCommand cmd = conn.CreateCommand())
                {
                    cmd.CommandText = "select Contacts.id, nom,prenom,numeroTelephone,Fax,Company,DateDeNaissance,Courriel,Profession,NoAppt,NomRue,CodePostal,Ville,Pays,Addreess.id ,id_compte " +
                                      "from Contacts " +
                                      "left join Addreess on Contacts.id_Address = Addreess.id " +
                                      "where id_Compte =@idCompte";
                    cmd.Parameters.AddWithValue("idCompte",compte.Id);
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            Contacts contact = new Contacts();
                            contact.compte.Id = compte.Id;
                            contact.Id = reader.GetInt32(0);
                            contact.Nom = reader.GetString(1);
                            contact.Prenom = reader.GetString(2);
                            contact.numeroTelephone = reader.GetString(3);
                            cmd.Parameters.AddWithValue("idCompte",compte.Id);

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

        // Ajout d'un nouveau Cmpte
        public void AjouterCompte(Compte compte)
        {
            using (SqlConnection conn = new SqlConnection(ChaineConnexion))
            {
                conn.Open();
                using (SqlCommand cmd= conn.CreateCommand())
                {
                    cmd.CommandText = "insert into Users(UseName,Passworld) values(@name,@passworld)";
                    cmd.Parameters.AddWithValue("name",compte.UserName);
                    cmd.Parameters.AddWithValue("Passworld", compte.Password);
                    cmd.ExecuteNonQuery();
                }
            }
        }
        
        //Retourne tous les comptes
        public List<Compte> LesComptes()
        {
            List<Compte> comptes = new List<Compte>();
            using (SqlConnection conn = new SqlConnection(ChaineConnexion))
            {
                conn.Open();
                using (SqlCommand cmd = conn.CreateCommand())
                {
                    cmd.CommandText = "select id,UseName,Passworld from Users";
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            Compte compte = new Compte();
                            compte.Id = reader.GetInt32(0);
                            compte.UserName = reader.GetString(1);
                            compte.Password = reader.GetString(2);
                            comptes.Add(compte);
                        }
                    }
                }
            }
            return comptes;
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
                    

                    cmd.CommandText = "insert into Contacts (nom,prenom,numeroTelephone,Fax,Company,DateDeNaissance,Courriel,Profession,id_Address,id_Compte) " +
                                      "values (@Name, @Prenom,@Num, @fax,@company, @dateNaissance, @courriel, @profession, @Id_Address,@id_compte)";
                    
                    //les champs obligatoires
                    cmd.Parameters.AddWithValue("Name", contacts.Nom);
                    cmd.Parameters.AddWithValue("Prenom", contacts.Prenom);
                    cmd.Parameters.AddWithValue("Num", contacts.numeroTelephone);
                    cmd.Parameters.AddWithValue("dateNaissance",contacts.DateNaissance);
                    cmd.Parameters.AddWithValue("courriel", contacts.Courriel);
                    cmd.Parameters.AddWithValue("id_compte", contacts.compte.Id);
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
                    cmd.CommandText = "delete from Contacts where id = @id";
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
        
        //retourne les differents pays 
        public List<string> lesPays(Compte compte)
        {
            return this.GetListe("select distinct pays from Addreess inner join Contacts on Contacts. id_Address=Addreess.id  where id_Compte =@idCompte", compte);
        }
       
        //retourne les villes
        public List<string> lesVilles(Compte compte)
        {
            return this.GetListe("select distinct Ville from Addreess inner join Contacts on Contacts. id_Address=Addreess.id  where id_Compte =@idCompte", compte);
        }

        //retourne les professions
        public List<string> LesProfessions(Compte compte)
        {
            return this.GetListe("select distinct Profession from Contacts  where id_Compte =@idCompte", compte);
        }

        //retourne les Entreprises
        public List<string> lesEntreprises(Compte compte)
        {
            return this.GetListe("select distinct Company from Contacts where id_Compte =@idCompte",compte);
        }
        //fonction pour simplifier les get des differents element
        public List<string> GetListe(string requete,Compte compte)
        {
            List<string> lst = new List<string>();
            using (SqlConnection conn = new SqlConnection(ChaineConnexion))
            {
                conn.Open();
                using (SqlCommand cmd = conn.CreateCommand())
                {
                    cmd.CommandText = requete;
                    cmd.Parameters.AddWithValue("idCompte", compte.Id);
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            if (reader.GetValue(0) != DBNull.Value)
                            {
                                string pays = reader.GetString(0);
                                lst.Add(pays);
                            }
                        }
                    }
                }
            }
            return lst;
        }
        //Rechercher
        public List<Contacts> RechercherContact(string critereDeRechercheePays, string critereDeRechercheVille, string critereDeRechercheProfessions, string critereDeRechercheEntreprise, string TextSaisi,string methodeDeTri,Compte compte)
        {
            List<Contacts> lst = new List<Contacts>();
            using (SqlConnection conn = new SqlConnection(ChaineConnexion))
            {
                conn.Open();
                //cas ou tous les 4 c'est Tout
                string requete1 = "select Contacts.id, nom,prenom,numeroTelephone,Fax,Company,DateDeNaissance,Courriel,Profession,NoAppt,NomRue,CodePostal,Ville,Pays,Addreess.id,id_Compte " +
                                     "from Contacts " +
                                     "left join Addreess on Contacts.id_Address = Addreess.id" +
                                     " where (nom like @chaineSaisi+'%' or prenom like @chaineSaisi+'%') and id_Compte =@idCompte";
                string requete2 = requete1;
                string requete3 = requete2;
                string requete4 = requete3;

                if (critereDeRechercheePays.CompareTo("Tout") != 0)
                {
                    requete1 = "select Contacts.id, nom,prenom,numeroTelephone,Fax,Company,DateDeNaissance,Courriel,Profession,NoAppt,NomRue,CodePostal,Ville,Pays,Addreess.id,id_Compte " +
                                     "from Contacts " +
                                     "left join Addreess on Contacts.id_Address = Addreess.id " +
                                     "where id_Compte =@idCompte and Pays like '%'+@paysSelect+'%'  and (nom like @chaineSaisi+'%' or prenom like @chaineSaisi+'%') ";
                }


                if (critereDeRechercheVille.CompareTo("Tout") != 0)
                {
                    requete2 = "select Contacts.id, nom,prenom,numeroTelephone,Fax,Company,DateDeNaissance,Courriel,Profession,NoAppt,NomRue,CodePostal,Ville,Pays,Addreess.id,id_Compte " +
                                     "from Contacts " +
                                     "left join Addreess on Contacts.id_Address = Addreess.id " +
                                     "where id_Compte =@idCompte and Ville like '%'+@villeSelect+'%' and (nom Like @chaineSaisi+'%' or prenom like @chaineSaisi+'%')";
                }

                if (critereDeRechercheProfessions.CompareTo("Tout") != 0)
                {
                    requete3 = "select Contacts.id, nom,prenom,numeroTelephone,Fax,Company,DateDeNaissance,Courriel,Profession,NoAppt,NomRue,CodePostal,Ville,Pays,Addreess.id,id_Compte " +
                                     "from Contacts " +
                                     "left join Addreess on Contacts.id_Address = Addreess.id " +
                                     "where id_Compte =@idCompte and Profession like '%'+@professionSelect+'%' and (nom Like @chaineSaisi+'%' or prenom like @chaineSaisi+'%')";
                }
                
                if (critereDeRechercheEntreprise.CompareTo("Tout") != 0)
                {
                    requete4= "select Contacts.id, nom,prenom,numeroTelephone,Fax,Company,DateDeNaissance,Courriel,Profession,NoAppt,NomRue,CodePostal,Ville,Pays,Addreess.id,id_Compte " +
                                     "from Contacts " +
                                     "left join Addreess on Contacts.id_Address = Addreess.id " +
                                     "where id_Compte =@idCompte and Company like '%'+@companySelect+'%' and (nom Like @chaineSaisi+'%' or prenom like @chaineSaisi+'%')";
                }
               
                using (SqlCommand cmd = conn.CreateCommand())
                {
                    //n'oubliez pas les cmd parametere
                    if (methodeDeTri.CompareTo("Prenom Croissant") == 0)
                    {
                        cmd.CommandText =requete1+" intersect " + requete2 + " intersect " + requete3+ " intersect " + requete4+" order by prenom asc";
                    }
                    else if (methodeDeTri.CompareTo("Prenom Decroissant") == 0)
                    {
                        cmd.CommandText = requete1 + " intersect " + requete2 + " intersect " + requete3 + " intersect " + requete4 + " order by prenom desc";
                    }
                    else if (methodeDeTri.CompareTo("Nom Croissant") == 0)
                    {
                        cmd.CommandText = requete1 + " intersect " + requete2 + " intersect " + requete3 + " intersect " + requete4 + " order by prenom asc ";
                    }
                    else if (methodeDeTri.CompareTo("Nom Decroissant") == 0)
                    {
                        cmd.CommandText = requete1 + " intersect " + requete2 + " intersect "  + requete3 + " intersect " + requete4 + " order by prenom desc";
                    }
                    else
                    {
                        cmd.CommandText = requete1 + " intersect " + requete2 + " intersect " + requete3 + " intersect " + requete4 ;
                    }

                    cmd.Parameters.AddWithValue("paysSelect", critereDeRechercheePays);
                    cmd.Parameters.AddWithValue("villeSelect", critereDeRechercheVille);
                    cmd.Parameters.AddWithValue("professionSelect", critereDeRechercheProfessions);
                    cmd.Parameters.AddWithValue("companySelect", critereDeRechercheEntreprise);
                    cmd.Parameters.AddWithValue("chaineSaisi", TextSaisi);
                    cmd.Parameters.AddWithValue("idCompte",compte.Id);
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            Contacts contact = new Contacts();
                            contact.Id = reader.GetInt32(0);
                            contact.Nom = reader.GetString(1);
                            contact.Prenom = reader.GetString(2);
                            contact.numeroTelephone = reader.GetString(3);
                            contact.compte.Id = reader.GetInt32(15);
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

        //Trier
        public List<Contacts> ListeTrie(string methodeTri,Compte compte)
        {
            List<Contacts> lst = new List<Contacts>();
            using (SqlConnection conn = new SqlConnection(ChaineConnexion))
            {
                conn.Open();
                using (SqlCommand cmd = conn.CreateCommand())
                { 
                    if (methodeTri.CompareTo("Prenom Croissant") == 0)
                    {
                        cmd.CommandText = "select Contacts.id, nom,prenom,numeroTelephone,Fax,Company,DateDeNaissance,Courriel,Profession,NoAppt,NomRue,CodePostal,Ville,Pays,Addreess.id,id_Compte " +
                                     "from Contacts " +
                                     "left join Addreess on Contacts.id_Address = Addreess.id " +
                                     "where id_Compte =@idCompte "+
                                     "order by prenom asc";
                    }
                    else if (methodeTri.CompareTo("Prenom Decroissant") == 0)
                    {
                        cmd.CommandText = "select Contacts.id, nom,prenom,numeroTelephone,Fax,Company,DateDeNaissance,Courriel,Profession,NoAppt,NomRue,CodePostal,Ville,Pays,Addreess.id,id_Compte " +
                                     "from Contacts " +
                                     "left join Addreess on Contacts.id_Address = Addreess.id " +
                                     "where id_Compte =@idCompte " +
                                     "order by prenom desc";
                    }
                    else if (methodeTri.CompareTo("Nom Croissant") == 0)
                    {
                        cmd.CommandText = "select Contacts.id, nom,prenom,numeroTelephone,Fax,Company,DateDeNaissance,Courriel,Profession,NoAppt,NomRue,CodePostal,Ville,Pays,Addreess.id,id_Compte " +
                                     "from Contacts " +
                                     "left join Addreess on Contacts.id_Address = Addreess.id " +
                                     "where id_Compte =@idCompte " +
                                     "order by nom asc";

                    }
                    else if(methodeTri.CompareTo("Nom Decroissant") == 0)
                    {
                        cmd.CommandText = "select Contacts.id, nom,prenom,numeroTelephone,Fax,Company,DateDeNaissance,Courriel,Profession,NoAppt,NomRue,CodePostal,Ville,Pays,Addreess.id,id_Compte " +
                                     "from Contacts " +
                                     "left join Addreess on Contacts.id_Address = Addreess.id " +
                                     "where id_Compte =@idCompte " +
                                     "order by nom desc";
                    }
                    else
                    {
                        //cas de date d'ajout
                        cmd.CommandText = "select Contacts.id, nom,prenom,numeroTelephone,Fax,Company,DateDeNaissance,Courriel,Profession,NoAppt,NomRue,CodePostal,Ville,Pays,Addreess.id,id_Compte " +
                                            "from Contacts " +
                                            "left join Addreess on Contacts.id_Address = Addreess.id "+
                                            "where id_Compte =@idCompte" ;
                    }
                    cmd.Parameters.AddWithValue("idCompte",compte.Id);
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            Contacts contact = new Contacts();
                            contact.Id = reader.GetInt32(0);
                            contact.Nom = reader.GetString(1);
                            contact.Prenom = reader.GetString(2);
                            contact.numeroTelephone = reader.GetString(3);
                            contact.compte.Id = reader.GetInt32(15);

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
