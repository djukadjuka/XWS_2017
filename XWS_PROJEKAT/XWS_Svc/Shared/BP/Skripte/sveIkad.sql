USE [master]
GO
/****** Object:  Database [XWS2017]    Script Date: 6/22/2017 5:13:34 PM ******/
CREATE DATABASE [XWS2017]
 CONTAINMENT = NONE
 ON  PRIMARY 
( NAME = N'XWS2017', FILENAME = N'c:\Program Files\Microsoft SQL Server\MSSQL11.SQLEXPRESS\MSSQL\DATA\XWS2017.mdf' , SIZE = 3072KB , MAXSIZE = UNLIMITED, FILEGROWTH = 1024KB )
 LOG ON 
( NAME = N'XWS2017_log', FILENAME = N'c:\Program Files\Microsoft SQL Server\MSSQL11.SQLEXPRESS\MSSQL\DATA\XWS2017_log.ldf' , SIZE = 1024KB , MAXSIZE = 2048GB , FILEGROWTH = 10%)
GO
ALTER DATABASE [XWS2017] SET COMPATIBILITY_LEVEL = 110
GO
IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [XWS2017].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO
ALTER DATABASE [XWS2017] SET ANSI_NULL_DEFAULT OFF 
GO
ALTER DATABASE [XWS2017] SET ANSI_NULLS OFF 
GO
ALTER DATABASE [XWS2017] SET ANSI_PADDING OFF 
GO
ALTER DATABASE [XWS2017] SET ANSI_WARNINGS OFF 
GO
ALTER DATABASE [XWS2017] SET ARITHABORT OFF 
GO
ALTER DATABASE [XWS2017] SET AUTO_CLOSE OFF 
GO
ALTER DATABASE [XWS2017] SET AUTO_SHRINK OFF 
GO
ALTER DATABASE [XWS2017] SET AUTO_UPDATE_STATISTICS ON 
GO
ALTER DATABASE [XWS2017] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO
ALTER DATABASE [XWS2017] SET CURSOR_DEFAULT  GLOBAL 
GO
ALTER DATABASE [XWS2017] SET CONCAT_NULL_YIELDS_NULL OFF 
GO
ALTER DATABASE [XWS2017] SET NUMERIC_ROUNDABORT OFF 
GO
ALTER DATABASE [XWS2017] SET QUOTED_IDENTIFIER OFF 
GO
ALTER DATABASE [XWS2017] SET RECURSIVE_TRIGGERS OFF 
GO
ALTER DATABASE [XWS2017] SET  DISABLE_BROKER 
GO
ALTER DATABASE [XWS2017] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO
ALTER DATABASE [XWS2017] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO
ALTER DATABASE [XWS2017] SET TRUSTWORTHY OFF 
GO
ALTER DATABASE [XWS2017] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO
ALTER DATABASE [XWS2017] SET PARAMETERIZATION SIMPLE 
GO
ALTER DATABASE [XWS2017] SET READ_COMMITTED_SNAPSHOT OFF 
GO
ALTER DATABASE [XWS2017] SET HONOR_BROKER_PRIORITY OFF 
GO
ALTER DATABASE [XWS2017] SET RECOVERY SIMPLE 
GO
ALTER DATABASE [XWS2017] SET  MULTI_USER 
GO
ALTER DATABASE [XWS2017] SET PAGE_VERIFY CHECKSUM  
GO
ALTER DATABASE [XWS2017] SET DB_CHAINING OFF 
GO
ALTER DATABASE [XWS2017] SET FILESTREAM( NON_TRANSACTED_ACCESS = OFF ) 
GO
ALTER DATABASE [XWS2017] SET TARGET_RECOVERY_TIME = 0 SECONDS 
GO
USE [XWS2017]
GO
/****** Object:  Table [dbo].[faktura]    Script Date: 6/22/2017 5:13:34 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[faktura](
	[idfakture] [int] NOT NULL,
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
/****** Object:  Table [dbo].[nalogzagp]    Script Date: 6/22/2017 5:13:34 PM ******/
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
/****** Object:  Table [dbo].[nalogzaplacanje]    Script Date: 6/22/2017 5:13:34 PM ******/
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
/****** Object:  Table [dbo].[porukaoodobrenju]    Script Date: 6/22/2017 5:13:34 PM ******/
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
/****** Object:  Table [dbo].[porukaozaduzenju]    Script Date: 6/22/2017 5:13:34 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[porukaozaduzenju](
	[idporukeozaduzenju] [int] NOT NULL,
	[idporuke] [varchar](50) NULL,
	[swiftbankeduznika] [varchar](8) NULL,
	[obracunskiracunbankeduznika] [varchar](20) NULL,
	[idporukenaloga] [varchar](50) NULL,
	[datumvalute] [date] NULL,
	[iznos] [numeric](15, 2) NULL,
	[sifravalute] [char](3) NULL,
 CONSTRAINT [porukaozaduzenju_pk] PRIMARY KEY CLUSTERED 
(
	[idporukeozaduzenju] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[presek]    Script Date: 6/22/2017 5:13:34 PM ******/
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
/****** Object:  Table [dbo].[rtgsnalog]    Script Date: 6/22/2017 5:13:34 PM ******/
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
/****** Object:  Table [dbo].[sgp]    Script Date: 6/22/2017 5:13:34 PM ******/
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
/****** Object:  Table [dbo].[stavkafakture]    Script Date: 6/22/2017 5:13:34 PM ******/
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
/****** Object:  Table [dbo].[stavkapreseka]    Script Date: 6/22/2017 5:13:34 PM ******/
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
/****** Object:  Table [dbo].[zahtevzadobijanjeizvoda]    Script Date: 6/22/2017 5:13:34 PM ******/
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
ALTER TABLE [dbo].[sgp]  WITH CHECK ADD  CONSTRAINT [sgp_nalogzagp_fk] FOREIGN KEY([nalogzagp_idnalogazagp])
REFERENCES [dbo].[nalogzagp] ([idnzgp])
GO
ALTER TABLE [dbo].[sgp] CHECK CONSTRAINT [sgp_nalogzagp_fk]
GO
ALTER TABLE [dbo].[stavkafakture]  WITH CHECK ADD  CONSTRAINT [stavkafakture_faktura_fk] FOREIGN KEY([faktura_idfakture])
REFERENCES [dbo].[faktura] ([idfakture])
GO
ALTER TABLE [dbo].[stavkafakture] CHECK CONSTRAINT [stavkafakture_faktura_fk]
GO
ALTER TABLE [dbo].[stavkapreseka]  WITH CHECK ADD  CONSTRAINT [stavkapreseka_presek_fk] FOREIGN KEY([presek_idpreseka])
REFERENCES [dbo].[presek] ([idpreseka])
GO
ALTER TABLE [dbo].[stavkapreseka] CHECK CONSTRAINT [stavkapreseka_presek_fk]
GO
USE [master]
GO
ALTER DATABASE [XWS2017] SET  READ_WRITE 
GO
