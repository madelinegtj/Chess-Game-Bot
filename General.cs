using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Advance
{
    /// <summary>
    /// Represents General object, one of the Pieces.
    /// </summary>
    public class General : Piece
    {
        /// <summary>
        /// Defines General object constructor for instantiation.
        /// </summary>
        /// <param name="player">Current player; black or white.</param>
        /// <param name="initialSquare">General piece's initial square coordinates.</param>
        public General(Player player, Square initialSquare) : base(player, initialSquare){
        }

        /// <summary>
        /// Checks for General's legal movements.
        /// </summary>
        /// <param name="newSquare">New square coordinates.</param>
        /// <returns>Boolean true if General is allowed to move to the new square.</returns>
        public override bool CanMoveTo(Square newSquare)
        {
            bool success = Square.NeighbourSquares.Any(square => square.Equals(newSquare))
                          && !newSquare.IsThreatenedBy(Player.Opponent);
            return success;
        }

        /// <summary>
        /// Checks for General's legal attacks.
        /// </summary>
        /// <param name="newSquare">New square coordinates.</param>
        /// <returns>Boolean true if General is allowed to capture the Piece on the new square.</returns>
        public override bool CanAttack(Square newSquare)
        {
            //Similar to its move, General can capture on any of the 8 adjoining squares
            bool success = Square.NeighbourSquares.Any(square => square.Equals(newSquare))
                          && !newSquare.IsThreatenedBy(Player.Opponent);
            return success;
        }

        /// <summary>
        /// Gets General symbol/icon lettercase based on its color.
        /// </summary>
        /// <returns>General's symbol.</returns>
        public override char Icon
        {
            get
            {
                return Player.Colour == Colour.White ? 'G' : 'g';
            }
        }
    }
}