﻿using System;
using Orchard.ContentManagement.Records;

namespace XinTuo.Accounts.Models
{
    public class RegionRecord 
    {
        public virtual int Id { get; set; }
        public virtual int RegionId { get; set; }
        public virtual int CityId { get; set; }
        public virtual int ProvinceId { get; set; }
        public virtual string CountyName { get; set; }
        public virtual string CityName { get; set; }
        public virtual string ProvinceName { get; set; }
        public virtual string Pinyin { get; set; }
        public virtual int Level { get; set; }
    }
}