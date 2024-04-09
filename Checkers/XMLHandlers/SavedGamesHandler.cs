using System.Collections.ObjectModel;
using Checkers.Model;
using Checkers.Services;

namespace Checkers.XMLHandlers
{
    internal class SavedGamesHandler
    {
        public static bool SaveCurrentGame(ObservableCollection<ObservableCollection<Cell>> cells, GameLogic.Player currentPlayer, bool multipleJumps, string gameName)
        {
            string path = "../../../Databases/SavedGames.xml";
            var xmlDoc = new System.Xml.XmlDocument();
            xmlDoc.Load(path);
            var rootNode = xmlDoc.SelectSingleNode("SavedGames");
            if (rootNode == null) return false;
            var gameNode = xmlDoc.CreateElement("Game");


            // Create attributes for the game node
            var nameAttribute = xmlDoc.CreateAttribute("name");
            nameAttribute.Value = gameName;
            gameNode.Attributes.Append(nameAttribute);

            var playerTurnAttribute = xmlDoc.CreateAttribute("playerTurn");
            playerTurnAttribute.Value = currentPlayer.ToString();
            gameNode.Attributes.Append(playerTurnAttribute);

            var multipleJumpsAllowedAttribute = xmlDoc.CreateAttribute("multipleJumpsAllowed");
            multipleJumpsAllowedAttribute.Value = multipleJumps.ToString();
            gameNode.Attributes.Append(multipleJumpsAllowedAttribute);

            for (int i = 0; i < 8; i++)
            {
                for (int j = 0; j < 8; j++)
                {
                    if (cells[i][j].CurrentState == State.Empty) continue;

                    var cell = cells[i][j];
                    var cellNode = xmlDoc.CreateElement("Cell");

                    var xCellAttribute = xmlDoc.CreateAttribute("y");
                    xCellAttribute.Value = i.ToString();
                    cellNode.Attributes.Append(xCellAttribute);

                    var yCellAttribute = xmlDoc.CreateAttribute("x");
                    yCellAttribute.Value = j.ToString();
                    cellNode.Attributes.Append(yCellAttribute);

                    var currentStateCellAttribute = xmlDoc.CreateAttribute("currentState");
                    currentStateCellAttribute.Value = cell.CurrentState.ToString();
                    cellNode.Attributes.Append(currentStateCellAttribute);

                    gameNode.AppendChild(cellNode);
                }
            }            

            // Append game node to root node
            rootNode.AppendChild(gameNode);

            // Save the modified XML document
            xmlDoc.Save(path);

            return true;
        }
    }
}
