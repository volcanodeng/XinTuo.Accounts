using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Net;
using System.Net.Http;
using System.Text;
using Newtonsoft.Json;
using Orchard.Logging;

namespace XinTuo.Accounts.Controllers
{
    public class ApiResponse : HttpResponseMessage
    {

        public ApiResponse(string msg) : base()
        {
            Logger = NullLogger.Instance;

            Message m = new Message();
            m.message = msg;
            this.Content = new StringContent(JsonConvert.SerializeObject(m), Encoding.UTF8, "application/json");
        }

        public ApiResponse(string msg, HttpStatusCode code) : base(code)
        {
            Logger = NullLogger.Instance;

            Message m = new Message();
            m.message = msg;
            this.Content = new StringContent(JsonConvert.SerializeObject(m), Encoding.UTF8, "application/json");
        }

        public ILogger Logger { get; set; }
    }

    public class Message
    {
        public string message
        {
            get;set;
        }
    }
}