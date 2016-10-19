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
                t => t.Column<int>("AuId", column => column.PrimaryKey().Identity()) //主键
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

            //科目类别
            SchemaBuilder.CreateTable("AccountCategoryRecord",
                t=>t.Column<int>("AcId",c=>c.PrimaryKey().Identity())               //类别主键
                .Column<int>("ParentAcId",c=>c.Nullable())                          //类别父id
                .Column<string>("CateName",c=>c.WithLength(50))                     //类别名称
                );

            //会计科目
            SchemaBuilder.CreateTable("AccountRecord",
                t=>t.Column<int>("AId",c=>c.PrimaryKey().Identity())                //科目主键
                .Column<string>("AccCode",c=>c.WithLength(50))                      //科目编码
                .Column<string>("ParentCode",c=>c.WithLength(50))                   //父编码
                .Column<int>("AccountCategoryRecord_AcId")                          //科目类别（取第二级类别）
                .Column<string>("AccName",c=>c.WithLength(50))                      //科目名称
                .Column<string>("Direction",c=>c.WithLength(10))                    //余额方向
                .Column<string>("AuxIds",c=>c.WithLength(100))                      //辅助核算id串（逗号分隔）
                .Column<string>("AuxCodes",c=>c.WithLength(100))                    //辅助核算编号串（逗号分隔）
                .Column<string>("AuxNames",c=>c.WithLength(150))                    //辅助核算名称串（逗号分隔）
                .Column<string>("Unit",c=>c.WithLength(10))                         //数量核算的计量单位
                .Column<decimal>("InitialQuantity",c=>c.WithScale(2))               //期初余额数量
                .Column<decimal>("InitialBalance",c=>c.WithScale(2))                //期初余额
                .Column<decimal>("YtdDebitQuantity", c=>c.WithScale(2))             //本年累积借方数量
                .Column<decimal>("YtdDebit",c=>c.WithScale(2))                      //本年累积借方金额
                .Column<decimal>("YtdCreditQuantity",c=>c.WithScale(2))             //本年累积贷方数量
                .Column<decimal>("YtdCredit",c=>c.WithScale(2))                     //本年累积贷方金额
                .Column<decimal>("YtdBeginBalanceQuantity",c=>c.WithScale(2))       //年初余额数量
                .Column<decimal>("YtdBeginBalance",c=>c.WithScale(2))               //年初余额
                .Column<int>("AccState",c=>c.WithDefault(1))                        //科目状态：1 正常 0 禁用
                .Column<int>("CompanyRecord_CId")                                   //科目所属公司
                .Column<int>("Creator")
                .Column<DateTime>("CreateTime")
                .Column<int>("Updater")
                .Column<DateTime>("UpdateTime")
                );

            //凭证摘要库(通过Creator关联获取数据)
            SchemaBuilder.CreateTable("VouAbstractRecord",
                t=>t.Column<int>("AbId",c=>c.PrimaryKey().Identity())
                .Column<string>("Abstract",c=>c.WithLength(255))
                .Column<int>("Creator")
                .Column<DateTime>("CreateTime")
                );

            //凭证
            SchemaBuilder.CreateTable("VoucherRecord",
                t=>t.Column<int>("VId",c=>c.PrimaryKey().Identity())            //凭证主键
                .Column<int>("AccCertificateWordRecord_CwId")                   //凭证字
                .Column<int>("CertWordSN",c=>c.WithDefault(1))                  //凭证字流水号
                .Column<DateTime>("Date",c=>c.Nullable())                       //日期
                .Column<int>("InvoiceCount",c=>c.WithDefault(0))                //附加单据数量
                .Column<int>("State",c=>c.WithDefault(1))                       //状态：1 正常 2 已审 -1 作废
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