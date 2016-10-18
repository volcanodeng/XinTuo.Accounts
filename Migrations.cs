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
            //行政区域
            SchemaBuilder.CreateTable("RegionRecord",
                t => t.Column<int>("RegionId",column=>column.PrimaryKey())      //区域编号
                .Column<int>("CityId")                                          //城市编号
                .Column<int>("ProvinceId")                                      //省份编号
                .Column<string>("CountyName", c => c.WithLength(50))           //区域
                .Column<string>("CityName", c => c.WithLength(50))             //城市
                .Column<string>("ProvinceName", c => c.WithLength(50))         //身份
                .Column<string>("Pinyin", c => c.WithLength(100))               //区域名称拼音
                );

            //用户公司
            SchemaBuilder.CreateTable("CompanyRecord",
                t => t.Column<int>("CId", column => column.PrimaryKey().Identity()) //公司id
                .Column<string>("FullName", c => c.WithLength(100))                 //公司全称
                .Column<string>("ShortName", c => c.WithLength(50))                 //公司简称
                .Column<int>("RegionRecord_RegionId")                               //行政区码
                .Column<string>("Address", c => c.WithLength(200))                  //公司地址
                .Column<string>("Tel", c => c.WithLength(20))                       //公司电话
                .Column<string>("ContactName", c => c.WithLength(100))              //联系人名称
                .Column<string>("ContactMobile", c => c.WithLength(30))             //联系人电话
                .Column<string>("ContactEmail", c => c.WithLength(100))             //联系人邮件
                );

            //辅助核算类型
            SchemaBuilder.CreateTable("AccAuxiliaryTypeRecord",
                t=>t.Column<int>("AtId",column=>column.PrimaryKey().Identity())
                .Column<string>("AuxType",c=>c.WithLength(50))                      //辅助核算类型共8种
                );

            //辅助核算
            SchemaBuilder.CreateTable("AccAuxiliaryRecord",
                t => t.Column<int>("AId", column => column.PrimaryKey().Identity()) //主键
                .Column<int>("AccAuxiliaryTypeRecord_AtId")                         //辅助核算类型
                .Column<string>("AuxCode", c => c.WithLength(50))                   //核算编码
                .Column<string>("AuxName", c => c.WithLength(50))                   //核算名称
                .Column<int>("AuxState", c => c.WithDefault(1))                     //状态：1 正常  0 禁用
                .Column<int>("CompanyRecord_CId")                                   //核算所属公司
                .Column<int>("Creator")                                             //创建人
                .Column<DateTime>("CreateTime", c => c.WithDefault(DateTime.Now))   //创建时间
                );

            //凭证字
            SchemaBuilder.CreateTable("AccCertificateWordRecord",
                t=>t.Column<int>("CwId",c=>c.PrimaryKey().Identity())               //凭证字主键
                .Column<string>("CertWord",c=>c.WithLength(50))                     //凭证字
                .Column<string>("PrintTitle",c=>c.WithLength(50))                   //打印标题
                .Column<int>("SortIndex",c=>c.WithDefault(1))                       //排序索引
                .Column<bool>("IsDefault",c=>c.WithDefault(false))                  //是否默认凭证字
                .Column<int>("CompanyRecord_CId")                                   //所属公司
                .Column<int>("Creator")                                             //创建人
                .Column<DateTime>("CreateTime", c => c.WithDefault(DateTime.Now))   //创建时间
                );


            return 1;
        }
    }
}