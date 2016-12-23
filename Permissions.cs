using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Orchard.Environment.Extensions.Models;
using Orchard.Security.Permissions;

namespace XinTuo.Accounts
{
    public class Permissions : IPermissionProvider
    {
        public static readonly Permission ModifyAccount = new Permission { Name= "ModifyAccount", Description="创建/修改新科目"};
        public static readonly Permission ModifyAuxiliary = new Permission { Name= "ModifyAccount", Description="创建/修改新的辅助核算内容"};
        public static readonly Permission ModifyAuxiliaryType = new Permission { Name = "ModifyAccount", Description = "创建/修改辅助核算类型" };

        public virtual Feature Feature { get; set; }

        public IEnumerable<Permission> GetPermissions()
        {
            return new[] {
                ModifyAccount,
                ModifyAuxiliary,
                ModifyAuxiliaryType
            };
        }

        public IEnumerable<PermissionStereotype> GetDefaultStereotypes()
        {
            return new[] {
                new PermissionStereotype {
                    Name="Accountant",
                    Permissions=new[] { ModifyAccount , ModifyAuxiliary,ModifyAuxiliaryType }
                }
            };
        }
    }
}