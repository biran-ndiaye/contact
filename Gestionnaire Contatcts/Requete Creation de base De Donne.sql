use master
go
drop database if Exists GestionContacts
go 
create database GestionContacts
go
drop Table if exists Conatcts
create table Contacts(
	id int primary key clustered identity(1,1) not null,
	numeroTelephone bigInt not null,
	Company varchar(10) null,
	Fax bigInt  null,
	nom varchar(50) not null,
	prenom varchar(50) not null,
	Courriel varchar(30) null,
	Profession varchar(20) null,
	Addresse varchar(50)  null,
	CodePostal varchar(10) null,
	Ville varchar(25) not null,
	 Pays varchar(25) not null
	);

