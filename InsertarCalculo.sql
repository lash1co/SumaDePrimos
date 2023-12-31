USE [CalculosDB]
GO
/****** Object:  StoredProcedure [dbo].[InsertarCalculo]    Script Date: 28/06/2023 8:28:17 a. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		Luis Alejandro Hernández Carrillo
-- Create date: 26/06/2023
-- Description:	Inserta calculo
-- =============================================
ALTER PROCEDURE [dbo].[InsertarCalculo] 
	-- Add the parameters for the stored procedure here
	@Nombre Varchar(10), 
	@Calculo bigint 
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
