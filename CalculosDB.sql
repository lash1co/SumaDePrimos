USE [master]
GO
/****** Object:  Database [CalculosDB]    Script Date: 28/06/2023 1:24:19 p. m. ******/
CREATE DATABASE [CalculosDB]
 CONTAINMENT = NONE
 ON  PRIMARY 
( NAME = N'CalculosDB', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL15.SQLEXPRESS\MSSQL\DATA\CalculosDB.mdf' , SIZE = 8192KB , MAXSIZE = UNLIMITED, FILEGROWTH = 65536KB )
 LOG ON 
( NAME = N'CalculosDB_log', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL15.SQLEXPRESS\MSSQL\DATA\CalculosDB_log.ldf' , SIZE = 8192KB , MAXSIZE = 2048GB , FILEGROWTH = 65536KB )
 WITH CATALOG_COLLATION = DATABASE_DEFAULT
GO
ALTER DATABASE [CalculosDB] SET COMPATIBILITY_LEVEL = 150
GO
IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [CalculosDB].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO
ALTER DATABASE [CalculosDB] SET ANSI_NULL_DEFAULT OFF 
GO
ALTER DATABASE [CalculosDB] SET ANSI_NULLS OFF 
GO
ALTER DATABASE [CalculosDB] SET ANSI_PADDING OFF 
GO
ALTER DATABASE [CalculosDB] SET ANSI_WARNINGS OFF 
GO
ALTER DATABASE [CalculosDB] SET ARITHABORT OFF 
GO
ALTER DATABASE [CalculosDB] SET AUTO_CLOSE OFF 
GO
ALTER DATABASE [CalculosDB] SET AUTO_SHRINK OFF 
GO
ALTER DATABASE [CalculosDB] SET AUTO_UPDATE_STATISTICS ON 
GO
ALTER DATABASE [CalculosDB] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO
ALTER DATABASE [CalculosDB] SET CURSOR_DEFAULT  GLOBAL 
GO
ALTER DATABASE [CalculosDB] SET CONCAT_NULL_YIELDS_NULL OFF 
GO
ALTER DATABASE [CalculosDB] SET NUMERIC_ROUNDABORT OFF 
GO
ALTER DATABASE [CalculosDB] SET QUOTED_IDENTIFIER OFF 
GO
ALTER DATABASE [CalculosDB] SET RECURSIVE_TRIGGERS OFF 
GO
ALTER DATABASE [CalculosDB] SET  DISABLE_BROKER 
GO
ALTER DATABASE [CalculosDB] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO
ALTER DATABASE [CalculosDB] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO
ALTER DATABASE [CalculosDB] SET TRUSTWORTHY OFF 
GO
ALTER DATABASE [CalculosDB] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO
ALTER DATABASE [CalculosDB] SET PARAMETERIZATION SIMPLE 
GO
ALTER DATABASE [CalculosDB] SET READ_COMMITTED_SNAPSHOT OFF 
GO
ALTER DATABASE [CalculosDB] SET HONOR_BROKER_PRIORITY OFF 
GO
ALTER DATABASE [CalculosDB] SET RECOVERY SIMPLE 
GO
ALTER DATABASE [CalculosDB] SET  MULTI_USER 
GO
ALTER DATABASE [CalculosDB] SET PAGE_VERIFY CHECKSUM  
GO
ALTER DATABASE [CalculosDB] SET DB_CHAINING OFF 
GO
ALTER DATABASE [CalculosDB] SET FILESTREAM( NON_TRANSACTED_ACCESS = OFF ) 
GO
ALTER DATABASE [CalculosDB] SET TARGET_RECOVERY_TIME = 60 SECONDS 
GO
ALTER DATABASE [CalculosDB] SET DELAYED_DURABILITY = DISABLED 
GO
ALTER DATABASE [CalculosDB] SET ACCELERATED_DATABASE_RECOVERY = OFF  
GO
ALTER DATABASE [CalculosDB] SET QUERY_STORE = OFF
GO
USE [CalculosDB]
GO
/****** Object:  Table [dbo].[Calculo]    Script Date: 28/06/2023 1:24:20 p. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Calculo](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Usuario] [nvarchar](50) NULL,
	[Respuesta] [bigint] NULL,
	[Fecha] [datetime] NULL,
 CONSTRAINT [PK_Calculo] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
SET IDENTITY_INSERT [dbo].[Calculo] ON 

INSERT [dbo].[Calculo] ([Id], [Usuario], [Respuesta], [Fecha]) VALUES (2, N'juan', 2150, CAST(N'2023-05-27T11:45:36.850' AS DateTime))
INSERT [dbo].[Calculo] ([Id], [Usuario], [Respuesta], [Fecha]) VALUES (3, N'pedro', 58418, CAST(N'2023-06-27T15:14:44.540' AS DateTime))
INSERT [dbo].[Calculo] ([Id], [Usuario], [Respuesta], [Fecha]) VALUES (4, N'juliana', 84300, CAST(N'2023-06-27T16:20:27.037' AS DateTime))
INSERT [dbo].[Calculo] ([Id], [Usuario], [Respuesta], [Fecha]) VALUES (5, N'juan', 58418, CAST(N'2023-06-27T22:13:19.590' AS DateTime))
INSERT [dbo].[Calculo] ([Id], [Usuario], [Respuesta], [Fecha]) VALUES (6, N'juliana', 583341668, CAST(N'2023-06-28T08:38:52.550' AS DateTime))
SET IDENTITY_INSERT [dbo].[Calculo] OFF
GO
/****** Object:  StoredProcedure [dbo].[BuscarCalculos]    Script Date: 28/06/2023 1:24:20 p. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		Luis Alejandro Hernández Carrillo
-- Create date: 26/06/2023
-- Description:	Buscar por nombre, respuesta máxima y mínima
-- =============================================
CREATE PROCEDURE [dbo].[BuscarCalculos]
	-- Add the parameters for the stored procedure here
	@Nombre Varchar(10) = NULL, 
	@RespuestaMax INT = NULL,
	@RespuestaMin INT = NULL
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;
	Declare @SQLQuery varchar(Max) = 'SELECT * from dbo.Calculo Where 1=1';

    -- Insert statements for procedure here
	IF @Nombre IS NOT NULL
		SET @SQLQuery += ' AND Usuario like ''%'+@Nombre+'%''';
	IF @RespuestaMax IS NOT NULL AND @RespuestaMax != 0 
		SET @SQLQuery += ' AND Respuesta < '+ CONVERT(VARCHAR, @RespuestaMax);
	IF @RespuestaMin IS NOT NULL AND @RespuestaMin != 0
		SET @SQLQuery += ' AND Respuesta > '+ CONVERT(VARCHAR, @RespuestaMin);

	EXECUTE sp_sqlexec @SQLQuery;

END
GO
/****** Object:  StoredProcedure [dbo].[InsertarCalculo]    Script Date: 28/06/2023 1:24:20 p. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		Luis Alejandro Hernández Carrillo
-- Create date: 26/06/2023
-- Description:	Inserta calculo
-- =============================================
CREATE PROCEDURE [dbo].[InsertarCalculo] 
	-- Add the parameters for the stored procedure here
	@Nombre Varchar(10), 
	@Calculo int 
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;
	Declare @Fecha DateTime;
	set @Fecha = GETDATE();

    -- Insert statements for procedure here
	INSERT INTO CalculosDB.dbo.Calculo(Usuario, Respuesta, Fecha) Values (@Nombre, @Calculo, @Fecha)
END
GO
USE [master]
GO
ALTER DATABASE [CalculosDB] SET  READ_WRITE 
GO
