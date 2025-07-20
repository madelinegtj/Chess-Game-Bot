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
            if (newSquare.Occupant != null)
                return false;

            int dy = newSquare.Row - Square.Row;
            int dx = newSquare.Col - Square.Col;

            //Absolute value of dx and dy
            if (dx < 0) dx = -dx;
            if (dy < 0) dy = -dy;

            //Catapult can only move 1 square at a time, and only in the 4 cardinal directions.
            return ((dy == 0 && dx == 1) || (dy == 1 && dx == 0));
        }

        /// <summary>
        /// Checks for Catapult's legal attacks.
        /// </summary>
        /// <param name="newSquare">New square coordinates.</param>
        /// <returns>Boolean true if Catapult is allowed to capture the Piece on the new square.</returns>
        public override bool CanAttack(Square newSquare)
        {
            if (newSquare.Occupant is Wall)
                return false;

            int dy = newSquare.Row - Square.Row;
            int dx = newSquare.Col - Square.Col;

            //Absolute value of dx and dy
            if (dx < 0) dx = -dx;
            if (dy < 0) dy = -dy;

            //Catapult is able to capture pieces that are either 3 squares away in a cardinal direction
            //or 2 squares away in two perpendicular cardinal directions
            return ((dy == 0 && dx == 3) || (dy == 3 && dx == 0) || (dy == 2 && dx == 2));
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