USE [XWS2017]
GO
/****** Object:  Table [dbo].[presek]    Script Date: 6/22/2017 5:09:39 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[presek](
	[idpreseka] [int] NOT NULL,
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
