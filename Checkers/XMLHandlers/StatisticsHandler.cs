using System.Collections.ObjectModel;
using System.Xml;

namespace Checkers.XMLHandlers;

internal static class StatisticsHandler
{
    public static bool UpdateStatisticsWhite(int piecesNumber)
    {
        XmlDocument xmlDoc = new();
        xmlDoc.Load("../../../Databases/Statistics.xml");
        var rootNode = xmlDoc.SelectSingleNode("Statistics");

        var whiteNode = rootNode?.SelectSingleNode("White");
        if (whiteNode == null) return false;

        var winsNumberAttr = whiteNode.Attributes["winsNumber"];
        if (winsNumberAttr == null) return false;
        winsNumberAttr.Value.ToString();
        var winsNumber = int.Parse(winsNumberAttr.Value);
        winsNumber++;
        winsNumberAttr.Value = winsNumber.ToString();

        var maxPieceWinAttr = whiteNode.Attributes["maxPieceWin"];
        if (maxPieceWinAttr == null) return false;
        maxPieceWinAttr.Value.ToString();
        var maxPieceWin = int.Parse(maxPieceWinAttr.Value);
        maxPieceWinAttr.Value = piecesNumber > maxPieceWin ? piecesNumber.ToString() : maxPieceWin.ToString();


        xmlDoc.Save("../../../Databases/Statistics.xml");
        return true;
    }

    public static bool UpdateStatisticsBlack(int piecesNumber)
    {
        XmlDocument xmlDoc = new();
        xmlDoc.Load("../../../Databases/Statistics.xml");
        var rootNode = xmlDoc.SelectSingleNode("Statistics");
        if (rootNode == null) return false;

        var blackNode = rootNode.SelectSingleNode("Black");
        if (blackNode == null) return false;

        var winsNumberAttr = blackNode.Attributes["winsNumber"];
        if (winsNumberAttr == null) return false;
        winsNumberAttr.Value.ToString();
        var winsNumber = int.Parse(winsNumberAttr.Value);
        winsNumber++;
        winsNumberAttr.Value = winsNumber.ToString();

        var maxPieceWinAttr = blackNode.Attributes["maxPieceWin"];
        if (maxPieceWinAttr == null) return false;
        maxPieceWinAttr.Value.ToString();
        var maxPieceWin = int.Parse(maxPieceWinAttr.Value);
        maxPieceWinAttr.Value = piecesNumber > maxPieceWin ? piecesNumber.ToString() : maxPieceWin.ToString();


        xmlDoc.Save("../../../Databases/Statistics.xml");
        return true;
    }

    internal static ObservableCollection<int> GetStatistics()
    {
        ObservableCollection<int> statistics = new();

        XmlDocument xmlDoc = new();
        xmlDoc.Load("../../../Databases/Statistics.xml");

        var rootNode = xmlDoc.SelectSingleNode("Statistics");
        if (rootNode == null) return statistics;

        var whiteNode = rootNode.SelectSingleNode("White");
        if (whiteNode == null) return statistics;

        var winsNumberAttr = whiteNode.Attributes["winsNumber"];
        if (winsNumberAttr == null) return statistics;
        statistics.Add(int.Parse(winsNumberAttr.Value));

        var maxPieceWinAttr = whiteNode.Attributes["maxPieceWin"];
        if (maxPieceWinAttr == null) return statistics;
        statistics.Add(int.Parse(maxPieceWinAttr.Value));


        var blackNode = rootNode.SelectSingleNode("Black");
        if (blackNode == null) return statistics;

        winsNumberAttr = blackNode.Attributes["winsNumber"];
        if (winsNumberAttr == null) return statistics;
        statistics.Add(int.Parse(winsNumberAttr.Value));

        maxPieceWinAttr = blackNode.Attributes["maxPieceWin"];
        if(maxPieceWinAttr == null) return statistics;
        statistics.Add(int.Parse(maxPieceWinAttr.Value));

        return statistics;
    }
}