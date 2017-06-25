USE [XWS_BANKA_2017]
GO

/****** Object:  Table [dbo].[firma]    Script Date: 6/24/2017 10:01:21 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[firma](
	[idfirme] [int] IDENTITY(1,1) NOT NULL,
	[naziv] [varchar](255) NULL,
	[adresa] [varchar](255) NULL,
	[pib] [varchar](255) NULL,
 CONSTRAINT [firma_pk] PRIMARY KEY CLUSTERED 
(
	[idfirme] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO


CREATE TABLE [dbo].[banka](
	[idbanke] [int] IDENTITY(1,1) NOT NULL,
	[naziv] [varchar](255) NULL,
	[adresa] [varchar](255) NULL,
	[obracunskiracun] [numeric](18,0) NULL,
	[SWIFTkod] [char](8) NULL,
 CONSTRAINT [banka_pk] PRIMARY KEY CLUSTERED 
(
	[idbanke] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO

CREATE TABLE [dbo].[racun](
	[idracuna] [int] IDENTITY(1,1) NOT NULL,
	[predhodnostanje][numeric](18,0),
	[trenutnostanje][numeric](18,0),
	[idbanke][int]NOT NULL,
	[idfirme][int]NOT NULL,
	[brojracuna][numeric](18,0)NULL,
	[datum][date]NULL,
 CONSTRAINT [racun_pk] PRIMARY KEY CLUSTERED 
(
	[idracuna] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO


ALTER TABLE [dbo].[racun]  WITH CHECK ADD  CONSTRAINT [racun_banka_fk] FOREIGN KEY([idbanke])
REFERENCES [dbo].[banka] ([idbanke])
GO

ALTER TABLE [dbo].[racun] CHECK CONSTRAINT [racun_banka_fk]
GO

ALTER TABLE [dbo].[racun]  WITH CHECK ADD  CONSTRAINT [racun_firma_fk] FOREIGN KEY([idfirme])
REFERENCES [dbo].[firma] ([idfirme])
GO

ALTER TABLE [dbo].[racun] CHECK CONSTRAINT [racun_firma_fk]
GO

CREATE TABLE [dbo].[nalogzaplacanje](
	[idnalogazaplacanje] [int] IDENTITY(1,1) NOT NULL,
	[idporuke] [varchar](50) NULL,
	[duznik] [varchar](255) NULL,
	[svrhaplacanja] [varchar](255) NULL,
	[primalac] [varchar](255) NULL,
	[datumnaloga] [date] NULL,
	[datumvalute] [date] NULL,
	[racunduznika] [varchar](18) NULL,
	[modelzaduzenja] [numeric](18, 0) NULL,
	[pozivnabrzaduzenja] [varchar](20) NULL,
	[racunpoverioca] [varchar](18) NULL,
	[modelodobrenja] [numeric](18, 0) NULL,
	[pozivnabrodobrenja] [numeric](18, 0) NULL,
	[iznos] [numeric](15, 2) NULL,
	[oznakavalute] [char](3) NULL,
	[hitno] [char](1) NULL,
	[status][char](1) NULL,
 CONSTRAINT [nalogzaplacanje_pk] PRIMARY KEY CLUSTERED 
(
	[idnalogazaplacanje] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO

CREATE TABLE [dbo].[zahtevzadobijanjeizvoda](
	[idzzdi] [int] IDENTITY(1,1) NOT NULL,
	[brracuna] [varchar](18) NULL,
	[datum] [date] NULL,
	[rednibrpreseka] [numeric](18, 0) NULL,
 CONSTRAINT [zahtevzadobijanjeizvoda_pk] PRIMARY KEY CLUSTERED 
(
	[idzzdi] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO

CREATE TABLE [dbo].[presek](
	[idpreseka] [int] IDENTITY(1,1) NOT NULL,
	[brracuna] [varchar](18) NULL,
	[datumnaloga] [date] NULL,
	[brpreseka] [numeric](18, 0) NULL,
	[prethodnostanje] [numeric](15, 2) NULL,
	[brpromenaukorist] [numeric](18, 0) NULL,
	[ukupnoukorist] [numeric](15, 2) NULL,
	[brpromenanateret] [numeric](18, 0) NULL,
	[ukupnonateret] [numeric](15, 2) NULL,
	[novostanje] [numeric](15, 2) NULL,
 CONSTRAINT [presek_pk] PRIMARY KEY CLUSTERED 
(
	[idpreseka] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO

CREATE TABLE [dbo].[stavkapreseka](
	[idstavkepreseka] [int] IDENTITY(1,1) NOT NULL,
	[duznik] [varchar](255) NULL,
	[svrhaplacanja] [varchar](255) NULL,
	[primalac] [varchar](255) NULL,
	[datumnaloga] [date] NULL,
	[datumvalute] [date] NULL,
	[racunduznika] [varchar](18) NULL,
	[modelzaduzenja] [numeric](18, 0) NULL,
	[pozivnabrzaduzenja] [varchar](20) NULL,
	[racunpoverioca] [varchar](18) NULL,
	[modelodobrenja] [numeric](18, 0) NULL,
	[pozivnabrodobrenja] [varchar](20) NULL,
	[iznos] [numeric](15, 2) NULL,
	[smer] [char](1) NULL,
	[presek_idpreseka] [int] NOT NULL,
 CONSTRAINT [stavkapreseka_pk] PRIMARY KEY CLUSTERED 
(
	[idstavkepreseka] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO

ALTER TABLE [dbo].[stavkapreseka]  WITH CHECK ADD  CONSTRAINT [stavkapreseka_presek_fk] FOREIGN KEY([presek_idpreseka])
REFERENCES [dbo].[presek] ([idpreseka])
GO

ALTER TABLE [dbo].[stavkapreseka] CHECK CONSTRAINT [stavkapreseka_presek_fk]
GO

