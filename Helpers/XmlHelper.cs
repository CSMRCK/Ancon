using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Xml;

namespace Ancon.Helpers
{
    public static class XmlHelper
    {
        static bool CheckPattern(string input)
        {
            string pattern = @"^\d{3}\.";
            bool isMatch = Regex.IsMatch(input, pattern);

            return isMatch;
        }

        public static string FindIdInXml(string pathToFile)
        {
            string ip = "";

            XmlDocument xmlDoc = new XmlDocument();
            xmlDoc.Load(pathToFile);

            XmlNodeList nodes = xmlDoc.GetElementsByTagName("node");

            foreach (XmlNode node in nodes)
            {
                if (CheckPattern(node.Attributes["text"].Value) && !node.Attributes["text"].Value.StartsWith("101.188.67.134") && node.Attributes["index"].Value == "0")
                {
                    ip = node.Attributes["text"].Value;
                }

            }

            return ip;
        }
    }
}
