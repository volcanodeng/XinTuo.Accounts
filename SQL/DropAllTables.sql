drop table [dbo].[XinTuo_Accounts_AuxiliaryRecord]
drop table [dbo].[XinTuo_Accounts_AuxiliaryTypeRecord]
drop table [dbo].[XinTuo_Accounts_CertificateWordRecord]
drop table [dbo].[XinTuo_Accounts_AccountCategoryRecord]
drop table [dbo].[XinTuo_Accounts_AccountRecord]
drop table [dbo].[XinTuo_Accounts_CompanyRecord]
drop table [dbo].[XinTuo_Accounts_CompanyUserRecord]
drop table [dbo].[XinTuo_Accounts_RegionRecord]
drop table [dbo].[XinTuo_Accounts_AbstractRecord]
drop table [dbo].[XinTuo_Accounts_VoucherDetailRecord]
drop table [dbo].[XinTuo_Accounts_VoucherDetailTemplateRecord]
drop table [dbo].[XinTuo_Accounts_VoucherRecord]

delete from dbo.Orchard_Framework_DataMigrationRecord where [DataMigrationClass]='XinTuo.Accounts.Migrations'