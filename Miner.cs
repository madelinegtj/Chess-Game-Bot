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
            int dy = newSquare.Row - Square.Row;
            int dx = newSquare.Col - Square.Col;

            //Absolute value of dx and dy
            if (dx < 0) dx = -dx;
            if (dy < 0) dy = -dy;

            //Miner can move in one of the 4 cardinal directions
            return (dy == 0 || dx == 0);
        }

        /// <summary>
        /// Checks for Miner's legal attacks.
        /// </summary>
        /// <param name="newSquare">New square coordinates.</param>
        /// <returns>Boolean true if Miner is allowed to capture the Piece on the new square.</returns>
        public override bool CanAttack(Square newSquare)
        {
            //If Piece is a Wall, Miner can capture it
            if (CanMoveTo(newSquare) && (newSquare.Occupant is Wall))
            {
                newSquare.Remove();
                return true;
            }

            //Similar to its move, Miner can attack in one of the 4 cardinal directions
            return CanMoveTo(newSquare);
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