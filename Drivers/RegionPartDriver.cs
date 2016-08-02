using System.Runtime.CompilerServices;
using Orchard.ContentManagement;
using Orchard.ContentManagement.Drivers;
using Orchard.Data;
using XinTuo.Accounts.Models;

namespace XinTuo.Accounts.Drivers
{
    public class RegionPartDriver : ContentPartDriver<RegionPart>
    {
        private readonly IRepository<RegionPartRecord> _regionPart;

        protected override string Prefix
        {
            get
            {
                return "Region";
            }
        }

        public RegionPartDriver(IRepository<RegionPartRecord> regionPart)
        {
            _regionPart = regionPart;
        }

        protected override DriverResult Display(RegionPart part, string displayType, dynamic shapeHelper)
        {
            return ContentShape("Parts_Region",()=>shapeHelper.Parts_Region(
                
                ));
        }

    }
}