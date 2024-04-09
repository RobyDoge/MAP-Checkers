using System.Xml;

namespace Checkers.XMLHandlers
{
    internal class StatisticsHandler
    {
        public static bool UpdateStatisticsWhite(int piecesNumber)
        {
            XmlDocument xmlDoc = new();
            xmlDoc.Load("../../../Databases/Statistics.xml");
            XmlNode? rootNode = xmlDoc.SelectSingleNode("Statistics");
            if (rootNode == null) return false;

            XmlNode? whiteNode = rootNode.SelectSingleNode("White");
            if (whiteNode == null) return false;

            XmlAttribute? winsNumberAttr = whiteNode.Attributes["winsNumber"];
            if (winsNumberAttr == null) return false;
            winsNumberAttr.Value.ToString();
            int winsNumber = int.Parse(winsNumberAttr.Value);
            winsNumber++;
            winsNumberAttr.Value = winsNumber.ToString();

            XmlAttribute? maxPieceWinAttr = whiteNode.Attributes["maxPieceWin"];
            if (maxPieceWinAttr == null) return false;
            maxPieceWinAttr.Value.ToString();
            int maxPieceWin = int.Parse(maxPieceWinAttr.Value);
            maxPieceWinAttr.Value = piecesNumber > maxPieceWin ? piecesNumber.ToString() : maxPieceWin.ToString();
            

            xmlDoc.Save("../../../Databases/Statistics.xml");
            return true;
        }

        public static bool UpdateStatisticsBlack(int piecesNumber)
        {
            XmlDocument xmlDoc = new();
            xmlDoc.Load("../../../Databases/Statistics.xml");
            XmlNode? rootNode = xmlDoc.SelectSingleNode("Statistics");
            if (rootNode == null) return false;

            XmlNode? blackNode = rootNode.SelectSingleNode("Black");
            if (blackNode == null) return false;

            XmlAttribute? winsNumberAttr = blackNode.Attributes["winsNumber"];
            if (winsNumberAttr == null) return false;
            winsNumberAttr.Value.ToString();
            int winsNumber = int.Parse(winsNumberAttr.Value);
            winsNumber++;
            winsNumberAttr.Value = winsNumber.ToString();

            XmlAttribute? maxPieceWinAttr = blackNode.Attributes["maxPieceWin"];
            if (maxPieceWinAttr == null) return false;
            maxPieceWinAttr.Value.ToString();
            int maxPieceWin = int.Parse(maxPieceWinAttr.Value);
            maxPieceWinAttr.Value = piecesNumber > maxPieceWin ? piecesNumber.ToString() : maxPieceWin.ToString();


            xmlDoc.Save("../../../Databases/Statistics.xml");
            return true;
        }
    }
}
