using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Orchard.Users.Models;

namespace XinTuo.Accounts.Models
{
    public class CompanyUserRecord
    {
        public virtual int Id { get; set; }

        public virtual CompanyRecord CompanyRecord {get;set;}

        public virtual UserPartRecord UserPartRecord { get; set; }
    }
}