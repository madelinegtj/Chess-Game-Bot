using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Advance {

    /// <summary>
    /// Represents the Army of pieces.
    /// </summary>
    public class Army 
    {
        /// <summary>
        /// Defines an Enum of type Piece
        /// </summary>
        /// <returns>Pieces in the game as a whole.</returns>
        public IEnumerable<Piece> Pieces {
            get {
                return pieces;
            }
        }

        //Initialize a new list for all the pieces
        private List<Piece> pieces = new List<Piece>();

        /// <summary>
        /// Adds a piece to the army.
        /// </summary>
        /// <param name="piece">The piece to add.</param>
        public void AddPiece(Piece defected)
        {
            pieces.Add(defected);
        }

        /// <summary>
        /// Removes a piece from the army.
        /// </summary>
        /// <param name="piece">The piece to remove.</param>
        public void RemovePiece(Piece defected)
        {
            pieces.Remove(defected);
        }

        /// <summary>
        /// Defines Army object constructor for instantiation.
        /// </summary>
        /// <param name="player">Player white or black.</param>
        /// <param name="board">Current game board.</param>
        public Army(Player player, Board board) {
            Player = player;
            Board = board;

            //Black starts from top rows, White starts from bottom
            int baseRow = player.Colour == Colour.Black ? 0 : Board.Size - 1;
            //lack moves downwards; White moves upwards.
            int direction = player.Direction;

            /*
            try
            {
                //Initialize zombie pieces on the board
                for (int col = 1; col < Board.Size-1; col++) {
                    Square initialSquare = board.Get(baseRow + direction, col);
                    Piece newPiece = new Zombie(player, initialSquare);
                    pieces.Add(newPiece);
                }

                //Initialize all the pieces on the board
                pieces.Add(new Builder(player, board.Get(baseRow + direction, 0)));
                pieces.Add(new Builder(player, board.Get(baseRow + direction, 8)));
                pieces.Add(new Miner(player, board.Get(baseRow, 0)));
                pieces.Add(new Jester(player, board.Get(baseRow, 1)));
                pieces.Add(new Catapult(player, board.Get(baseRow, 2)));
                pieces.Add(new Sentinel(player, board.Get(baseRow, 3)));
                pieces.Add(new General(player, board.Get(baseRow, 4)));
                pieces.Add(new Sentinel(player, board.Get(baseRow, 5)));
                pieces.Add(new Dragon(player, board.Get(baseRow, 6)));
                pieces.Add(new Jester(player, board.Get(baseRow, 7)));
                pieces.Add(new Miner(player, board.Get(baseRow, 8)));
            }
            catch(Exception e)
            {
                Console.WriteLine(e.ToString());
            }
            */

            
        }

        /// <summary>
        /// Gets Player object.
        /// </summary>
        public Player Player { get; }
        /// <summary>
        /// Gets Board object.
        /// </summary>
        public Board Board { get; }

        /// <summary>
        /// Removes all pieces from the Board.
        /// </summary>
        public void RemoveAllPieces() {
            foreach (var currentPiece in pieces) {
                if (currentPiece.OnBoard) currentPiece.LeaveBoard();
            }

            pieces.Clear();
        }

        /// <summary>
        /// Recruits Piece based on the icon shown on the Board.
        /// </summary>
        /// <param name="icon">Player icon/symbol.</param>
        /// <param name="initialSquare">Piece initial position.</param>
        public void Recruit(char icon, Square? initialSquare) {
            if (initialSquare == null)
                throw new ArgumentException("initialSquare must not be null");

            var symbol = Char.ToLower(icon);
            Piece? newPiece = null;

            //Checks for all the symbol and assign it to its appropriate piece.
            switch (symbol) {
                case 'z':
                    newPiece = new Zombie(Player, initialSquare);
                    break;
                case 'b':
                    newPiece = new Builder(Player, initialSquare);
                    break;
                case 'm':
                    newPiece = new Miner(Player, initialSquare);
                    break;
                case 'j':
                    newPiece = new Jester(Player, initialSquare);
                    break;
                case 's':
                    newPiece = new Sentinel(Player, initialSquare);
                    break;
                case 'c':
                    newPiece = new Catapult(Player, initialSquare);
                    break;
                case 'd':
                    newPiece = new Dragon(Player, initialSquare);
                    break;
                case 'g':
                    newPiece = new General(Player, initialSquare);
                    break;
                case '#':
                    newPiece = new Wall(null, initialSquare);
                    break;
                default:
                    throw new ArgumentException("Unrecognised icon");
            }

            pieces.Add(newPiece);
        }
    }
}