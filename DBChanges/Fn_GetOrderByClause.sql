USE [Ver1]
GO
/****** Object:  UserDefinedFunction [dbo].[Fn_GetOrderByClause]    Script Date: 12/14/2022 4:35:05 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Create the Order by clase for dyanamic statement  
-- =============================================
Create OR ALTER FUNCTION [dbo].[Fn_GetOrderByClause]
(
  @OrderBy NVARCHAR(MAX)
  ,@DefaultOrderBy NVARCHAR(1000)
)
RETURNS NVARCHAR(MAX)
AS
BEGIN
	-- Declare the return variable here
	DECLARE @Order  NVARCHAR(max)

	SET @Order = ' Order By '+CASE WHEN @OrderBy = '' OR @OrderBy IS NULL  THEN @DefaultOrderBy ELSE @OrderBy END 

    RETURN @Order
   

END
