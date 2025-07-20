using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Advance 
{
    /// <summary>
    /// Represents Sentinel object, one of the Pieces.
    /// </summary>
    public class Sentinel : Piece 
    {
        /// <summary>
        /// Defines Sentinel object constructor for instantiation.
        /// </summary>
        /// <param name="player">Current player; black or white.</param>
        /// <param name="initialSquare">Sentinel piece's initial square coordinates.</param>
        public Sentinel(Player player, Square initialSquare) : base(player, initialSquare) {
        }

        /// <summary>
        /// Checks for Sentinel's legal movements.
        /// </summary>
        /// <param name="newSquare">New square coordinates.</param>
        /// <returns>Boolean true if Sentinel is allowed to move to the new square.</returns>
        public override bool CanMoveTo(Square newSquare) {
            if (newSquare == null)
                return false;
            if (newSquare == Square) return false;

            int dy = newSquare.Row - Square.Row;
            int dx = newSquare.Col - Square.Col;

            //Absolute value of dx and dy
            if (dx < 0) dx = -dx;
            if (dy < 0) dy = -dy;

            //Sentinels can move two squares in one cardinal direction and one square in a perpendicular direction
            //Can jump over any intervening pieces and walls
            return dx != 0 && dy != 0 && dx + dy == 3;
        }

        /// <summary>
        /// Checks for Sentinel's legal attacks.
        /// </summary>
        /// <param name="newSquare">New square coordinates.</param>
        /// <returns>Boolean true if Sentinel is allowed to capture the Piece on the new square.</returns>
        public override bool CanAttack(Square newSquare) 
        {
            //Similar to its move, Sentinels can capture two squares in one cardinal diretion
            return CanMoveTo(newSquare);
        }

        /// <summary>
        /// Enumerates the squares that are protected by this Sentinel piece.
        /// </summary>
        private IEnumerable<Square> ProtectedSquares
        {
            get
            {
                return Square.AdjacentSquares;
            }
        }

        /// <summary>
        /// Determines if a target square is protected by this Sentinel piece.
        /// </summary>
        /// <param name="targetSquare">The target square to check.</param>
        /// <returns>True if the target square is protected, false otherwise.</returns>
        public bool IsProtected(Square targetSquare)
        {
            //Sentinel can protect friendly pieces on 4 adjoining squares
            if (targetSquare == null) throw new ArgumentNullException("Target square cannot be null");
            return ProtectedSquares.Any(square => square.Equals(targetSquare));
        }

        /// <summary>
        /// Gets Sentinel symbol/icon lettercase based on its color.
        /// </summary>
        /// <returns>Sentinel's symbol.</returns>
        public override char Icon {
            get {
                return Player.Colour == Colour.White ? 'S' : 's';
            }
        }
    }
}