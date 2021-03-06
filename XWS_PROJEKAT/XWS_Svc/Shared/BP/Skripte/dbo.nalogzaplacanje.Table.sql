USE [XWS2017]
GO
/****** Object:  Table [dbo].[nalogzaplacanje]    Script Date: 6/22/2017 5:09:39 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[nalogzaplacanje](
	[idnalogazaplacanje] [int] NOT NULL,
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
 CONSTRAINT [nalogzaplacanje_pk] PRIMARY KEY CLUSTERED 
(
	[idnalogazaplacanje] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
