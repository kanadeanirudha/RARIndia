USE [Ver1]
GO
/****** Object:  StoredProcedure [dbo].[USP_GetBalancesheetList]    Script Date: 1/4/2023 5:38:05 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO




------------------------------------------------------------------------------------------------------------

------------------------------------------------------------------------------------------------------------
-- Stored procedure that will select an existing row from the table '[USP_GetBalancesheetList]]'
-- based on the Primary Key.
-- Gets: @iAdminRoleId int,
-- Return: @iErrorCode int 
 
------------------------------------------------------------------------------------------------------------
ALTER PROCEDURE [dbo].[USP_GetBalancesheetList]
	@iAdminRoleId int,	
	@iErrorCode int OUTPUT
AS
BEGIN
	BEGIN TRY

		;with cteAdminRoleCentreRights as(
		select
			A.AdminRoleMasterID,
			A.AdminRoleRightTypeID,
			B.RightName,
			A.CentreCode,
			A.ID,
			D.CentreName,
			E.ID as BalsheetID,
			E.ActBalsheetHeadDesc,
			D.HoCoRoScFlag
		from
			AdminRoleCentreRights A
			inner join AdminRoleRightType B on (A.AdminRoleRightTypeID = B.ID
												and A.AdminRoleMasterID=@iAdminRoleId
												and B.IsDeleted = 0
												and A.IsDeleted = 0
												and A.IsActive = 1
												and (B.RightName='Account Manager' or B.RightName='Sales Manager'or B.RightName='HR Manager'
													or B.RightName='Purchase Manager'or B.RightName='Store Manager')
												)
			inner join AdminRoleMaster C on (A.AdminRoleMasterID = C.ID
											--and C.MonitoringLevel = 'Self'
											and C.IsActive = 1)
			inner join OrganisationStudyCentreMaster D on (A.CentreCode = D.CentreCode)
			Inner Join ActBalsheetMaster E on (
										E.CentreCode = D.CentreCode
										And E.IsActive = 1
										and E.IsDeleted=0		
										)
		)

		Select 
			CentreCode
			,CentreName
			,HoCoRoScFlag
			--,IsSuperUser		
			--,IsAcadMgr		
			--,IsEstMgr		
			--,IsFinMgr		
			--,IsAdmMgr		
			,'Centre' as ScopeIdentity
			--,EntityType 
			,BalsheetID           
			,ActBalsheetHeadDesc 
		from  
			cteAdminRoleCentreRights 
		group by CentreCode,CentreName,HoCoRoScFlag,BalsheetID,ActBalsheetHeadDesc 
		order by 
			HoCoRoScFlag ,ActBalsheetHeadDesc


			SELECT  @iErrorCode =@@ERROR

	  
	  END TRY
	BEGIN CATCH
		
		SELECT  @iErrorCode =@@ERROR
	END CATCH
End	










