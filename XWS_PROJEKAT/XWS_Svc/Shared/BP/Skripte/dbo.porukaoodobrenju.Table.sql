USE [XWS2017]
GO
/****** Object:  Table [dbo].[porukaoodobrenju]    Script Date: 6/22/2017 5:09:39 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[porukaoodobrenju](
	[idporukeoodobrenju] [int] NOT NULL,
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
