namespace Advance 
{
    /// <summary>
    /// Represents Zombie object, one of the Pieces.
    /// </summary>
    public class Zombie : Piece 
    {
        /// <summary>
        /// Defines Zombie object constructor for instantiation.
        /// </summary>
        /// <param name="player">Current player; black or white.</param>
        /// <param name="initialSquare">Zombie piece's initial square coordinates.</param>
        public Zombie(Player player, Square initialSquare) : base(player, initialSquare) {
        }

        /// <summary>
        /// Checks for Zombie's legal movements.
        /// </summary>
        /// <param name="newSquare">New square coordinates.</param>
        /// <returns>Boolean true if Zombie is allowed to move to the new square.</returns>
        public override bool CanMoveTo(Square newSquare)
        {
            if (newSquare.Occupant is Wall)
                return false;

            int rowDiff = Math.Abs(newSquare.Row - Square.Row);
            int colDiff = Math.Abs(newSquare.Col - Square.Col);

            // Check if the new square is within the valid movement range of the zombie
            // upwards for white and downwards for black
            if ((Player.Colour == Colour.White) && (newSquare.Row > Square.Row))
            {
                return false;
            }

            if ((Player.Colour == Colour.Black) && (newSquare.Row < Square.Row))
            {
                return false;
            }

            // Check if the new square is adjacent to the current square
            if (rowDiff == 1 && colDiff <= 1) return true;

            return false;
        }

        /// <summary>
        /// Checks for Zombie's legal attacks.
        /// </summary>
        /// <param name="newSquare">New square coordinates.</param>
        /// <returns>Boolean true if Zombie is allowed to capture the Piece on the new square.</returns>
        public override bool CanAttack(Square newSquare) {
            if (newSquare.Occupant is Wall)
                return false;

            //Absolute value of dx and dy
            int rowDiff = newSquare.Row - Square.Row;
            int colDiff = newSquare.Col - Square.Col;

            int absRowDiff = Math.Abs(rowDiff);
            int absColDiff = Math.Abs(colDiff);

            // Check if the new square is within the valid movement range of the zombie
            // upwards for white and downwards for black
            if ((Player.Colour == Colour.White) && (newSquare.Row > Square.Row))
            {
                return false;
            }

            if ((Player.Colour == Colour.Black) && (newSquare.Row < Square.Row))
            {
                return false;
            }

            // If the new square is within the leaping attack range of the zombie
            if (absRowDiff == 2 && (absColDiff == 2 || absColDiff == 0))
            {
                if(colDiff == 0)
                {
                    //Get the intermediate squares that is right in front of current square xXx
                    Square interveningSquare = Square.Board.Get(Square.Row + Player.Direction, Square.Col);
                    if (interveningSquare.IsOccupied)
                    {
                        //If the intermediate square is occupied, Zombie can only attack 3 adj squares like its move
                        return CanMoveTo(newSquare);
                    }
                    else
                    {
                        return true;
                    }
                }
                else
                {
                    //Get the intermediate squares that is diagonal from current square XxX
                    Square interveningSquare = Square.Board.Get(Square.Row + Player.Direction, Square.Col - (colDiff%2));
                    if (interveningSquare.IsOccupied)
                    {
                        //If the intermediate square is occupied, Zombie can only attack 3 adj squares like its move
                        return CanMoveTo(newSquare);
                    }
                    else
                    {
                        return true;
                    }
                }
            }
            return false;
            
        }


        /// <summary>
        /// Gets Zombie symbol/icon lettercase based on its color.
        /// </summary>
        /// <returns>Zombie's symbol.</returns>
        public override char Icon{
            get
            {
                return Player.Colour == Colour.White ? 'Z' : 'z';
            }
        }

    }
}