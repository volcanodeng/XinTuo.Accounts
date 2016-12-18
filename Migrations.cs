using System;
using System.Collections.Generic;
using System.Data;
using Orchard.ContentManagement.Drivers;
using Orchard.ContentManagement.MetaData;
using Orchard.ContentManagement.MetaData.Builders;
using Orchard.Core.Contents.Extensions;
using Orchard.Data.Migration;
using XinTuo.Accounts.Models;
using Orchard.Users.Models;
using Orchard.Roles.Models;
using Orchard.Roles.Services;
using Orchard.Core.Common.Models;

namespace XinTuo.Accounts {
    public class Migrations : DataMigrationImpl {

        private readonly IRoleService _role;

        public Migrations(IRoleService role)
        {
            _role = role;
        }

        public int Create()
        {
            //��������
            SchemaBuilder.CreateTable("RegionRecord",
                t => t.Column<int>("Id",c=>c.PrimaryKey().Identity())
                .Column<int>("RegionId")                                        //������
                .Column<int>("CityId")                                          //���б��
                .Column<int>("ProvinceId")                                      //ʡ�ݱ��
                .Column<string>("CountyName", c => c.WithLength(50))            //����
                .Column<string>("CityName", c => c.WithLength(50))              //����
                .Column<string>("ProvinceName", c => c.WithLength(50))          //���
                .Column<string>("Pinyin", c => c.WithLength(100))               //��������ƴ��
                .Column<int>("Level")                                           //���򼶱�1 ʡ��  2 ����   3 ��/��
                );

            //�û���˾
            SchemaBuilder.CreateTable("CompanyRecord",
                t => t.ContentPartRecord()                                          //��˾id
                .Column<string>("FullName", c => c.WithLength(100))                 //��˾ȫ��
                .Column<string>("ShortName", c => c.WithLength(50))                 //��˾���
                .Column<int>("RegionRecord_Id")                                     //��������
                .Column<string>("Address", c => c.WithLength(200))                  //��˾��ַ
                .Column<string>("Tel", c => c.WithLength(20))                       //��˾�绰
                .Column<string>("ContactName", c => c.WithLength(100))              //��ϵ������
                .Column<string>("ContactMobile", c => c.WithLength(30))             //��ϵ�˵绰
                .Column<string>("ContactEmail", c => c.WithLength(100))             //��ϵ���ʼ�
                .Column<int>("StartYear",c=>c.Nullable())                           //����ʱ�䣨��ȣ�
                .Column<int>("StartPeriod", c=>c.Nullable())                        //����ʱ�䣨���ڣ�
                .Column<string>("FiscalSystem", c=>c.WithLength(50))                //����ƶ�
                );
            //������˾���û�
            SchemaBuilder.CreateTable("CompanyUserRecord",
                t => t.Column<int>("Id", c => c.PrimaryKey().Identity())
                .Column<int>("CompanyRecord_Id")                                    //��˾id
                .Column<int>("UserPartRecord_Id")                                   //�û�id(����UserPart)
                );

            //������������
            SchemaBuilder.CreateTable("AuxiliaryTypeRecord",
                t=>t.Column<int>("Id",c=>c.PrimaryKey().Identity())
                .Column<string>("AuxType",c=>c.WithLength(50))                      //���������������͹�6��
                .Column<int>("CompanyRecord_Id")                                    //�Զ��帨����������빫˾����
                );

            //��������
            SchemaBuilder.CreateTable("AuxiliaryRecord",
                t => t.ContentPartRecord()                                          //id
                .Column<int>("AuxiliaryTypeRecord_Id")                              //������������
                .Column<string>("AuxCode", c => c.WithLength(50))                   //�������
                .Column<string>("AuxName", c => c.WithLength(50))                   //��������
                .Column<int>("AuxState", c => c.WithDefault(1))                     //״̬��1 ����  0 ����
                .Column<int>("CompanyRecord_Id")                                   //����������˾
                .Column<int>("Creator")                                             //������
                .Column<DateTime>("CreateTime", c => c.WithDefault(DateTime.Now))   //����ʱ��
                );

            //ƾ֤��
            SchemaBuilder.CreateTable("CertificateWordRecord",
                t=>t.ContentPartRecord()                                            //ƾ֤������
                .Column<string>("CertWord",c=>c.WithLength(50))                     //ƾ֤��
                .Column<string>("PrintTitle",c=>c.WithLength(50))                   //��ӡ����
                .Column<int>("SortIndex",c=>c.WithDefault(1))                       //��������
                .Column<bool>("IsDefault",c=>c.WithDefault(false))                  //�Ƿ�Ĭ��ƾ֤��
                .Column<int>("CompanyRecord_Id")                                   //������˾
                .Column<int>("Creator")                                             //������
                .Column<DateTime>("CreateTime", c => c.WithDefault(DateTime.Now))   //����ʱ��
                );

            //��Ŀ���
            SchemaBuilder.CreateTable("AccountCategoryRecord",
                t => t.Column<int>("Id", c => c.PrimaryKey().Identity())               //�������
                .Column<int>("ParentAcId", c => c.Nullable())                          //���id
                .Column<string>("CateName", c => c.WithLength(50))                     //�������
                );

            //��ƿ�Ŀ
            SchemaBuilder.CreateTable("AccountRecord",
                t=>t.Column<int>("Id",c=>c.PrimaryKey().Identity())                 //��Ŀ����
                .Column<string>("AccCode",c=>c.WithLength(50))                      //��Ŀ����
                .Column<string>("ParentCode",c=>c.WithLength(50))                   //������
                .Column<int>("AccountCategoryRecord_Id")                            //��Ŀ���ȡ�ڶ������
                .Column<string>("AccName",c=>c.WithLength(50))                      //��Ŀ����
                .Column<string>("Direction",c=>c.WithLength(10))                    //����
                .Column<int>("IsAuxiliary")                                         //�Ƿ������������㣨0 �����ã�1 ������
                .Column<string>("AuxTypeIds",c=>c.WithLength(100))                  //������������id�������ŷָ���
                .Column<int>("IsQuantity")                                          //�Ƿ�������������(0 �����ã�1 ����)
                .Column<string>("Unit",c=>c.WithLength(10))                         //��������ļ�����λ
                .Column<decimal>("InitialQuantity",c=>c.WithScale(2))               //�ڳ��������
                .Column<decimal>("InitialBalance",c=>c.WithScale(2))                //�ڳ����
                .Column<decimal>("YtdDebitQuantity", c=>c.WithScale(2))             //�����ۻ��跽����
                .Column<decimal>("YtdDebit",c=>c.WithScale(2))                      //�����ۻ��跽���
                .Column<decimal>("YtdCreditQuantity",c=>c.WithScale(2))             //�����ۻ���������
                .Column<decimal>("YtdCredit",c=>c.WithScale(2))                     //�����ۻ��������
                .Column<decimal>("YtdBeginBalanceQuantity",c=>c.WithScale(2))       //����������
                .Column<decimal>("YtdBeginBalance",c=>c.WithScale(2))               //������
                .Column<int>("AccState",c=>c.WithDefault(1))                        //��Ŀ״̬��1 ���� 0 ����
                .Column<int>("CompanyRecord_Id")                                    //��Ŀ������˾
                .Column<int>("Creator")
                .Column<DateTime>("CreateTime")
                .Column<int>("Updater")
                .Column<DateTime>("UpdateTime")
                );

            //ƾ֤ժҪ��(ͨ��Creator������ȡ����)
            SchemaBuilder.CreateTable("AbstractRecord",
                t=>t.ContentPartRecord()
                .Column<string>("Abstract",c=>c.WithLength(255))
                .Column<int>("Creator")
                .Column<DateTime>("CreateTime")
                );

            //ƾ֤
            SchemaBuilder.CreateTable("VoucherRecord",
                t=>t.Column<int>("Id",c=>c.PrimaryKey().Identity())             //ƾ֤����
                .Column<int>("CertificateWordRecord_Id")                        //ƾ֤��
                .Column<int>("CertWordSN",c=>c.WithDefault(1))                  //ƾ֤����ˮ��
                .Column<DateTime>("Date",c=>c.Nullable())                       //����
                .Column<int>("InvoiceCount",c=>c.WithDefault(0))                //���ӵ�������
                .Column<int>("State",c=>c.WithDefault(1))                       //״̬��1 ���� 2 ���� -1 ����
                .Column<int>("CompanyRecord_Id")                                //ƾ֤������˾
                .Column<int>("Creator")                                         //������
                .Column<DateTime>("CreateTime")                                 //����ʱ��
                .Column<int>("Review")                                          //�����
                .Column<DateTime>("ReviewTime")                                 //���ʱ��
                );

            //ƾ֤��ϸ
            SchemaBuilder.CreateTable("VoucherDetailRecord",
                t=>t.Column<int>("Id",c=>c.PrimaryKey().Identity())             //��ϸ����
                .Column<int>("VoucherRecord_Id")                                //ƾ֤�������
                .Column<string>("Abstract",c=>c.WithLength(255))                //ժҪ
                .Column<int>("AccountRecord_Id")                                //��Ŀ��������Ŀ��
                .Column<string>("AccountCode",c=>c.WithLength(100))             //��Ŀ����
                .Column<string>("AccountName",c=>c.WithLength(100))             //��Ŀ���ƣ���������չ��Ŀ���ƣ�
                .Column<decimal>("Quantity",c=>c.WithScale(2))                  //��������������ѡ��������
                .Column<decimal>("Price",c=>c.WithScale(2))                     //���ۣ���������ѡ��������
                .Column<decimal>("Debit",c=>c.WithScale(2))                     //�跽���
                .Column<decimal>("Credit",c=>c.WithScale(2))                    //�������
                );

            //ƾ֤ģ��
            SchemaBuilder.CreateTable("VoucherDetailTemplateRecord",
                t=>t.Column<int>("Id",c=>c.PrimaryKey().Identity())
                .Column<string>("Abstract", c => c.WithLength(255))                //ժҪ
                .Column<string>("AccountCode", c => c.WithLength(100))             //��Ŀ����
                .Column<string>("AccountName", c => c.WithLength(100))             //��Ŀ���ƣ���������չ��Ŀ���ƣ�
                .Column<decimal>("Quantity", c => c.WithScale(2))                  //��������������ѡ��������
                .Column<decimal>("Price", c => c.WithScale(2))                     //���ۣ���������ѡ��������
                .Column<decimal>("Debit", c => c.WithScale(2))                     //�跽���
                .Column<decimal>("Credit", c => c.WithScale(2))                    //�������
                );

            return 1;
        }

