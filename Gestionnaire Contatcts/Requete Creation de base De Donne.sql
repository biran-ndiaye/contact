use master

drop database if Exists GestionContacts
go 
create database GestionContacts
go
use GestionContacts

--Creation de la table utilisateur
--creation de la table Users
drop table if exists Users
create table Users(
	id int identity(1,1) not null primary key Clustered,
	UseName varchar(50) not null  ,
	Passworld varchar(30) not null
	
	);
--crestion de la table Address
go
drop table  if exists Addreess
create table Addreess(
	id int identity(1,1) not null primary key clustered,
	NoAppt int null,
	NomRue varchar(30)  null,
	CodePostal varchar(10)  null,
	Ville varchar(25)  null,
	Pays varchar(25)  null,
	);
--creation de la table des contacts
go
drop Table if exists Conatcts
go
create table Contacts(
	id int primary key clustered identity(1,1) not null,
	nom varchar(50) not null,
	prenom varchar(50) not null,
	numeroTelephone varchar(15) not null,
	Fax varchar(10) null,
	Company varchar(10) null,
	DateDeNaissance DateTime  not null,
	Courriel varchar(30) not null ,--check (Courriel like '%@%'),
	Profession varchar(20) null,
	id_Address int null foreign key references Addreess (id),
	id_Compte int not null foreign key references Users(id)  

	);

