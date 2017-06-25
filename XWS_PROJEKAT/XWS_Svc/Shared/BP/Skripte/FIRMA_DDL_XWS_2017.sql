USE [XWS_FIRMA_2017]
GO

/****** Object:  Table [dbo].[faktura]    Script Date: 6/24/2017 9:54:37 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[faktura](
	[idfakture] [int] IDENTITY(1,1) NOT NULL,
	[idporuke] [varchar](50) NULL,
	[nazivdobavljaca] [varchar](255) NULL,
	[adresadobavljaca] [varchar](255) NULL,
	[pibdobavljaca] [varchar](11) NULL,
	[nazivkupca] [varchar](55) NULL,
	[adresakupca] [varchar](55) NULL,
	[pibkupca] [varchar](11) NULL,
	[brracuna] [numeric](18, 0) NULL,
	[datumracuna] [date] NULL,
	[vrednostrobe] [numeric](15, 2) NULL,
	[vrednostusluga] [numeric](15, 2) NULL,
	[ukupnorobaiusluge] [numeric](15, 2) NULL,
	[ukupanrabat] [numeric](15, 2) NULL,
	[ukupanporez] [numeric](15, 2) NULL,
	[oznakavalute] [char](3) NULL,
	[iznoszauplatu] [numeric](15, 2) NULL,
	[uplatanaracun] [varchar](18) NULL,
	[datumvalute] [date] NULL,
	[status][char](1)NULL
 CONSTRAINT [faktura_pk] PRIMARY KEY CLUSTERED 
(
	[idfakture] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO

CREATE TABLE [dbo].[stavkafakture](
	[idstavke] [int] IDENTITY(1,1) NOT NULL,
	[rednibr] [numeric](18, 0) NULL,
	[nazivrobeiliusluge] [varchar](120) NULL,
	[kolicina] [numeric](10, 2) NULL,
	[jedinicamere] [varchar](6) NULL,
	[jedinicnacena] [numeric](10, 2) NULL,
	[vrednost] [numeric](12, 2) NULL,
	[procenatrabata] [numeric](5, 2) NULL,
	[iznosrabata] [numeric](12, 2) NULL,
	[umanjenozarabat] [numeric](12, 2) NULL,
	[ukupanporez] [numeric](12, 2) NULL,
	[faktura_idfakture] [int] NOT NULL,
 CONSTRAINT [stavkafakture_pk] PRIMARY KEY CLUSTERED 
(
	[idstavke] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO

ALTER TABLE [dbo].[stavkafakture]  WITH CHECK ADD  CONSTRAINT [stavkafakture_faktura_fk] FOREIGN KEY([faktura_idfakture])
REFERENCES [dbo].[faktura] ([idfakture])
GO

ALTER TABLE [dbo].[stavkafakture] CHECK CONSTRAINT [stavkafakture_faktura_fk]
GO

CREATE TABLE [dbo].[firma](
	[idfirme][int] IDENTITY(1,1) NOT NULL,
	[naziv][varchar](255)NULL,
	[adresa][varchar](255)NULL,
	[pib][varchar](255)NULL,
	[brojracuna][numeric](18,0)NULL,
	CONSTRAINT [firma_pk] PRIMARY KEY CLUSTERED
	(
		[idfirme] ASC
	)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO