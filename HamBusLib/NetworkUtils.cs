using System.Net;

namespace HamBusLib
{
    public class NetworkUtils
    {
        static public string getHostName()
        {
            string hostName = Dns.GetHostName(); // Retrive the Name of HOST  
            return hostName;
        }
        static public string getIpAddress()
        {
            string hostName = Dns.GetHostName();
            string ip = Dns.GetHostEntry(hostName).AddressList[0].ToString();
            return ip;
        }
    }
}
