using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Orchard.Data;
using Orchard.ContentManagement.Handlers;
using Orchard.Core.Common.Models;
using XinTuo.Accounts.Models;

namespace XinTuo.Accounts.Handlers
{
    public class CerttificateWordHandler : ContentHandler
    {
        public CerttificateWordHandler(IRepository<CertificateWordRecord> repository)
        {
            Filters.Add(StorageFilter.For(repository));
            Filters.Add(new ActivatingFilter<CertificateWordPart>("CertificateWord"));
            Filters.Add(new ActivatingFilter<CommonPart>("CertificateWord"));
        }
    }
}