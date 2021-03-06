USE [XWS2017]
GO
/****** Object:  Table [dbo].[stavkafakture]    Script Date: 6/22/2017 5:09:39 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[stavkafakture](
	[idstavke] [int] NOT NULL,
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
