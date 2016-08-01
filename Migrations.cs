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

        public int Create() {
            SchemaBuilder.CreateTable("RegionPartRecord", 
                t => t.ContentPartRecord()
                .Column<int>("CityId")
                .Column<int>("ProvinceId")
                .Column<string>("CountyName",c=>c.WithLength(255))
                .Column<string>("CityName",c=>c.WithLength(255))
                .Column<string>("ProvinceName",c=>c.WithLength(255))
                .Column<string>("Pinyin",c=>c.WithLength(255))
                );

            SchemaBuilder.CreateTable("CompanyPartRecord",
                t => t.ContentPartRecord()
                .Column<string>("FullName",c=>c.WithLength(100))
                .Column<string>("ShortName",c=>c.WithLength(50))
                .Column<int>("RegionPartRecord_Id")
                .Column<string>("Address",c=>c.WithLength(200))
                .Column<string>("Tel",c=>c.WithLength(20))
                .Column<string>("ContactName",c=>c.WithLength(100))
                .Column<string>("ContactMobile",c=>c.WithLength(30))
                .Column<string>("ContactEmail",c=>c.WithLength(100))
                );

            ContentDefinitionManager.AlterPartDefinition("RegionPart",
                part => part.Attachable());

            return 1;
        }
    }
}