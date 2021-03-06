USE [XWS2017]
GO
/****** Object:  Table [dbo].[nalogzagp]    Script Date: 6/22/2017 5:09:39 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[nalogzagp](
	[idnzgp] [int] NOT NULL,
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
