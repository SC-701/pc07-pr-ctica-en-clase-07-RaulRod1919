-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE ObtenerSubCategoria
	-- Add the parameters for the stored procedure here
	@Id as uniqueidentifier
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
	SELECT   Id, IdCategoria, Nombre
	FROM         SubCategorias
	WHERE     (Id = @Id)
END