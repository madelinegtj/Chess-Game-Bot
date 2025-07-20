using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Advance
{
    /// <summary>
    /// Represents Builder object, one of the Pieces.
    /// </summary>
    public class Builder : Piece
    {
        /// <summary>
        /// Defines Builder object constructor for instantiation.
        /// </summary>
        /// <param name="player">Current player; black or white.</param>
        /// <param name="initialSquare">Builder piece's initial square coordinates.</param>
        public Builder(Player player, Square initialSquare) : base(player, initialSquare){
        }

        /// <summary>
        /// Gets a value indicating whether the builder requires an enemy to attack.
        /// Builders can attack walls directly without requiring an enemy piece.
        /// </summary>
        public override bool RequiresEnemyToAttack { get { return false; } }

        /// <summary>
        /// Checks for Builder's legal movements.
        /// </summary>
        /// <param name="newSquare">New square coordinates.</param>
        /// <returns>Boolean true if Builder is allowed to move to the new square.</returns>
        public override bool CanMoveTo(Square newSquare)
        {
            if (Square == null) throw new Exception("Cannot Move with piece that is off the board");

            int dx = newSquare.Row - Square.Row;
            int dy = newSquare.Col - Square.Col;

            //Absolute value of dx and dy
            if (dx < 0) dx = -dx;
            if (dy < 0) dy = -dy;

            if (dx == 0 && dy == 0) return false;

            //Builder can move on any of the 8 adjoining squares
            return (dy <= 1 && dx <= 1);
        }

        /// <summary>
        /// Checks for Builder's legal attacks.
        /// </summary>
        /// <param name="newSquare">New square coordinates.</param>
        /// <returns>Boolean true if Builder is allowed to capture the Piece on the new square.</returns>
        public override bool CanAttack(Square newSquare)
        {
            if (Square == null) throw new Exception("Cannot attack with piece that is off the board");
            if (Square == newSquare) return false;

            int dx = newSquare.Row - Square.Row;
            int dy = newSquare.Col - Square.Col;

            if (dx < 0) dx = -dx;
            if (dy < 0) dy = -dy;

            if (dx == 0 && dy == 0) return false;

            return dx <= 1 && dy <= 1;
        }

        /// <summary>
        /// Attacks a square, removing any piece occupying it.
        /// </summary>
        /// <param name="targetSquare">The target square to attack.</param>
        /// <returns><c>true</c> if the attack was successful; otherwise, <c>false</c>.</returns>
        public override bool Attack(Square targetSquare)
        {

            if (!CanAttack(targetSquare)) return false;
            if (targetSquare.IsFree) return false;
            if (targetSquare == Square) return false;

            targetSquare.Occupant?.LeaveBoard();
            LeaveBoard();
            EnterBoard(targetSquare);
            return true;
        }
        
        /// <summary>
        /// Checks if Builder can build wall on a square.
        /// </summary>
        /// <param name="newSquare">New square coordinates.</param>
        /// <returns>Boolean true if Piece is allowed to build wall on that square.</returns>
        /*
        public virtual bool CanBuildWall(Square newSquare)
        {
            //Similar to its move, Builder can build wall on any of the 8 adjoining squares
            return CanMoveTo(newSquare);
        }
        

        /// <summary>
        /// Builds a wall, only applicable for Builder.
        /// </summary>
        /// <param name="newSquare">New square coordinates.</param>
        /// <returns>Boolean true if Wall is successfully built.</returns>
        public virtual bool BuildWall(Square newSquare, Wall wall)
        {
            if (!CanBuildWall(newSquare)) return false;
            if (newSquare.IsOccupied) return false;
            if (newSquare == Square) return false;

            newSquare.Place(wall);
            return true;
        }
        */

        

        /// <summary>
        /// Gets Builder symbol/icon lettercase based on its color.
        /// </summary>
        /// <returns>Builder's symbol.</returns>
        public override char Icon{
            get
            {
                return Player.Colour == Colour.White ? 'B' : 'b';
            }
        }
    }
}