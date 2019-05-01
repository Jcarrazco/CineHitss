create database CineHitss;
use CineHitss;
--Tabla usuarios--
CREATE TABLE Users (
  [id] int IDENTITY(1,1) PRIMARY KEY,
  [Username] varchar(255) not null,
  [Password] varchar(255) NOT NULL,
  [Email] varchar(255) not null,
  [Points] int null
);

--tabla de peliculas--
CREATE TABLE Movies (
  [id] int IDENTITY(1,1) PRIMARY KEY,
  [Nombre] varchar(255) not null,
  [Duracion] time NOT NULL,
  [Genero] varchar (20) not null,
  [Sinopsis] varchar(255) not null,
  [Clasification] Varchar(20) null
);

--tabla de cines--
CREATE TABLE Cines (
	[id] int Identity(1,1) primary key,
	[Location] varchar (50) not null
);


--tabla de cartelera--
CREATE TABLE Cartelera(
	[id] int Identity(1,1) primary key,
	[Horario] Datetime not null,
	FOREIGN KEY (id) REFERENCES [dbo].[Movies](id),
	FOREIGN KEY (id) REFERENCES [dbo].[Cines](id)
);

--tabla de cartelera--
CREATE TABLE Historial(
	[id] int Identity(1,1) primary key,
	FOREIGN KEY (id) REFERENCES [dbo].[Users](id),
	FOREIGN KEY (id) REFERENCES [dbo].[Cartelera](id)
);