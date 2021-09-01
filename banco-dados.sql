IF NOT EXISTS (SELECT 0 FROM sys.databases WHERE name = 'AvaliacaoRodrigo')
	CREATE DATABASE AvaliacaoRodrigo
GO

USE AvaliacaoRodrigo
GO

IF (OBJECT_ID('Cliente', 'U') IS NULL)
	CREATE TABLE Cliente
	(
		Id INT IDENTITY(1, 1) NOT NULL,
		Nome VARCHAR(100) NOT NULL,
		Email VARCHAR(255) NOT NULL,
		Nascimento DATE NULL,

		CONSTRAINT PK_Cliente PRIMARY KEY (Id),
		CONSTRAINT UQ_Cliente_Email UNIQUE (Email)
	)
GO

IF (OBJECT_ID('TelefoneTipo', 'U') IS NULL)
	CREATE TABLE TelefoneTipo
	(
		Id INT NOT NULL,
		Codigo CHAR(11) NOT NULL,

		CONSTRAINT PK_TelefoneTipo PRIMARY KEY (Id)
	)
GO

MERGE INTO TelefoneTipo AS Target
USING (VALUES (1, 'Pessoal'), (2, 'Comercial'), (3, 'Residencial'), (4, 'Outros')) AS Source (Id, Codigo) ON (Target.Id = Source.Id)
WHEN MATCHED THEN UPDATE SET Codigo = Source.Codigo
WHEN NOT MATCHED BY Target THEN INSERT (Id, Codigo) VALUES (Source.Id, Source.Codigo)
WHEN NOT MATCHED BY SOURCE THEN DELETE;
GO

IF (OBJECT_ID('ClienteTelefone', 'U') IS NULL)
	CREATE TABLE ClienteTelefone
	(
		Id INT IDENTITY(1, 1) NOT NULL,
		ClienteId INT NOT NULL,
		TelefoneTipoId INT NOT NULL,
		Telefone VARCHAR(13) NOT NULL,

		CONSTRAINT PK_ClienteTelefone PRIMARY KEY (Id),
		CONSTRAINT FK_ClienteTelefone_Cliente FOREIGN KEY (ClienteId) REFERENCES Cliente(Id),
		CONSTRAINT FK_ClienteTelefone_TelefoneTipo FOREIGN KEY (TelefoneTipoId) REFERENCES TelefoneTipo(Id)
	)
GO