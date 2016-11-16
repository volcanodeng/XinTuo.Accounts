﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using XinTuo.Accounts.Models;
using XinTuo.Accounts.ViewModels;

namespace XinTuo.Accounts.Services
{
    public interface IAccount
    {
        AccountPart GetAccount(int id);

        AccountPart SaveAccount(VMAccount account);
    }
}