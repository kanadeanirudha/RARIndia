USE Ver1
GO
/****** Object:  UserDefinedFunction [dbo].[Fn_GetPagingRowId]    Script Date: 12/14/2022 4:34:46 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO


Create OR ALTER FUNCTION [dbo].[Fn_GetPagingRowId]
(

	 @OrderBY  VARCHAR(1000) 
	,@PkColumn VARCHAR(100)
	
)
RETURNS varchar(2000) 
AS
 ------------------------------------------------------------------------------------------------------------------------------------
 -- Summary 
 -- This Function is used to find the rows as per rows and page    
 -- here return the varchar use in dyanamic statement 
 -- 
------------------------------------------------------------------------------------------------------------------------------------
BEGIN
	-- Declare the return variable here
	DECLARE @Row varchar(2000) 
	 
		SET @Row = ' DENSE_RANK()Over('+dbo.Fn_GetOrderByClause(@OrderBY,@PkColumn)+','+@PkColumn+' ) RowId '

	-- Return the result of the function
	RETURN @Row

END
