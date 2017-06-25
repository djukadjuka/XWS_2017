USE [XWS_CB_2017]
GO

/****** Object:  Table [dbo].[rtgsnalog]    Script Date: 6/24/2017 10:12:55 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[centralnabanka](
	[idcb] [int] IDENTITY(1,1) NOT NULL,
	[naziv] [varchar](255) NULL,
 CONSTRAINT [cb_pk] PRIMARY KEY CLUSTERED 
(
	[idcb] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO

CREATE TABLE [dbo].[banka](
	[idbanke] [int] IDENTITY(1,1) NOT NULL,
	[naziv] [varchar](255) NULL,
	[adresa] [varchar](255) NULL,
 CONSTRAINT [banka_pk] PRIMARY KEY CLUSTERED 
(
	[idbanke] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO

CREATE TABLE [dbo].[obracunskiracun](
	[idobracunskogracuna] [int] IDENTITY(1,1) NOT NULL,
	[stanje][numeric](18,0),
	[idbanke][int]NOT NULL,
	[idcb][int]NOT NULL,
	[brojobracunskogracuna][numeric](18,0)NULL,
	[SWIFTkod] [char](8) NULL,
 CONSTRAINT [idobracunskogracuna_pk] PRIMARY KEY CLUSTERED 
(
	[idobracunskogracuna] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO


ALTER TABLE [dbo].[obracunskiracun]  WITH CHECK ADD  CONSTRAINT [racun_banka_fk] FOREIGN KEY([idbanke])
REFERENCES [dbo].[banka] ([idbanke])
GO

ALTER TABLE [dbo].[obracunskiracun] CHECK CONSTRAINT [racun_banka_fk]
GO

ALTER TABLE [dbo].[obracunskiracun]  WITH CHECK ADD  CONSTRAINT [racun_cb_fk] FOREIGN KEY([idcb])
REFERENCES [dbo].[centralnabanka] ([idcb])
GO

ALTER TABLE [dbo].[obracunskiracun] CHECK CONSTRAINT [racun_cb_fk]
GO

CREATE TABLE [dbo].[rtgsnalog](
	[idrtgsnaloga] [int] IDENTITY(1,1) NOT NULL,
	[idporuke] [varchar](50) NULL,
	[swiftbankaduznika] [varchar](8) NULL,
	[obracunskiracunbankeduznika] [varchar](18) NULL,
	[swiftbankapoverioca] [varchar](8) NULL,
	[obracunskiracunbankepoverioca] [varchar](18) NULL,
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
	[sifravalute] [char](3) NULL,
 CONSTRAINT [rtgsnalog_pk] PRIMARY KEY CLUSTERED 
(
	[idrtgsnaloga] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO

CREATE TABLE [dbo].[nalogzagp](
	[idnzgp] [int] IDENTITY(1,1) NOT NULL,
	[idporuke] [varchar](50) NULL,
	[swiftbankeduznika] [varchar](8) NULL,
	[obracunskiracunbankeduznika] [varchar](18) NULL,
	[swiftbankepoverioca] [varchar](8) NULL,
	[obracunskiracunbankepoverioca] [varchar](18) NULL,
	[ukupaniznos] [numeric](15, 2) NULL,
	[sifravalute] [char](3) NULL,
	[datumvalute] [date] NULL,
	[datum] [date] NULL,
 CONSTRAINT [nalogzagp_pk] PRIMARY KEY CLUSTERED 
(
	[idnzgp] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO

CREATE TABLE [dbo].[sgp](
	[idstavkegrupnogplacanja] [int] IDENTITY(1,1) NOT NULL,
	[idnalogazaplacanje] [varchar](50) NULL,
	[duznik] [varchar](255) NULL,
	[svrhaplacanja] [varchar](255) NULL,
	[primalac] [varchar](255) NULL,
	[datumnaloga] [date] NULL,
	[racunduznika] [varchar](18) NULL,
	[modelzaduzenja] [numeric](18, 0) NULL,
	[pozivnabrzaduzenja] [varchar](20) NULL,
	[racunpoverioca] [varchar](18) NULL,
	[modelodobrenja] [numeric](18, 0) NULL,
	[pozivnabrodobrenja] [varchar](20) NULL,
	[iznos] [numeric](15, 2) NULL,
	[sifravalute] [char](3) NULL,
	[nalogzagp_idnalogazagp] [int] NOT NULL,
 CONSTRAINT [sgp_pk] PRIMARY KEY CLUSTERED 
(
	[idstavkegrupnogplacanja] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO

ALTER TABLE [dbo].[sgp]  WITH CHECK ADD  CONSTRAINT [sgp_nalogzagp_fk] FOREIGN KEY([nalogzagp_idnalogazagp])
REFERENCES [dbo].[nalogzagp] ([idnzgp])
GO

ALTER TABLE [dbo].[sgp] CHECK CONSTRAINT [sgp_nalogzagp_fk]
GO

CREATE TABLE [dbo].[porukaozaduzenju](
	[idporukeozaduzenju] [int] IDENTITY(1,1) NOT NULL,
	[idporuke] [varchar](50) NULL,
	[swiftbankeduznika] [varchar](8) NULL,
	[obracunskiracunbankeduznika] [varchar](20) NULL,
	[idporukenaloga] [varchar](50) NULL,
	[datumvalute] [date] NULL,
	[iznos] [numeric](15, 2) NULL,
	[sifravalute] [char](3) NULL,
 CONSTRAINT [porukaozaduzenju_pk] PRIMARY KEY CLUSTERED 
(
	[idporukeozaduzenju] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO

CREATE TABLE [dbo].[porukaoodobrenju](
	[idporukeoodobrenju] [int] IDENTITY(1,1) NOT NULL,
	[idporuke] [varchar](50) NULL,
	[swiftbankepoverioca] [varchar](8) NULL,
	[obracunskiracunbankepoverioca] [varchar](20) NULL,
	[idporukenaloga] [varchar](50) NULL,
	[datumvalute] [date] NULL,
	[iznos] [numeric](15, 2) NULL,
	[sifravalute] [char](3) NULL,
 CONSTRAINT [porukaoodobrenju_pk] PRIMARY KEY CLUSTERED 
(
	[idporukeoodobrenju] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO

