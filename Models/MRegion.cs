using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Orchard.ContentManagement;

namespace XinTuo.Accounts.Models
{
    public class MRegion : ContentPart<RegionRecord>
    {
        public int RegionId
        {
            get
            {
                return Retrieve(r => r.RegionId);
            }
            set
            {
                Store(r => r.RegionId, value);
            }
        }

        public int CityId
        {
            get
            {
                return Retrieve(r => r.CityId);
            }
            set
            {
                Store(r =>r.CityId, value);
            }
        }
    }
}