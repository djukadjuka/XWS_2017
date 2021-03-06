USE [XWS2017]
GO
/****** Object:  Table [dbo].[zahtevzadobijanjeizvoda]    Script Date: 6/22/2017 5:09:39 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[zahtevzadobijanjeizvoda](
	[idzzdi] [int] NOT NULL,
	[brracuna] [varchar](18) NULL,
	[datum] [date] NULL,
	[rednibrpreseka] [numeric](18, 0) NULL,
 CONSTRAINT [zahtevzadobijanjeizvoda_pk] PRIMARY KEY CLUSTERED 
(
	[idzzdi] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
