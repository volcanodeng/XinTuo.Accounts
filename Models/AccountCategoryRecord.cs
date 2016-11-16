using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace XinTuo.Accounts.Models
{
    public class AccountCategoryRecord
    {
        public virtual int Id { get; set; }
        public virtual int? ParentAcId { get; set; }
        public virtual string CateName { get; set; }
    }
}