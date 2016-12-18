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
            //行政区域
            SchemaBuilder.CreateTable("RegionRecord",
                t => t.Column<int>("Id",c=>c.PrimaryKey().Identity())
                .Column<int>("RegionId")                                        //区域编号
                .Column<int>("CityId")                                          //城市编号
                .Column<int>("ProvinceId")                                      //省份编号
                .Column<string>("CountyName", c => c.WithLength(50))            //区域
                .Column<string>("CityName", c => c.WithLength(50))              //城市
                .Column<string>("ProvinceName", c => c.WithLength(50))          //身份
                .Column<string>("Pinyin", c => c.WithLength(100))               //区域名称拼音
                .Column<int>("Level")                                           //区域级别：1 省份  2 城市   3 县/区
                );

            //用户公司
            SchemaBuilder.CreateTable("CompanyRecord",
                t => t.ContentPartRecord()                                          //公司id
                .Column<string>("FullName", c => c.WithLength(100))                 //公司全称
                .Column<string>("ShortName", c => c.WithLength(50))                 //公司简称
                .Column<int>("RegionRecord_Id")                                     //行政区码
                .Column<string>("Address", c => c.WithLength(200))                  //公司地址
                .Column<string>("Tel", c => c.WithLength(20))                       //公司电话
                .Column<string>("ContactName", c => c.WithLength(100))              //联系人名称
                .Column<string>("ContactMobile", c => c.WithLength(30))             //联系人电话
                .Column<string>("ContactEmail", c => c.WithLength(100))             //联系人邮件
                .Column<int>("StartYear",c=>c.Nullable())                           //启动时间（年度）
                .Column<int>("StartPeriod", c=>c.Nullable())                        //启动时间（账期）
                .Column<string>("FiscalSystem", c=>c.WithLength(50))                //会计制度
                );
            //所属公司的用户
            SchemaBuilder.CreateTable("CompanyUserRecord",
                t => t.Column<int>("Id", c => c.PrimaryKey().Identity())
                .Column<int>("CompanyRecord_Id")                                    //公司id
                .Column<int>("UserPartRecord_Id")                                   //用户id(关联UserPart)
                );

            //辅助核算类型
            SchemaBuilder.CreateTable("AuxiliaryTypeRecord",
                t=>t.Column<int>("Id",c=>c.PrimaryKey().Identity())
                .Column<string>("AuxType",c=>c.WithLength(50))                      //基础辅助核算类型共6种
                .Column<int>("CompanyRecord_Id")                                    //自定义辅助核算必须与公司关联
                );

            //辅助核算
            SchemaBuilder.CreateTable("AuxiliaryRecord",
                t => t.ContentPartRecord()                                          //id
                .Column<int>("AuxiliaryTypeRecord_Id")                              //辅助核算类型
                .Column<string>("AuxCode", c => c.WithLength(50))                   //核算编码
                .Column<string>("AuxName", c => c.WithLength(50))                   //核算名称
                .Column<int>("AuxState", c => c.WithDefault(1))                     //状态：1 正常  0 禁用
                .Column<int>("CompanyRecord_Id")                                   //核算所属公司
                .Column<int>("Creator")                                             //创建人
                .Column<DateTime>("CreateTime", c => c.WithDefault(DateTime.Now))   //创建时间
                );

            //凭证字
            SchemaBuilder.CreateTable("CertificateWordRecord",
                t=>t.ContentPartRecord()                                            //凭证字主键
                .Column<string>("CertWord",c=>c.WithLength(50))                     //凭证字
                .Column<string>("PrintTitle",c=>c.WithLength(50))                   //打印标题
                .Column<int>("SortIndex",c=>c.WithDefault(1))                       //排序索引
                .Column<bool>("IsDefault",c=>c.WithDefault(false))                  //是否默认凭证字
                .Column<int>("CompanyRecord_Id")                                   //所属公司
                .Column<int>("Creator")                                             //创建人
                .Column<DateTime>("CreateTime", c => c.WithDefault(DateTime.Now))   //创建时间
                );

            //科目类别
            SchemaBuilder.CreateTable("AccountCategoryRecord",
                t => t.Column<int>("Id", c => c.PrimaryKey().Identity())               //类别主键
                .Column<int>("ParentAcId", c => c.Nullable())                          //类别父id
                .Column<string>("CateName", c => c.WithLength(50))                     //类别名称
                );

            //会计科目
            SchemaBuilder.CreateTable("AccountRecord",
                t=>t.Column<int>("Id",c=>c.PrimaryKey().Identity())                 //科目主键
                .Column<string>("AccCode",c=>c.WithLength(50))                      //科目编码
                .Column<string>("ParentCode",c=>c.WithLength(50))                   //父编码
                .Column<int>("AccountCategoryRecord_Id")                            //科目类别（取第二级类别）
                .Column<string>("AccName",c=>c.WithLength(50))                      //科目名称
                .Column<string>("Direction",c=>c.WithLength(10))                    //余额方向
                .Column<int>("IsAuxiliary")                                         //是否启动辅助核算（0 不启用，1 启动）
                .Column<string>("AuxTypeIds",c=>c.WithLength(100))                  //辅助核算类型id串（逗号分隔）
                .Column<int>("IsQuantity")                                          //是否启用数量核算(0 不启用，1 启用)
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
                .Column<int>("CompanyRecord_Id")                                    //科目所属公司
                .Column<int>("Creator")
                .Column<DateTime>("CreateTime")
                .Column<int>("Updater")
                .Column<DateTime>("UpdateTime")
                );

            //凭证摘要库(通过Creator关联获取数据)
            SchemaBuilder.CreateTable("AbstractRecord",
                t=>t.ContentPartRecord()
                .Column<string>("Abstract",c=>c.WithLength(255))
                .Column<int>("Creator")
                .Column<DateTime>("CreateTime")
                );

            //凭证
            SchemaBuilder.CreateTable("VoucherRecord",
                t=>t.Column<int>("Id",c=>c.PrimaryKey().Identity())             //凭证主键
                .Column<int>("CertificateWordRecord_Id")                        //凭证字
                .Column<int>("CertWordSN",c=>c.WithDefault(1))                  //凭证字流水号
                .Column<DateTime>("Date",c=>c.Nullable())                       //日期
                .Column<int>("InvoiceCount",c=>c.WithDefault(0))                //附加单据数量
                .Column<int>("State",c=>c.WithDefault(1))                       //状态：1 正常 2 已审 -1 作废
                .Column<int>("CompanyRecord_Id")                                //凭证所属公司
                .Column<int>("Creator")                                         //经办人
                .Column<DateTime>("CreateTime")                                 //创建时间
                .Column<int>("Review")                                          //审核人
                .Column<DateTime>("ReviewTime")                                 //审核时间
                );

            //凭证明细
            SchemaBuilder.CreateTable("VoucherDetailRecord",
                t=>t.Column<int>("Id",c=>c.PrimaryKey().Identity())             //明细主键
                .Column<int>("VoucherRecord_Id")                                //凭证主表关联
                .Column<string>("Abstract",c=>c.WithLength(255))                //摘要
                .Column<int>("AccountRecord_Id")                                //科目（关联科目表）
                .Column<string>("AccountCode",c=>c.WithLength(100))             //科目代码
                .Column<string>("AccountName",c=>c.WithLength(100))             //科目名称（可生成扩展科目名称）
                .Column<decimal>("Quantity",c=>c.WithScale(2))                  //数量（辅助核算选择数量）
                .Column<decimal>("Price",c=>c.WithScale(2))                     //单价（辅助核算选择数量）
                .Column<decimal>("Debit",c=>c.WithScale(2))                     //借方金额
                .Column<decimal>("Credit",c=>c.WithScale(2))                    //贷方金额
                );

            //凭证模板
            SchemaBuilder.CreateTable("VoucherDetailTemplateRecord",
                t=>t.Column<int>("Id",c=>c.PrimaryKey().Identity())
                .Column<string>("Abstract", c => c.WithLength(255))                //摘要
                .Column<string>("AccountCode", c => c.WithLength(100))             //科目代码
                .Column<string>("AccountName", c => c.WithLength(100))             //科目名称（可生成扩展科目名称）
                .Column<decimal>("Quantity", c => c.WithScale(2))                  //数量（辅助核算选择数量）
                .Column<decimal>("Price", c => c.WithScale(2))                     //单价（辅助核算选择数量）
                .Column<decimal>("Debit", c => c.WithScale(2))                     //借方金额
                .Column<decimal>("Credit", c => c.WithScale(2))                    //贷方金额
                );

            return 1;
        }

        public int UpdateFrom1()
        {
            //part和type只能用于基础数据定义表或全局功能
            ContentDefinitionManager.AlterPartDefinition(typeof(CompanyPart).Name, part => part.Attachable(false).WithDescription("用于注册公司信息"));

            ContentDefinitionManager.AlterPartDefinition(typeof(AuxiliaryPart).Name, part => part.Attachable().WithDescription("修改辅助核算项目"));

            ContentDefinitionManager.AlterPartDefinition(typeof(CertificateWordPart).Name, part => part.Attachable().WithDescription("编辑凭证字"));

            //业务数据的不能定义为part或type
            //ContentDefinitionManager.AlterPartDefinition(typeof(AccountPart).Name,part=>part.Attachable().WithDescription("科目维护"));

            ContentDefinitionManager.AlterPartDefinition(typeof(AbstractPart).Name,part=>part.Attachable().WithDescription("历史摘要信息维护"));

            //ContentDefinitionManager.AlterPartDefinition(typeof(VoucherPart).Name,part=>part.Attachable(false).WithDescription("凭证录入"));

            //ContentDefinitionManager.AlterPartDefinition(typeof(VoucherDetailPart).Name,part=>part.Attachable().WithDescription("凭证明细表"));

            //ContentDefinitionManager.AlterPartDefinition(typeof(VoucherDetailTemplatePart).Name,part=>part.Attachable().WithDescription("凭证模板维护"));

            //加identity为了能使用导入导出模块
            //加common为了加入新增和修改时间记录
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
            //角色模块必须开启Form模块
            _role.CreateRole("Accountant");

            _role.CreatePermissionForRole("Accountant", Permissions.CreateAccount.Name);
            _role.CreatePermissionForRole("Accountant", Permissions.CreateAuxiliary.Name);
            _role.CreatePermissionForRole("Accountant", Permissions.CreateAuxiliaryType.Name);

            return 3;
        }

        
    }
}