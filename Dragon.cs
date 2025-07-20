namespace Advance 
{
    /// <summary>
    /// Represents Dragon object, one of the Pieces.
    /// </summary>
    public class Dragon : Piece 
    {
        /// <summary>
        /// Defines Dragon object constructor for instantiation.
        /// </summary>
        /// <param name="player">Current player; black or white.</param>
        /// <param name="initialSquare">Dragon piece's initial square coordinates.</param>
        public Dragon(Player player, Square initialSquare) : base(player, initialSquare) {
        }

        /// <summary>
        /// Checks for Dragon's legal movements.
        /// </summary>
        /// <param name="newSquare">New square coordinates.</param>
        /// <returns>Boolean true if Dragon is allowed to move to the new square.</returns>
        public override bool CanMoveTo(Square newSquare) 
        {
            if (newSquare.Occupant is Wall)
                return false;

            int dy = newSquare.Row - Square.Row;
            int dx = newSquare.Col - Square.Col;

            //Absolute value of dx and dy
            int adx = dx < 0 ? -dx : dx;
            int ady = dy < 0 ? -dy : dy;

            //Dragon can move horizontally, vertically, or diagonally any number of squares
            if (dy == 0 || dx == 0 || dx == dy) 
            {
                //But Dragon cannot jump over occupied squares
                for (int i = 1; i < dx; i++)
                {
                    int row = Square.Row + i * dy / ady;
                    int col = Square.Col + i * dx / adx;
                    Square interveningSquare = Square.Board.Get(row, col);

                    if (interveningSquare.IsOccupied) return false;
                }
                return true;
            }
            return false;
        }

        /// <summary>
        /// Checks for Dragon's legal attacks.
        /// </summary>
        /// <param name="newSquare">New square coordinates.</param>
        /// <returns>Boolean true if Dragon is allowed to capture the Piece on the new square.</returns>
        public override bool CanAttack(Square newSquare)
        { 
            if (newSquare.Occupant is Wall)
                return false;

            int dy = newSquare.Row - Square.Row;
            int dx = newSquare.Col - Square.Col;

            //Absolute value of dx and dy
            if (dx < 0) dx = -dx;
            if (dy < 0) dy = -dy;

            //Similar to its move, Dragon can capture horizontally, vertically, or diagonally any number of squares
            if (CanMoveTo(newSquare)) 
            {
                //except Dragon cannot capture on the 8 adjoining squares
                if (dy <= 1 && dx <= 1)
                {
                    return false;
                }
                return true;
            }
            return false;
        }

        /// <summary>
        /// Gets Dragon symbol/icon lettercase based on its color.
        /// </summary>
        /// <returns>Dragon's symbol.</returns>
        public override char Icon{
            get
            {
                return Player.Colour == Colour.White ? 'D' : 'd';
            }
        }
    }
}
