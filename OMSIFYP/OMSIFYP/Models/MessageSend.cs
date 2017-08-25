using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace OMSIFYP.Models
{
    public class MessageSend
    {
        public int ID { get; set; }
        public string email { get; set; }
        public string Sender { get; set; }
        public string subject { get; set; }
        public string Message { get; set; }
    }
}