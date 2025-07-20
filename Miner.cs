using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Advance
{
    /// <summary>
    /// Represents Miner object, one of the Pieces.
    /// </summary>
    public class Miner : Piece
    {
        /// <summary>
        /// Defines Miner object constructor for instantiation.
        /// </summary>
        /// <param name="player">Current player; black or white.</param>
        /// <param name="initialSquare">Miner piece's initial square coordinates.</param>
        public Miner(Player player, Square initialSquare) : base(player, initialSquare){
        }

        /// <summary>
        /// Checks for Miner's legal movements.
        /// </summary>
        /// <param name="newSquare">New square coordinates.</param>
        /// <returns>Boolean true if Miner is allowed to move to the new square.</returns>
        public override bool CanMoveTo(Square newSquare)
        {
            if (Square == null) throw new Exception("Cannot Move with piece that is off the board");
            if (Square.Row == newSquare.Row && Square.Col == newSquare.Col) return false;

            bool success = true;

            if (Square.Row == newSquare.Row)
            {
                if (HasPiecesBetweenRowDir(Square, newSquare))
                    success = false;
            }
            else if (Square.Col == newSquare.Col)
            {
                if (HasPiecesBetweenColumnDir(Square, newSquare))
                    success = false;
            }
            else
            {
                // if newSquare is neither in the same row or column
                success = false;
            }

            return success;
        }

        /// <summary>
        /// Checks for Miner's legal attacks.
        /// </summary>
        /// <param name="newSquare">New square coordinates.</param>
        /// <returns>Boolean true if Miner is allowed to capture the Piece on the new square.</returns>
        public override bool CanAttack(Square newSquare)
        {
            /*
            //If Piece is a Wall, Miner can capture it
            if (CanMoveTo(newSquare) && (newSquare.Occupant is Wall))
            {
                newSquare.Remove();
                return true;
            }
            */

            //Similar to its move, Miner can attack in one of the 4 cardinal directions
            return CanMoveTo(newSquare);
        }

        /// <summary>
        /// Checks if there are any pieces between the source and destination squares in the same row.
        /// </summary>
        /// <param name="source">The source square.</param>
        /// <param name="des">The destination square.</param>
        /// <returns>True if there are pieces between the squares, false otherwise.</returns>
        private bool HasPiecesBetweenRowDir(Square source, Square des)
        {
            if (source == null || des == null) throw new ArgumentNullException("Source or Destination square cannot be null.");
            if (source.Row == des.Row)
            {
                int startCol = Math.Min(source.Col, des.Col);
                int endCol = Math.Max(source.Col, des.Col);

                // Check if each square between start and end row contains any pieces
                for (int col = startCol + 1; col < endCol; col++)
                {
                    Square squareBetween = source.Board.Get(source.Row, col);
                    if (squareBetween.IsOccupied)
                    {
                        return true;
                    }
                }
            }

            return false;
        }

        /// <summary>
        /// Checks if there are any pieces between the source and destination squares in the same column.
        /// </summary>
        /// <param name="source">The source square.</param>
        /// <param name="des">The destination square.</param>
        /// <returns>True if there are pieces between the squares, false otherwise.</returns>
        private bool HasPiecesBetweenColumnDir(Square source, Square des)
        {
            if (source == null || des == null) throw new ArgumentNullException("Source or Destination square cannot be null.");
            if (source.Col == des.Col)
            {
                int startRow = Math.Min(source.Row, des.Row);
                int endRow = Math.Max(source.Row, des.Row);

                // Check if each square between start and end column contains any pieces
                for (int row = startRow + 1; row < endRow; row++)
                {
                    Square squareBetween = source.Board.Get(row, source.Col);
                    if (squareBetween.IsOccupied)
                    {
                        return true;
                    }
                }
            }


            return false;
        }

        /// <summary>
        /// Gets Miner symbol/icon lettercase based on its color.
        /// </summary>
        /// <returns>Miner's symbol.</returns>
        public override char Icon
        {
            get
            {
                return Player.Colour == Colour.White ? 'M' : 'm';
            }
        }
    }
}