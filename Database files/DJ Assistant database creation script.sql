
-- Switch to the system (aka master) database
USE master;
GO

-- Delete the ProjectOrganizer database (IF EXISTS)
DROP DATABASE IF EXISTS DJAssistant;
GO

-- Create a new ProjectOrganizer database
CREATE DATABASE DJAssistant;
GO

-- Switch to the ProjectOrganizer database
USE DJAssistant;
GO

BEGIN TRANSACTION

--basic syntax
CREATE TABLE DJ
(
  id integer identity NOT NULL,
  Email varchar(45) NOT NULL,
  Displayname varchar(45) NOT NULL,
  Hash varchar(50) Not Null,
  Salt varchar(50) NOT NULL,
  CONSTRAINT pk_DJ_id PRIMARY KEY (id),
);
Go

CREATE TABLE Song (
    id integer identity NOT NULL,
    Title varchar(45) NOT NULL,
	Artist varchar(45) NOT NULL,
	Length int NULL, 
	Explicit bit NOT NULL
    CONSTRAINT pk_Song_id PRIMARY KEY (id),
	);
Go

CREATE TABLE Genre (
    id integer identity NOT NULL,
    Name varchar(45) NOT NULL,
    CONSTRAINT pk_Genre_id PRIMARY KEY (id),
	);
Go

	CREATE TABLE Party (
    id integer identity NOT NULL,
    DJ_id integer NOT NULL,
	Name varchar(45) NOT NULL,
    Description varchar(500) NOT NULL,
    CONSTRAINT pk_Party_id PRIMARY KEY (id),
);
GO

Create Table Song_Genre (
	Song_id integer NOT NULL,
	Genre_id integer  NOT NULL,
	CONSTRAINT pk_Song_Genre_Song_id PRIMARY KEY (Song_id, Genre_id),
);
Go

Create Table Song_DJ (
	Song_id integer NOT NULL,
	DJ_id integer NOT NULL,
	CONSTRAINT pk_Library_DJ_id PRIMARY KEY (DJ_id, Song_id),
);
Go

Create Table Party_Song (
    id integer identity NOT NULL,
	Song_id integer NOT NULL,
	Party_id integer  NOT NULL,
	Play_Order integer NOT NULL,
	Played bit NOT NULL,
	CONSTRAINT pk_Party_Song_Song_id PRIMARY KEY (id),
);
Go

Create Table Party_Genre (
	Party_id integer NOT NULL,
	Genre_id integer NOT NULL,
	CONSTRAINT pk_Party_Genre PRIMARY KEY (Party_id, Genre_id),
);
Go

Create Table Playlist (
	Id integer identity NOT NULL,
	Dj_id integer NOT NULL,
	Title varChar(50) NOT NULL,
	CONSTRAINT pk_PlayList_id PRIMARY KEY (Id),
);

Create Table Song_Playlist (
	Song_id integer NOT NULL,
	Playlist_id integer NOT NULL,
	CONSTRAINT pk_Song_Playlist Primary KEY (Song_id, PlayList_id)	
);

ALTER TABLE Party
ADD FOREIGN KEY(DJ_id)
REFERENCES DJ(id);

ALTER TABLE Party_Song
ADD FOREIGN KEY(Party_id)
REFERENCES Party(id);

ALTER TABLE Party_Song
ADD FOREIGN KEY(Song_id)
REFERENCES Song(id);

ALTER TABLE Song_DJ
ADD FOREIGN KEY(Song_id)
REFERENCES Song(id);

ALTER TABLE Song_DJ
ADD FOREIGN KEY(DJ_id)
REFERENCES DJ(id);

ALTER TABLE Song_Genre
ADD FOREIGN KEY(Song_id)
REFERENCES Song(id);

ALTER TABLE Song_Genre
ADD FOREIGN KEY(Genre_id)
REFERENCES Genre(id);
Go

ALTER TABLE Party_Genre
ADD FOREIGN KEY(Genre_id)
REFERENCES Genre(id);
Go
ALTER TABLE Party_Genre
ADD FOREIGN KEY(Party_id)
REFERENCES Party(id);
Go

ALTER TABLE Playlist
ADD FOREIGN KEY(Dj_id)
REFERENCES DJ(id);
go

ALTER TABLE Song_Playlist
ADD FOREIGN KEY(Song_id)
REFERENCES Song(id);
ALTER TABLE Song_Playlist
ADD FOREIGN KEY(Playlist_id)
REFERENCES Playlist(id);
go
go

COMMIT TRANSACTION;


