using System;
using System.Collections.Generic;
using XinTuo.Accounts.Models;
using Orchard.Data;
using System.Linq;

namespace XinTuo.Accounts.Services
{
    public class Region : IRegion
    {
        private readonly IRepository<RegionRecord> _regionPartRecord;

        public Region(IRepository<RegionRecord> regionPartRecord)
        {
            _regionPartRecord = regionPartRecord;
        }

        public List<RegionRecord> GetRegions(int? RegionId)
        {
            //id没有值则返回所有省份
            if (!RegionId.HasValue)
            {
                return (from region in _regionPartRecord.Table
                        group region by new { region.ProvinceId, region.ProvinceName } into g
                        select new RegionRecord() { ProvinceId = g.Key.ProvinceId, ProvinceName = g.Key.ProvinceName }).ToList();
            }

            //值包含0000，说明是一个省份的id
            //返回该省份包含的城市
            if (RegionId.HasValue && RegionId.Value.ToString().EndsWith("0000"))
            {
                return (from region in _regionPartRecord.Table
                        where region.ProvinceId == RegionId.Value && region.CityId != RegionId.Value
                        group region by new { region.CityId, region.CityName } into g
                        select new RegionRecord() { CityId = g.Key.CityId, CityName = g.Key.CityName }).ToList();
            }

            //值以00结尾，说明是一个城市的id
            //返回该城市包含的县/城区
            if ( RegionId.HasValue && RegionId.Value.ToString().EndsWith("00"))
            {
                return (from region in _regionPartRecord.Table
                        where region.CityId == RegionId.Value && region.RegionId != RegionId.Value
                        group region by new { region.RegionId, region.CountyName } into g
                        select new RegionRecord() { RegionId = g.Key.RegionId, CountyName = g.Key.CountyName }).ToList();
            }

            return _regionPartRecord.Fetch(r => r.RegionId == RegionId.Value).ToList();
        }
    }
}