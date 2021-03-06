USE [XWS2017]
GO
/****** Object:  Table [dbo].[rtgsnalog]    Script Date: 6/22/2017 5:09:39 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[rtgsnalog](
	[idrtgsnaloga] [int] NOT NULL,
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
