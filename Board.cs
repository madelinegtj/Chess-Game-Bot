namespace Advance {

    /// <summary>
    /// Represents the Board in Advance Game.
    /// </summary>
    public class Board 
    {
        //Board size is 9x9
        public const int Size = 9;

        //Initialize the size of one single square
        private Square[] squares = new Square[Size * Size];

        /// <summary>
        /// Defines an Enum of type Square.
        /// </summary>
        /// <returns>Squares on board as a whole.</returns>
        public IEnumerable<Square> Squares {
            get {
                return squares;
            }
        }

        /// <summary>
        /// Board object constructor; board is made up of squares with row and column number.
        /// </summary>
        public Board()
        {
            for(int row = 0; row < Size; row++) {
                for (int col = 0; col < Size; col++) {
                    Square newSquare = new Square(this, row, col);
                    Set(row, col, newSquare);
                }
            }
        }

        /// <summary>
        /// Sets the current position/coordinates as new square.
        /// </summary>
        /// <param name="row">New square row number.</param>
        /// <param name="col">New square column number.</param>
        /// <param name="newSquare">a new square with its coordinates on the board.</param>
        private void Set(int row, int col, Square newSquare) 
        {
            if (row < 0 || row >= Size || col < 0 || col >= Size) throw new ArgumentException();

            //squares[n] = newSquare => n-th square = newSquare
            squares[row * Size + col] = newSquare;
        }

        /// <summary>
        /// Gets a square from certain current position/coordinates.
        /// </summary>
        /// <param name="row">New square row number.</param>
        /// <param name="col">New square column number.</param>
        /// <returns>Square on that particular coordinates of the board.</returns>
        public Square ? Get( int row, int col) 
        {
            if (row < 0 || row >= Size || col < 0 || col >= Size) return null;

            /// squares[n] => n-th square; square with index of n in the array
            return squares[row * Size + col];
        }

        /// <summary>
        /// Prints Board state in string.
        /// </summary>
        /// <returns>Parsed squares on board state.</returns>
        public override string ToString() {
            return $"Board:\n{string.Join<Square>("\n", squares)}";
        }
    }
}