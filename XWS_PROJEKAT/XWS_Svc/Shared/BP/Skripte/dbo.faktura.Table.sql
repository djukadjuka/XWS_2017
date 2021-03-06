USE [XWS2017]
GO
/****** Object:  Table [dbo].[faktura]    Script Date: 6/22/2017 5:09:39 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[faktura](
	[idfakture] [int](1,1) NOT NULL,
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
 CONSTRAINT [faktura_pk] PRIMARY KEY CLUSTERED 
(
	[idfakture] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
