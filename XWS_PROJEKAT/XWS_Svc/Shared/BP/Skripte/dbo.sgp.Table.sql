USE [XWS2017]
GO
/****** Object:  Table [dbo].[sgp]    Script Date: 6/22/2017 5:09:39 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[sgp](
	[idstavkegrupnogplacanja] [int] NOT NULL,
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
