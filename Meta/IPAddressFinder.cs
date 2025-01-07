using System.Net;

namespace ClinicalXPDataConnections.Meta
{
    public class IPAddressFinder
    {
        private readonly HttpContext _context;

        public IPAddressFinder(HttpContext context)
        {
            _context = context;
        }
        public List<string> GetIPAddresses()
        {
            List<string> ips = new List<string>();
            string hostName = Dns.GetHostName(); // Retrive the Name of HOST
            Console.WriteLine(hostName);
            // Get the IP
            foreach (var item in Dns.GetHostByName(hostName).AddressList)
            {
                ips.Add(item.ToString() + "-" + Dns.GetHostByAddress(item.ToString()).HostName);
            }

            return ips;
        }

        public string GetIPAddress()
        {
            string myIP = "";

            if (_context != null)
            {
                myIP = _context.Connection.RemoteIpAddress.ToString();
            }


            return myIP;
        }
    }
}
