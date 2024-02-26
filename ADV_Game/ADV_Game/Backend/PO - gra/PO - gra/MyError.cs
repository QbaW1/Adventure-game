using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PO___gra
{
    public class MyError : Exception
    {
        public string ExtraInfo { get; }
        public MyError(string message, string extrainfo) : base(message) 
        {
            ExtraInfo = extrainfo;
        }
       
    }
}
