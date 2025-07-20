using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Advance
{
    /// <summary>
    /// Represents Catapult object, one of the Pieces.
    /// </summary>
    public class Catapult : Piece
    {
        /// <summary>
        /// Defines Catapult object constructor for instantiation.
        /// </summary>
        /// <param name="player">Current player; black or white.</param>
        /// <param name="initialSquare">Catapult piece's initial square coordinates.</param>
        public Catapult(Player player, Square initialSquare) : base(player, initialSquare){
        }

        /// <summary>
        /// Checks for Catapult's legal movements.
        /// </summary>
        /// <param name="newSquare">New square coordinates.</param>
        /// <returns>Boolean true if Catapult is allowed to move to the new square.</returns>
        public override bool CanMoveTo(Square newSquare)
        {
            //Catapult can move to 4 cardinal direction but cannot capture them
            if (Square == null) throw new Exception("Cannot move piece that is off the board");
            if (newSquare == null)
                return false;

            return Square.AdjacentSquares.Any(square => square.Equals(newSquare));
        }

        /// <summary>
        /// Checks for Catapult's legal attacks.
        /// </summary>
        /// <param name="newSquare">New square coordinates.</param>
        /// <returns>Boolean true if Catapult is allowed to capture the Piece on the new square.</returns>
        public override bool CanAttack(Square newSquare)
        {
            if (Square == null) throw new Exception("Cannot attack with piece that is off the board");

            int dx = Square.Row - newSquare.Row;
            int dy = Square.Col - newSquare.Col;

            //Absolute value of dx and dy
            if (dx < 0) dx = -dx;
            if (dy < 0) dy = -dy;

            //Catapult is able to capture pieces that are either 3 squares away in a cardinal direction
            //or 2 squares away in two perpendicular cardinal directions
            return dx == 0 && dy == 3 || dx == 3 && dy == 0 || dx == 2 && dy == 2;
        }

        /// <summary>
        /// Attacks a target square with the Catapult.
        /// </summary>
        /// <param name="targetSquare">The square to attack.</param>
        /// <returns><c>true</c> if the attack was successful; otherwise, <c>false</c>.</returns>
        public override bool Attack(Square targetSquare)
        {
            if (!CanAttack(targetSquare)) return false;
            if (targetSquare.IsFree) return false;
            if (targetSquare == Square) return false;

            targetSquare.Occupant?.LeaveBoard();
            return true;
        }

        /// <summary>
        /// Gets Catapult symbol/icon lettercase based on its color.
        /// </summary>
        /// <returns>Catapult's symbol.</returns>
        public override char Icon
        {
            get
            {
                return Player.Colour == Colour.White ? 'C' : 'c';
            }
        }
    }
}