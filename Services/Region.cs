using System;
using System.Collections.Generic;
using XinTuo.Accounts.Models;
using Orchard.Data;
using System.Linq;

namespace XinTuo.Accounts.Services
{
    public class Region : IRegion
    {
        private readonly IRepository<RegionRecord> _regionRecord;

        public Region(IRepository<RegionRecord> regionRecord)
        {
            _regionRecord = regionRecord;
        }

        public List<RegionRecord> test()
        {
            return _regionRecord.Table.Select(r=>new RegionRecord() { ProvinceId = r.ProvinceId, ProvinceName = r.ProvinceName }).ToList();
        }

        public List<RegionRecord> GetRegions(int? RegionId)
        {
            //id没有值则返回所有省份
            if (!RegionId.HasValue)
            {
                return (from region in _regionRecord.Table
                        where region.Level == 1
                        select new RegionRecord() { ProvinceId = region.ProvinceId, ProvinceName = region.ProvinceName }).ToList();
            }

            RegionRecord reg = _regionRecord.Fetch(r => r.RegionId == RegionId.Value).FirstOrDefault();

            
            //返回该省份包含的城市
            if (reg != null && reg.Level == 1)
            {
                return (from region in _regionRecord.Table
                        where region.ProvinceId == RegionId.Value && region.CityId != RegionId.Value
                        select new RegionRecord() { CityId = region.CityId, CityName = region.CityName }).ToList();
            }

            //返回该城市包含的县/城区
            if (reg != null && reg.Level == 2)
            {
                return (from region in _regionRecord.Table
                        where region.CityId == RegionId.Value && region.RegionId != RegionId.Value
                        select new RegionRecord() { RegionId = region.RegionId, CountyName = region.CountyName }).ToList();
            }

            return new List<RegionRecord>() { reg};
        }
    }
}