        public int UpdateFrom1()
        {
            //part��typeֻ�����ڻ������ݶ�����ȫ�ֹ���
            ContentDefinitionManager.AlterPartDefinition(typeof(CompanyPart).Name, part => part.Attachable(false).WithDescription("����ע�ṫ˾��Ϣ"));

            ContentDefinitionManager.AlterPartDefinition(typeof(AuxiliaryPart).Name, part => part.Attachable().WithDescription("�޸ĸ���������Ŀ"));

            ContentDefinitionManager.AlterPartDefinition(typeof(CertificateWordPart).Name, part => part.Attachable().WithDescription("�༭ƾ֤��"));

            //ҵ�����ݵĲ��ܶ���Ϊpart��type
            //ContentDefinitionManager.AlterPartDefinition(typeof(AccountPart).Name,part=>part.Attachable().WithDescription("��Ŀά��"));

            ContentDefinitionManager.AlterPartDefinition(typeof(AbstractPart).Name,part=>part.Attachable().WithDescription("��ʷժҪ��Ϣά��"));

            //ContentDefinitionManager.AlterPartDefinition(typeof(VoucherPart).Name,part=>part.Attachable(false).WithDescription("ƾ֤¼��"));

            //ContentDefinitionManager.AlterPartDefinition(typeof(VoucherDetailPart).Name,part=>part.Attachable().WithDescription("ƾ֤��ϸ��"));

            //ContentDefinitionManager.AlterPartDefinition(typeof(VoucherDetailTemplatePart).Name,part=>part.Attachable().WithDescription("ƾ֤ģ��ά��"));

            //��identityΪ����ʹ�õ��뵼��ģ��
            //��commonΪ�˼����������޸�ʱ���¼
            ContentDefinitionManager.AlterTypeDefinition("Company", type => type
                                                                    .WithPart(typeof(CompanyPart).Name)
                                                                    .WithPart(typeof(UserPart).Name)
                                                                    .WithPart(typeof(UserRolesPart).Name));

            ContentDefinitionManager.AlterTypeDefinition("Auxiliary", type => type
                                                                    .WithPart(typeof(AuxiliaryPart).Name)
                                                                    .WithPart(typeof(CommonPart).Name)
                                                                    .WithIdentity());

            ContentDefinitionManager.AlterTypeDefinition("CertificateWord", type => type
                                                                     .WithPart(typeof(CertificateWordPart).Name)
                                                                     .WithPart(typeof(CommonPart).Name)
                                                                     .WithIdentity());

            ContentDefinitionManager.AlterTypeDefinition("Abstract",type=>type
                                                                    .WithPart(typeof(AbstractPart).Name)
                                                                    .WithPart(typeof(CommonPart).Name)
                                                                    .WithIdentity());

            //ContentDefinitionManager.AlterTypeDefinition("Account", type => type
            //                                                         .WithPart(typeof(CertificateWordPart).Name)
            //                                                         .WithPart(typeof(AccountPart).Name)
            //                                                         .WithPart(typeof(AbstractPart).Name));

            //ContentDefinitionManager.AlterTypeDefinition("Voucher", type => type
            //                                                         .WithPart(typeof(VoucherPart).Name)
            //                                                         .WithPart(typeof(VoucherDetailPart).Name)
            //                                                         .WithPart(typeof(VoucherDetailTemplatePart).Name));

            return 2;
        }

        public int UpdateFrom2()
        {
            //��ɫģ����뿪��Formģ��
            _role.CreateRole("Accountant");

            _role.CreatePermissionForRole("Accountant", Permissions.CreateAccount.Name);
            _role.CreatePermissionForRole("Accountant", Permissions.CreateAuxiliary.Name);
            _role.CreatePermissionForRole("Accountant", Permissions.CreateAuxiliaryType.Name);

            return 3;
        }

        
    }
}