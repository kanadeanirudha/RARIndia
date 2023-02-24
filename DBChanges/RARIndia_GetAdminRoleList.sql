USE [Ver1]
GO
/****** Object:  StoredProcedure [dbo].[RARIndia_GetAdminRoleList]    Script Date: 2/23/2023 7:52:54 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
Create Or ALTER PROCEDURE [dbo].[RARIndia_GetAdminRoleList]
(   
	@CentreCode		VARCHAR(60),
	@DepartmentId   INT,
    @WhereClause	VARCHAR(MAX),
    @Rows			INT          = 100,
    @PageNo			INT          = 1,
    @Order_BY		VARCHAR(100) = '',
	@RowsCount		INT OUT
)
	--exec RARIndia_GetAdminRoleList @CentreCode='HO',@DepartmentId=7,@WhereClause=null,@Rows=25,@PageNo=1,@Order_BY='',@RowsCount= null

AS
   
     BEGIN
         SET NOCOUNT ON;
		 
         BEGIN TRY
             DECLARE @SQL NVARCHAR(MAX);
             DECLARE @TBL_AdminRoleDetail TABLE
			 (
				 AdminRoleMasterId					smallint,
				 AdminRoleCode					NVARCHAR(60),
				 SactionedPostDescription		NVARCHAR(400),
				 MonitoringLevel				NVARCHAR(12),
				 IsActive						bit,
				 RowId							INT,
				 CountNo						INT
			 )
			
             SET @SQL = '
						;with Cte_filterAdminRole AS 
						(
							SELECT a.ID AdminRoleMasterId,a.AdminRoleCode,a.SanctPostName SactionedPostDescription,a.IsActive,a.MonitoringLevel,'
							+dbo.Fn_GetPagingRowId(@Order_By,'AdminRoleCode')+',Count(*)Over() CountNo 
							FROM AdminRoleMaster a
							inner join AdminSnPosts b on (a.AdminSnPostID = b.ID and b.DepartmentID='+CAST(@DepartmentId AS VARCHAR(10))+' ) 
							where b.CentreCode='''+@CentreCode+''' '+ [dbo].[Fn_GetFilterWhereClause](@WhereClause)+'
						)
						SELECT  AdminRoleMasterId,AdminRoleCode,SactionedPostDescription,IsActive,MonitoringLevel,RowId,CountNo
						FROM Cte_filterAdminRole
						'+dbo.Fn_GetPaginationWhereClause(@PageNo,@rows)
						print @SQL
			 INSERT INTO @TBL_AdminRoleDetail (AdminRoleMasterId,AdminRoleCode,SactionedPostDescription,IsActive,MonitoringLevel,RowId,CountNo )
			 EXEC(@SQL)

			 SET @RowsCount =ISNULL((SELECT TOP 1 CountNo  FROM @TBL_AdminRoleDetail),0)
			 SELECT AdminRoleMasterId,AdminRoleCode,SactionedPostDescription,IsActive,MonitoringLevel
			 FROM @TBL_AdminRoleDetail
			 	 
         END TRY
         BEGIN CATCH
          DECLARE @Status BIT ;
		  SET @Status = 0;
		  DECLARE @Error_procedure VARCHAR(1000)= ERROR_PROCEDURE(), @ErrorMessage NVARCHAR(MAX)= ERROR_MESSAGE(), @ErrorLine VARCHAR(100)= ERROR_LINE(), @ErrorCall NVARCHAR(MAX)= 'EXEC RARIndia_GetAdminRoleList @WhereClause = '+cast (@WhereClause AS VARCHAR(50))+',@Rows='+CAST(@Rows AS VARCHAR(50))+',@PageNo='+CAST(@PageNo AS VARCHAR(50))+',@Order_BY='+@Order_BY+',@RowsCount='+CAST(@RowsCount AS VARCHAR(50))+',@Status='+CAST(@Status AS VARCHAR(10));
              			 
          SELECT 0 AS ID,CAST(0 AS BIT) AS Status;                    
		  

          --EXEC Znode_InsertProcedureErrorLog
          --  @ProcedureName = 'RARIndia_GetAdminRoleList',
          --  @ErrorInProcedure = @Error_procedure,
          --  @ErrorMessage = @ErrorMessage,
          --  @ErrorLine = @ErrorLine,
          --  @ErrorCall = @ErrorCall;
         END CATCH;
     END;
