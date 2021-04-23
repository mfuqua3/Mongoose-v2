using System;
using System.Net;
using System.Net.Sockets;

namespace Mongoose.Common.Utility
{
    public static class IpHelpers
    {
        public static string GetLocal()
        {
            var host = Dns.GetHostEntry(Dns.GetHostName());
            foreach (var ip in host.AddressList)
            {
                if (ip.AddressFamily == AddressFamily.InterNetwork)
                {
                    return ip.ToString();
                }
            }
            throw new Exception("No network adapters with an IPv4 address in the system!");
        }
    }
}