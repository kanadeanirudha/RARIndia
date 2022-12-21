USE Ver1
GO
/****** Object:  UserDefinedFunction [dbo].[Fn_GetFilterWhereClause]    Script Date: 12/14/2022 4:49:40 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date, ,>
-- Description:	<Description, ,>
-- =============================================
CREATE OR ALTER FUNCTION [dbo].[Fn_GetFilterWhereClause]
(
  @WhereClause NVARCHAR(MAX)
 )
RETURNS NVARCHAR(MAX)
AS
BEGIN
	-- Declare the return variable here
	DECLARE @Where  NVARCHAR(max)=''

	SET @Where = CASE WHEN @WhereClause = '' OR @WhereClause IS NULL  THEN '' ELSE ' AND '+' '+@WhereClause END 

    RETURN @Where
   

END
