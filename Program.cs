using System;
using System.Linq;
using System.Xml;
using System.IO;
using System.Net;


namespace Autoconfig
{
    class Program
    {
        static void Main(string[] args)
        {

            Console.WriteLine("Enter email:");
            
            string url = @"https://autoconfig.thunderbird.net/v1.1/" + GetDomain(Console.ReadLine());
            string html = string.Empty;
            string serverpath = "//outgoingServer[@type= " + '\u0022' + "smtp" + '\u0022' + "]/hostname";
            string portpath = "//outgoingServer[@type= " + '\u0022' + "smtp" + '\u0022' + "]/port";
            string socketrpath = "//outgoingServer[@type= " + '\u0022' + "smtp" + '\u0022' + "]/socketType";

            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
            request.AutomaticDecompression = DecompressionMethods.GZip;

            using (HttpWebResponse response = (HttpWebResponse)request.GetResponse())
            using (Stream stream = response.GetResponseStream())
            using (StreamReader reader = new StreamReader(stream))
            {
                html = reader.ReadToEnd();
                XmlDocument xmlDoc = new XmlDocument();
                xmlDoc.LoadXml(html);
                XmlNode node_server = xmlDoc.SelectSingleNode(serverpath);
                XmlNode node_port = xmlDoc.SelectSingleNode(portpath);
                XmlNode node_socket = xmlDoc.SelectSingleNode(socketrpath);
                Console.WriteLine(node_server.ChildNodes[0].InnerText);
                Console.WriteLine(node_port.ChildNodes[0].InnerText);
                Console.WriteLine(node_socket.ChildNodes[0].InnerText);
                Console.ReadKey();
            }

            
        }
        private static string GetDomain(string email)
        {
            return email.Split('@').LastOrDefault();
        }


    }
}
