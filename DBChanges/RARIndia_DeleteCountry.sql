USE Ver1
GO
/****** Object:  StoredProcedure [dbo].[RARIndia_DeleteCountry]    Script Date: 12/16/2022 5:34:11 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

Create OR ALTER PROCEDURE [dbo].[RARIndia_DeleteCountry]
(
       @CountryId VARCHAR(max) ,
       @Status      BIT OUT
)
AS
/*
Summary: This Procedure is used to delete warehouse details
Unit Testing:
EXEC RARIndia_DeleteCountry 
*/
     BEGIN
         BEGIN TRAN;
         BEGIN TRY
             SET NOCOUNT ON;
             DECLARE @Country TABLE (
                                      CountryId INT
                                      );
             INSERT INTO @Country SELECT item FROM dbo.Split ( @CountryId , ',');
             DECLARE @DeleteAddressId TABLE (
                                            AddressId INT
                                            );
             DECLARE @DeleteWarehouseId TABLE (
                                              WarehouseId INT
                                              );
             INSERT INTO @DeleteWarehouseId
                    SELECT a.WarehouseId
                    FROM [dbo].ZnodeWarehouse AS a INNER JOIN @Warehouse AS b ON ( a.WarehouseId = b.WarehouseId );
             INSERT INTO @DeleteAddressId ( AddressId
                                          )
                    SELECT AddressId
                    FROM ZnodeWarehouseAddress AS a INNER JOIN @DeleteWarehouseId AS b ON ( a.WareHouseId = b.WarehouseId )
                    WHERE NOT EXISTS ( SELECT TOP 1 1
                                       FROM ZnodeWarehouseAddress AS c
                                       WHERE c.WareHouseId <> b.WareHouseId
                                             AND
                                             c.AddressId = a.AddressId
                                     );
              DELETE FROM ZnodePortalAlternateWarehouse
             WHERE EXISTS ( SELECT TOP 1 1
                            FROM @DeleteWarehouseId AS b
                            WHERE b.WarehouseId = ZnodePortalAlternateWarehouse.WarehouseId
                          );

			 DELETE FROM ZnodePortalAlternateWarehouse
             WHERE EXISTS ( SELECT TOP 1 1
                            FROM ZnodePortalWarehouse
             WHERE EXISTS ( SELECT TOP 1 1
                            FROM @DeleteWarehouseId AS b
                            WHERE b.WarehouseId = ZnodePortalWarehouse.WarehouseId
                          )
						  AND ZnodePortalWarehouse.PortalWarehouseId = ZnodePortalAlternateWarehouse.PortalWarehouseId
                          );

			 DELETE FROM ZnodePortalWarehouse
             WHERE EXISTS ( SELECT TOP 1 1
                            FROM @DeleteWarehouseId AS b
                            WHERE b.WarehouseId = ZnodePortalWarehouse.WarehouseId
                          );
            
			 DELETE FROM ZnodeInventory WHERE EXISTS ( SELECT TOP 1 1
                            FROM @DeleteWarehouseId AS b
                            WHERE b.WarehouseId = ZnodeInventory.WarehouseId
                          );


			 DELETE FROM ZnodeWarehouseAddress
             WHERE EXISTS ( SELECT TOP 1 1
                            FROM @DeleteWarehouseId AS b
                            WHERE b.WarehouseId = ZnodeWarehouseAddress.WarehouseId
                          );
             
             DELETE FROM ZnodeWarehouse
             WHERE EXISTS ( SELECT TOP 1 1
                            FROM @DeleteWarehouseId AS b
                            WHERE b.WarehouseId = ZnodeWarehouse.WarehouseId
                          );
             DELETE FROM ZnodeAddress
             WHERE EXISTS ( SELECT TOP 1 1
                            FROM @DeleteAddressId AS b
                            WHERE b.AddressId = ZnodeAddress.AddressId
                          );
             SET @Status = 1;
             IF ( SELECT COUNT(1)
                  FROM @DeleteWarehouseId
                ) = ( SELECT COUNT(1)
                      FROM @Warehouse
                    )
                 BEGIN
                     SELECT 1 AS ID , CAST(1 AS BIT) AS Status;
                 END;
             ELSE
                 BEGIN
                     SELECT 0 AS ID , CAST(0 AS BIT) AS Status;
                 END;
             COMMIT TRAN;
         END TRY
         BEGIN CATCH
             DECLARE @Error_procedure VARCHAR(1000)= ERROR_PROCEDURE(), @ErrorMessage NVARCHAR(MAX)= ERROR_MESSAGE(), @ErrorLine VARCHAR(100)= ERROR_LINE(), @ErrorCall NVARCHAR(MAX)= 'EXEC RARIndia_DeleteCountry @WarehouseId = '+@WarehouseId+',@Status='+CAST(@Status AS VARCHAR(200));
             SET @Status = 0;
             SELECT 0 AS ID,
                    CAST(0 AS BIT) AS Status;
			 ROLLBACK TRAN DeleteAccount;
             EXEC Znode_InsertProcedureErrorLog
                  @ProcedureName = 'RARIndia_DeleteCountry',
                  @ErrorInProcedure = @Error_procedure,
                  @ErrorMessage = @ErrorMessage,
                  @ErrorLine = @ErrorLine,
                  @ErrorCall = @ErrorCall;
            
         END CATCH;
     END;
