using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net;
using System.Collections.Specialized;

namespace checkin
{
    class Program
    {
        static void Main(string[] args)
        {
            using (var wb = new WebClient())
            {
                var url = "<Insert checkin URL here>" + "UserName=" + Environment.UserName + "&HostName=" + Environment.MachineName;
                var data = new NameValueCollection();
                var response = wb.DownloadString(url);
            }

        }
}
}
