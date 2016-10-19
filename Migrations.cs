using System;
using System.Collections.Generic;
using System.Data;
using Orchard.ContentManagement.Drivers;
using Orchard.ContentManagement.MetaData;
using Orchard.ContentManagement.MetaData.Builders;
using Orchard.Core.Contents.Extensions;
using Orchard.Data.Migration;

namespace XinTuo.Accounts {
    public class Migrations : DataMigrationImpl {

        public int Create()
        {
            //��������
            SchemaBuilder.CreateTable("RegionRecord",
                t => t.Column<int>("RegionId",column=>column.PrimaryKey())      //������
                .Column<int>("CityId")                                          //���б��
                .Column<int>("ProvinceId")                                      //ʡ�ݱ��
                .Column<string>("CountyName", c => c.WithLength(50))           //����
                .Column<string>("CityName", c => c.WithLength(50))             //����
                .Column<string>("ProvinceName", c => c.WithLength(50))         //���
                .Column<string>("Pinyin", c => c.WithLength(100))               //��������ƴ��
                );

            //�û���˾
            SchemaBuilder.CreateTable("CompanyRecord",
                t => t.Column<int>("CId", column => column.PrimaryKey().Identity()) //��˾id
                .Column<string>("FullName", c => c.WithLength(100))                 //��˾ȫ��
                .Column<string>("ShortName", c => c.WithLength(50))                 //��˾���
                .Column<int>("RegionRecord_RegionId")                               //��������
                .Column<string>("Address", c => c.WithLength(200))                  //��˾��ַ
                .Column<string>("Tel", c => c.WithLength(20))                       //��˾�绰
                .Column<string>("ContactName", c => c.WithLength(100))              //��ϵ������
                .Column<string>("ContactMobile", c => c.WithLength(30))             //��ϵ�˵绰
                .Column<string>("ContactEmail", c => c.WithLength(100))             //��ϵ���ʼ�
                );

            //������������
            SchemaBuilder.CreateTable("AccAuxiliaryTypeRecord",
                t=>t.Column<int>("AtId",column=>column.PrimaryKey().Identity())
                .Column<string>("AuxType",c=>c.WithLength(50))                      //�����������͹�8��
                );

            //��������
            SchemaBuilder.CreateTable("AccAuxiliaryRecord",
                t => t.Column<int>("AuId", column => column.PrimaryKey().Identity()) //����
                .Column<int>("AccAuxiliaryTypeRecord_AtId")                         //������������
                .Column<string>("AuxCode", c => c.WithLength(50))                   //�������
                .Column<string>("AuxName", c => c.WithLength(50))                   //��������
                .Column<int>("AuxState", c => c.WithDefault(1))                     //״̬��1 ����  0 ����
                .Column<int>("CompanyRecord_CId")                                   //����������˾
                .Column<int>("Creator")                                             //������
                .Column<DateTime>("CreateTime", c => c.WithDefault(DateTime.Now))   //����ʱ��
                );

            //ƾ֤��
            SchemaBuilder.CreateTable("AccCertificateWordRecord",
                t=>t.Column<int>("CwId",c=>c.PrimaryKey().Identity())               //ƾ֤������
                .Column<string>("CertWord",c=>c.WithLength(50))                     //ƾ֤��
                .Column<string>("PrintTitle",c=>c.WithLength(50))                   //��ӡ����
                .Column<int>("SortIndex",c=>c.WithDefault(1))                       //��������
                .Column<bool>("IsDefault",c=>c.WithDefault(false))                  //�Ƿ�Ĭ��ƾ֤��
                .Column<int>("CompanyRecord_CId")                                   //������˾
                .Column<int>("Creator")                                             //������
                .Column<DateTime>("CreateTime", c => c.WithDefault(DateTime.Now))   //����ʱ��
                );

            //��Ŀ���
            SchemaBuilder.CreateTable("AccountCategoryRecord",
                t=>t.Column<int>("AcId",c=>c.PrimaryKey().Identity())               //�������
                .Column<int>("ParentAcId",c=>c.Nullable())                          //���id
                .Column<string>("CateName",c=>c.WithLength(50))                     //�������
                );

            //��ƿ�Ŀ
            SchemaBuilder.CreateTable("AccountRecord",
                t=>t.Column<int>("AId",c=>c.PrimaryKey().Identity())                //��Ŀ����
                .Column<string>("AccCode",c=>c.WithLength(50))                      //��Ŀ����
                .Column<string>("ParentCode",c=>c.WithLength(50))                   //������
                .Column<int>("AccountCategoryRecord_AcId")                          //��Ŀ���ȡ�ڶ������
                .Column<string>("AccName",c=>c.WithLength(50))                      //��Ŀ����
                .Column<string>("Direction",c=>c.WithLength(10))                    //����
                .Column<string>("AuxIds",c=>c.WithLength(100))                      //��������id�������ŷָ���
                .Column<string>("AuxCodes",c=>c.WithLength(100))                    //���������Ŵ������ŷָ���
                .Column<string>("AuxNames",c=>c.WithLength(150))                    //�����������ƴ������ŷָ���
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
                .Column<int>("CompanyRecord_CId")                                   //��Ŀ������˾
                .Column<int>("Creator")
                .Column<DateTime>("CreateTime")
                .Column<int>("Updater")
                .Column<DateTime>("UpdateTime")
                );

            //ƾ֤ժҪ��(ͨ��Creator������ȡ����)
            SchemaBuilder.CreateTable("VouAbstractRecord",
                t=>t.Column<int>("AbId",c=>c.PrimaryKey().Identity())
                .Column<string>("Abstract",c=>c.WithLength(255))
                .Column<int>("Creator")
                .Column<DateTime>("CreateTime")
                );

            //ƾ֤
            SchemaBuilder.CreateTable("VoucherRecord",
                t=>t.Column<int>("VId",c=>c.PrimaryKey().Identity())            //ƾ֤����
                .Column<int>("AccCertificateWordRecord_CwId")                   //ƾ֤��
                .Column<int>("CertWordSN",c=>c.WithDefault(1))                  //ƾ֤����ˮ��
                .Column<DateTime>("Date",c=>c.Nullable())                       //����
                .Column<int>("InvoiceCount",c=>c.WithDefault(0))                //���ӵ�������
                .Column<int>("State",c=>c.WithDefault(1))                       //״̬��1 ���� 2 ���� -1 ����
                .Column<int>("CompanyRecord_CId")
                .Column<int>("Creator")
                .Column<DateTime>("CreateTime")
                .Column<int>("Review")
                .Column<DateTime>("ReviewTime")
                );

            SchemaBuilder.CreateTable("VoucherDetailRecord",
                t=>t.Column<int>("VdId",c=>c.PrimaryKey().Identity())
                .Column<int>("VoucherRecord_VId")
                .Column<string>("Abstract",c=>c.WithLength(255))
                .Column<int>("AccountRecord_AId")
                .Column<string>("AccountCode",c=>c.WithLength(100))
                .Column<string>("AccountName",c=>c.WithLength(100))
                .Column<decimal>("Quantity",c=>c.WithScale(2))
                .Column<decimal>("Price",c=>c.WithScale(2))
                .Column<decimal>("Debit",c=>c.WithScale(2))
                .Column<decimal>("Credit",c=>c.WithScale(2))
                );

            return 1;
        }
    }
}