use master

drop database if Exists GestionContacts
go 
create database GestionContacts
go
use GestionContacts

--Creation de la table utilisateur
drop table if exists Users
create table Users(
	UseName varchar(12) not null primary key Clustered,
	Passworld varchar(20) not null 
	
	);
--crestion de la table Address
go
drop table  if exists Addreess
create table Addreess(
	id int not null primary key clustered,
	NoAppt int null,
	NomRue varchar(30) not null,
	CodePostal varchar(10) not null,
	Ville varchar(25) not null,
	Pays varchar(25) not null,
	);
--creation de la table des contacts
go
drop Table if exists Conatcts
go
create table Contacts(
	id int primary key clustered identity(1,1) not null,
	nom varchar(50) not null,
	prenom varchar(50) not null,
	numeroTelephone bigInt not null,
	Fax string null,
	Company varchar(10) null,
	DateDeNaissance DateTime  not null,
	Courriel varchar(30) not null,
	Profession varchar(20) null,
	id_Address int null foreign key references Addreess (id)
	
	--UserName
	--UsersName varchar(12) not null foreign key references Users(UseName)

	);