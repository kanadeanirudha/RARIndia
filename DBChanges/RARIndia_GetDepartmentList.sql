USE [Ver1]
GO
/****** Object:  StoredProcedure [dbo].[RARIndia_GetDepartmentList]    Script Date: 1/27/2023 2:50:25 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE OR ALTER PROCEDURE [dbo].[RARIndia_GetDepartmentList]
(   @WhereClause  VARCHAR(MAX),
    @Rows         INT          = 100,
    @PageNo       INT          = 1,
    @Order_BY     VARCHAR(100) = '',
	@RowsCount    INT OUT
    )
	--exec RARIndia_GetDepartmentList @WhereClause=null,@Rows=25,@PageNo=1,@Order_BY='',@RowsCount= null
AS
   
     BEGIN
         SET NOCOUNT ON;
		 
         BEGIN TRY
             DECLARE @SQL NVARCHAR(MAX);
             DECLARE @TBL_DepartmentDetail TABLE
			 (
				 DepartmentId		smallint,
				 DepartmentName		NVARCHAR(100),
				 DeptShortCode		NVARCHAR(100),
				 PrintShortDesc		NVARCHAR(100),
				 RowId				INT,
				 CountNo			INT
			 )

             SET @SQL = '
						;with Cte_filterDepartment AS 
						(
							SELECT ID DepartmentId,DepartmentName,DeptShortCode,PrintShortDesc,'
							+dbo.Fn_GetPagingRowId(@Order_By,'DepartmentName')+',Count(*)Over() CountNo 
							FROM GeneralDepartmentMaster where 1=1  '+[dbo].[Fn_GetFilterWhereClause](@WhereClause)+'
						)
						SELECT  DepartmentId,DepartmentName,DeptShortCode,PrintShortDesc,RowId,CountNo
						FROM Cte_filterDepartment
						'+dbo.Fn_GetPaginationWhereClause(@PageNo,@rows)
						print @SQL
			 INSERT INTO @TBL_DepartmentDetail (DepartmentId,DepartmentName,DeptShortCode,PrintShortDesc,RowId,CountNo )
			
			 EXEC(@SQL)
			
			 SET @RowsCount =ISNULL((SELECT TOP 1 CountNo  FROM @TBL_DepartmentDetail),0)
			 SELECT DepartmentId,DepartmentName,DeptShortCode,PrintShortDesc
			 FROM @TBL_DepartmentDetail
			 	 
         END TRY
         BEGIN CATCH
          DECLARE @Status BIT ;
		  SET @Status = 0;
		  DECLARE @Error_procedure VARCHAR(1000)= ERROR_PROCEDURE(), @ErrorMessage NVARCHAR(MAX)= ERROR_MESSAGE(), @ErrorLine VARCHAR(100)= ERROR_LINE(), @ErrorCall NVARCHAR(MAX)= 'EXEC RARIndia_GetDepartmentList @WhereClause = '+cast (@WhereClause AS VARCHAR(50))+',@Rows='+CAST(@Rows AS VARCHAR(50))+',@PageNo='+CAST(@PageNo AS VARCHAR(50))+',@Order_BY='+@Order_BY+',@RowsCount='+CAST(@RowsCount AS VARCHAR(50))+',@Status='+CAST(@Status AS VARCHAR(10));
              			 
          SELECT 0 AS ID,CAST(0 AS BIT) AS Status;                    
		  

          --EXEC Znode_InsertProcedureErrorLog
          --  @ProcedureName = 'RARIndia_GetDepartmentList',
          --  @ErrorInProcedure = @Error_procedure,
          --  @ErrorMessage = @ErrorMessage,
          --  @ErrorLine = @ErrorLine,
          --  @ErrorCall = @ErrorCall;
         END CATCH;
     END;
