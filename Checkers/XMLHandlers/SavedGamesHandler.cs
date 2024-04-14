using System.Collections.ObjectModel;
using Checkers.Model;
using Checkers.Services;

namespace Checkers.XMLHandlers
{
    internal class SavedGamesHandler
    {
        public static bool SaveCurrentGame(ObservableCollection<ObservableCollection<Cell>> cells, GameLogic.Player currentPlayer, bool multipleJumps, string gameName, int whitePieceNumber, int blackPieceNumber)
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

            var whitePieceNumberAttribute = xmlDoc.CreateAttribute("whitePieceNumber");
            whitePieceNumberAttribute.Value = whitePieceNumber.ToString();
            gameNode.Attributes.Append(whitePieceNumberAttribute);

            var blackPieceNumberAttribute = xmlDoc.CreateAttribute("blackPieceNumber");
            blackPieceNumberAttribute.Value = blackPieceNumber.ToString();
            gameNode.Attributes.Append(blackPieceNumberAttribute);

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

        public static ObservableCollection<string> GetSavedGamesName()
        {
            var path = "../../../Databases/SavedGames.xml";
            var xmlDoc = new System.Xml.XmlDocument();
            xmlDoc.Load(path);
            var rootNode = xmlDoc.SelectSingleNode("SavedGames");
            if (rootNode == null) return null;
            var savedGames = new ObservableCollection<string>();
            foreach (System.Xml.XmlNode gameNode in rootNode.ChildNodes)
            {
                savedGames.Add(gameNode.Attributes["name"].Value);
            }
            return savedGames;
        }

        public static SGH_ReturnType LoadGame(string gameName)
        {
            var path = "../../../Databases/SavedGames.xml";
            var xmlDoc = new System.Xml.XmlDocument();
            xmlDoc.Load(path);
            var rootNode = xmlDoc.SelectSingleNode("SavedGames");
            if (rootNode == null) return null;
            foreach (System.Xml.XmlNode gameNode in rootNode.ChildNodes)
            {
                if (gameNode.Attributes["name"].Value != gameName) continue;
                var returnType = new SGH_ReturnType();
                var board = new ObservableCollection<ObservableCollection<Cell>>();
                var count = 0;
                for (var i = 0; i < 8; i++)
                {
                    var row = new ObservableCollection<Cell>();
                    for (var j = 0; j < 8; j++)
                    {
                        row.Add(count % 2 == 0
                            ? new Cell(j, i, "/Checkers;component/Image/BlackSpace_Empty.png",
                                "/Checkers;component/Image/BlackSpace_WhitePiece.png",
                                "/Checkers;component/Image/BlackSpace_BlackPiece.png")
                            : new Cell(j, i, "/Checkers;component/Image/WhiteSpace_Empty.png"));
                        row[j].CurrentImage = row[j].BackgroundEmptyPath;
                        row[j].CurrentState = State.Empty;
                        count++;
                       
                    }
                    board.Add(row);
                    count--;
                }
                foreach (System.Xml.XmlNode cellNode in gameNode.ChildNodes)
                {
                    var x = int.Parse(cellNode.Attributes["x"].Value);
                    var y = int.Parse(cellNode.Attributes["y"].Value);
                    var currentState = (State)Enum.Parse(typeof(State), cellNode.Attributes["currentState"].Value);
                    board[y][x].CurrentState = currentState;
                    switch (currentState)
                    {
                        case State.WhitePiece:
                            board[y][x].CurrentImage = board[y][x].WhitePiece;
                            break;
                        case State.BlackPiece:
                            board[y][x].CurrentImage = board[y][x].BlackPiece;
                            break;
                        case State.WhitePieceKing:
                            board[y][x].CurrentImage = board[y][x].WhitePieceKing;
                            break;
                        case State.BlackPieceKing:
                            board[y][x].CurrentImage = board[y][x].BlackPieceKing;
                            break;
                        default:
                            board[y][x].CurrentImage = board[y][x].BackgroundEmptyPath;
                            break;
                    }
                }
                returnType.Board = board;
                returnType.PlayerTurn = gameNode.Attributes["playerTurn"]!.Value;
                returnType.MultipleJumps = bool.Parse(gameNode.Attributes["multipleJumpsAllowed"]!.Value);
                returnType.WhitePiecesNumber = int.Parse(gameNode.Attributes["whitePieceNumber"]!.Value);
                returnType.BlackPiecesNumber = int.Parse(gameNode.Attributes["blackPieceNumber"]!.Value);
                return returnType;
            }
            return null;
        }
    }
}
