using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Orchard.ContentManagement;

namespace XinTuo.Accounts.Models
{
    public class AbstractPart : ContentPart<AbstractRecord>
    {
        public string Abstract
        {
            get
            {
                return Record.Abstract;
            }
            set
            {
                Record.Abstract = value;
            }
        }

        public int Creator
        {
            get
            {
                return Record.Creator;
            }
            set
            {
                Record.Creator = value;
            }
        }
        public DateTime CreateTime
        {
            get
            {
                return Record.CreateTime;
            }
            set
            {
                Record.CreateTime = value;
            }
        }
    }
}