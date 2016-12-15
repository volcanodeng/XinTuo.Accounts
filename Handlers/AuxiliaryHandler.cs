﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Orchard.Data;
using Orchard.ContentManagement.Handlers;
using Orchard.Core.Common.Models;
using XinTuo.Accounts.Models;

namespace XinTuo.Accounts.Handlers
{
    public class AuxiliaryHandler : ContentHandler
    {
        public AuxiliaryHandler(IRepository<AuxiliaryRecord> repository)
        {
            Filters.Add(StorageFilter.For(repository));
            Filters.Add(new ActivatingFilter<AuxiliaryPart>("Auxiliary"));
            Filters.Add(new ActivatingFilter<CommonPart>("Auxiliary"));
        }
    }
}