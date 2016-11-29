﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Orchard.Environment.Extensions.Models;
using Orchard.Security.Permissions;

namespace XinTuo.Accounts
{
    public class Permissions : IPermissionProvider
    {
        public static readonly Permission CreateAccount = new Permission { Name= "CreateAccount" ,Description="创建新科目"};
        public static readonly Permission CreateAuxiliary = new Permission { Name= "CreateAuxiliary" ,Description="创建新的辅助核算内容"};

        public virtual Feature Feature { get; set; }

        public IEnumerable<Permission> GetPermissions()
        {
            return new[] {
                CreateAccount,
                CreateAuxiliary
            };
        }

        public IEnumerable<PermissionStereotype> GetDefaultStereotypes()
        {
            return new[] {
                new PermissionStereotype {
                    Name = "Administrator",
                    Permissions = GetPermissions()
                },
                new PermissionStereotype {
                    Name="Accountant",
                    Permissions=new[] { CreateAccount , CreateAuxiliary }
                }
            };
        }
    }
}