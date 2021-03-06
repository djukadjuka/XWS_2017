USE [XWS2017]
GO
/****** Object:  Table [dbo].[stavkapreseka]    Script Date: 6/22/2017 5:09:39 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[stavkapreseka](
	[idstavkepreseka] [int] NOT NULL,
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
