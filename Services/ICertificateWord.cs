﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using XinTuo.Accounts.Models;
using XinTuo.Accounts.ViewModels;
using Orchard;

namespace XinTuo.Accounts.Services
{
    public interface ICertificateWord : IDependency
    {

        List<VMCertWord> GetCertificateWordForCom();

        VMCertWord GetCertificateWordForCom(int cwId);

        VMCertWord GetCertificateWordForCom(string certWord);

        VMCertWord GetDefaultCertificateWordForCom();

        CertificateWordPart GetCertWordPartForCom(string certWord);

        CertificateWordPart SaveCertWord(VMCertWord cw);

        void DeleteCertWord(int cwId);

        void SetDefault(int cwId);
    }
}