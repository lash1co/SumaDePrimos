USE [CalculosDB]
GO
/****** Object:  StoredProcedure [dbo].[BuscarCalculos]    Script Date: 28/06/2023 8:28:46 a. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		Luis Alejandro Hernández Carrillo
-- Create date: 26/06/2023
-- Description:	Buscar por nombre, respuesta máxima y mínima
-- =============================================
ALTER PROCEDURE [dbo].[BuscarCalculos]
	-- Add the parameters for the stored procedure here
	@Nombre Varchar(10) = NULL, 
	@RespuestaMax BIGINT = NULL,
	@RespuestaMin BIGINT = NULL
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
