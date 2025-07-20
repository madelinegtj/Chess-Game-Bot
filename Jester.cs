using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Advance
{   
    /// <summary>
    /// Represents Jester object, one of the Pieces.
    /// </summary>
    internal class Jester : Piece
    {
        /// <summary>
        /// Defines Jester object constructor for instantiation.
        /// </summary>
        /// <param name="player">Current player; black or white.</param>
        /// <param name="initialSquare">Jester piece's initial square coordinates.</param>
        public Jester(Player player, Square? initialSquare) : base(player, initialSquare){
        }

        /// <summary>
        /// Checks for Jester's legal movements.
        /// </summary>
        /// <param name="newSquare">New square coordinates.</param>
        /// <returns>Boolean true if Jester is allowed to move to the new square.</returns>
        public override bool CanMoveTo(Square newSquare)
        {
            if (Square == null) throw new Exception("Cannot Move with piece that is off the board");

            int dx = newSquare.Row - Square.Row;
            int dy = newSquare.Col - Square.Col;

            //Absolute value of dx and dy
            if (dx < 0) dx = -dx;
            if (dy < 0) dy = -dy;

            if (dx == 0 && dy == 0) return false;

            if (dx <= 1 && dy <= 1 && newSquare.IsFree) return true;

            // Check if the square is occupied by a friendly piece (but not another Jester)
            if (dx <= 1 && dy <= 1 && newSquare.IsOccupied
                && newSquare.Occupant.Player == Player
                && !(newSquare.Occupant is Jester))
            {
                return true;
            }

            return false;
        }

        /// <summary>
        /// Moves the jester to a new square, swapping places with a friendly piece if one is present.
        /// </summary>
        /// <param name="newSquare">The square to be moved to.</param>
        /// <returns>True if the move is successful, false otherwise.</returns>
        public override bool MoveTo(Square newSquare)
        {
            if (!CanMoveTo(newSquare)) return false;

            Square currentSquare = Square;
            Piece? friendlyPiece = newSquare.Occupant;
            if (newSquare.IsOccupied
                && newSquare.Occupant.Player == Player
                && !(newSquare.Occupant is Jester))
            {
                friendlyPiece.LeaveBoard();
                LeaveBoard(); // Jester leaves board
                friendlyPiece.EnterBoard(currentSquare);
                EnterBoard(newSquare);
            }
            else // Move to empty square
            {
                LeaveBoard();
                EnterBoard(newSquare);

            }
            return true;
        }

        /// <summary>
        /// Checks for Jester's legal attacks.
        /// </summary>
        /// <param name="newSquare">New square coordinates.</param>
        /// <returns>Boolean true if Jester is allowed to capture the Piece on the new square.</returns>
        public override bool CanAttack(Square newSquare)
        {
            //Jester is the only piece that cannot capture other pieces
            return false;
        }

        /*
        public override bool CanAttack(Square newSquare)
        {
            if (Square == null) throw new Exception("Cannot Attack with piece that is off the board");

            int dx = newSquare.Row - Square.Row;
            int dy = newSquare.Col - Square.Col;

            if (dx < 0) dx = -dx;
            if (dy < 0) dy = -dy;

            if (dx == 0 && dy == 0) return false;

            if (dx <= 1 && dy <= 1
                && !(newSquare.Occupant is General))
            {
                return true;
            }

            return false;
        }
        */

        /// <summary>
        /// Attacks a target square, causing the target piece to defect.
        /// </summary>
        /// <param name="targetSquare">The square to be attacked.</param>
        /// <returns>True if the attack is successful, false otherwise.</returns>
        public override bool Attack(Square targetSquare)
        {
            if (!CanAttack(targetSquare)) return false;

            Piece? targetPiece = targetSquare.Occupant;

            // Defect the enemy piece
            targetPiece?.Defect();
            return true;
        }



        /// <summary>
        /// Gets Jester symbol/icon lettercase based on its color.
        /// </summary>
        /// <returns>Jester's symbol.</returns>
        public override char Icon
        {
            get
            {
                return Player.Colour == Colour.White ? 'J' : 'j';
            }
        }
    }
}
