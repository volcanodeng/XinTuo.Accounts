if (exists (select * from sys.objects where name = 'P_Account_Init'))
    drop proc P_Account_Init
go


create proc P_Account_Init(@companyId int, @creatorId int)
as
begin

	INSERT INTO [XinTuo_Accounts_AccountRecord]
           ([AccCode]
           ,[ParentCode]
           ,[AccountCategoryRecord_Id]
           ,[AccName]
           ,[Direction]
           ,[IsAuxiliary]
           ,[AuxTypeIds]
           ,[IsQuantity]
           ,[Unit]
           ,[InitialQuantity]
           ,[InitialBalance]
           ,[YtdDebitQuantity]
           ,[YtdDebit]
           ,[YtdCreditQuantity]
           ,[YtdCredit]
           ,[YtdBeginBalanceQuantity]
           ,[YtdBeginBalance]
           ,[AccState]
           ,[CompanyRecord_Id]
           ,[Creator]
           ,[CreateTime]
           ,[Updater]
           ,[UpdateTime]
           ,[AuxTypeNames])
		   SELECT [AccCode]
			  ,[ParentCode]
			  ,[AccountCategoryRecord_Id]
			  ,[AccName]
			  ,[Direction]
			  ,[IsAuxiliary]
			  ,[AuxTypeIds]
			  ,[IsQuantity]
			  ,[Unit]
			  ,[InitialQuantity]
			  ,[InitialBalance]
			  ,[YtdDebitQuantity]
			  ,[YtdDebit]
			  ,[YtdCreditQuantity]
			  ,[YtdCredit]
			  ,[YtdBeginBalanceQuantity]
			  ,[YtdBeginBalance]
			  ,[AccState]
			  ,@companyId
			  ,@creatorId
			  ,GETDATE()
			  ,@creatorId
			  ,GETDATE()
			  ,[AuxTypeNames]
		  FROM [XinTuo_Accounts_AccountRecord]
		  where [CompanyRecord_Id] is null and [CreateTime] is null;


		  select * from [XinTuo_Accounts_AccountRecord] where [CompanyRecord_Id] = @companyId;

end