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
        /// Checks for Builder's legal movements.
        /// </summary>
        /// <param name="newSquare">New square coordinates.</param>
        /// <returns>Boolean true if Builder is allowed to move to the new square.</returns>
        public override bool CanMoveTo(Square newSquare)
        {
            if (newSquare.Occupant is Wall)
                return false;

            int dy = newSquare.Row - Square.Row;
            int dx = newSquare.Col - Square.Col;

            //Absolute value of dx and dy
            if (dx < 0) dx = -dx;
            if (dy < 0) dy = -dy;

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
            //Similar to its move, Builder can capture on any of the 8 adjoining squares
            return CanMoveTo(newSquare);
        }
        
        /// <summary>
        /// Checks if Builder can build wall on a square.
        /// </summary>
        /// <param name="newSquare">New square coordinates.</param>
        /// <returns>Boolean true if Piece is allowed to build wall on that square.</returns>
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