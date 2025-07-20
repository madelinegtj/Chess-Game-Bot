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
            if (newSquare.Occupant is Wall)
                return false;

            int dy = newSquare.Row - Square.Row;
            int dx = newSquare.Col - Square.Col;

            //Absolute value of dx and dy
            if (dx < 0) dx = -dx;
            if (dy < 0) dy = -dy;

            //Jester can move on any of the 8 adjoining squares
            return (dy <= 1 && dx <= 1);
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

        //TODO Jester can swap Piece's team
        //TODO Jester can swap place with friendly piece except jester

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
