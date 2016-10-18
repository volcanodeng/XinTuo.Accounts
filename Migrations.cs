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
                t => t.Column<int>("AId", column => column.PrimaryKey().Identity()) //����
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


            return 1;
        }
    }
}