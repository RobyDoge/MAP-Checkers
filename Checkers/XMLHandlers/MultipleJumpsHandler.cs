using System.Xml;

namespace Checkers.XMLHandlers
{
    internal static class MultipleJumpsHandler
    {
        public static bool GetMultipleJumps()
        {
            XmlDocument xmlDoc = new();
            xmlDoc.Load("../../../Databases/MultipleJumps.xml");
            XmlNode? rootNode = xmlDoc.SelectSingleNode("MultipleJumpsAllowed");
            if (rootNode == null) throw new Exception("MultipleJumpsAllowed node not found");
            return rootNode.InnerText == "true";
        }

        public static bool ChangeMultipleJumps(bool multipleJumps)
        {
            var change = multipleJumps ? "true" : "false";
            XmlDocument xmlDoc = new();
            xmlDoc.Load("../../../Databases/MultipleJumps.xml");
            XmlNode? rootNode = xmlDoc.SelectSingleNode("MultipleJumpsAllowed");
            if(rootNode == null) return false;
            rootNode.InnerText = change;
            xmlDoc.Save("../../../Databases/MultipleJumps.xml");
            return true;
        }
    }

}
