namespace Advance {

    /// <summary>
    /// Represents each of the squares object on the board.
    /// </summary>
    public class Square {

        /// <summary>
        /// Gets the board object.
        /// </summary>
        public Board Board { get; }
        /// <summary>
        /// Gets the row number of squares position on the board.
        /// </summary>
        public int Row { get; }
        /// <summary>
        /// Gets the column number of squares position on the board.
        /// </summary>
        public int Col { get; }

        /// <summary>
        /// Defines Square object constructor for instantiation.
        /// </summary>
        /// <param name="board">Current game board.</param>
        /// <param name="row">Current square's row number.</param>
        /// <param name="col">Current square's column number.</param>
        public Square(Board board, int row, int col) {
            Board = board;
            Row = row;
            Col = col;
        }

        /// <summary>
        /// Gets Piece on that particular square.
        /// </summary>
        /// <returns>Piece that occupies the square.</returns>
        private Piece? occupant;

        /// <summary>
        /// Gets and checks for square occupant.
        /// </summary>
        /// <returns>The Piece on that square, or null if there's none.</returns>
        public Piece? Occupant {
            get { return occupant; }

            //If the occupant above is null then it's exception error
            //Else the occupant is the piece sitting on that square
            private set {
                if (value == null) throw new ArgumentNullException();
                occupant = value;
            }
        }

        /// <summary>
        /// Checks if square is free or occupied.
        /// </summary>
        /// <returns>Boolean true if square has no occupant</returns>
        public bool IsFree {
            get {
                return Occupant == null;
            }
        }

        /// <summary>
        /// Checks if square is occupied or empty.
        /// </summary>
        /// <returns>Boolean true if square is not free</returns>
        public bool IsOccupied {
            get {
                return !IsFree;
            }
        }

        /// <summary>
        /// Places a piece on a particular square.
        /// </summary>
        /// <param name="piece">The Piece that is going to be placed.</param>
        public void Place(Piece piece) {
            if (IsOccupied) throw new ArgumentException("Piece cannot be placed in occupied square.");
            Occupant = piece;
        }

        /// <summary>
        /// Removes occupant Piece from a square.
        /// </summary>
        public void Remove() {
            occupant = null;
        }

        /// <summary>
        /// Checks if a Piece is threatened.
        /// </summary>
        /// <returns>Boolean true if there's any threatening pieces.</returns>
        public bool IsThreatened {
            get {
                return ThreateningPieces.Count() != 0;
            }
        }

        /// <summary>
        /// Lists all threatening pieces that can attack a particular Piece.
        /// </summary>
        /// <returns>Threatining Pieces in a list.</returns>
        public IEnumerable<Piece> ThreateningPieces {
            get {
                //Initialize a new list for threatening pieces
                List<Piece> threateningPieces = new List<Piece>();

                //Check each square's occupant piece
                //and add the Piece to the list if it can be threatening
                foreach ( Square square in Board.Squares) {
                    Piece? p = square.Occupant;
                    if ( p != null && p.CanAttack(this)) 
                    {
                        threateningPieces.Add(p);
                    }
                }
                return threateningPieces;
            }
        }

        /// <summary>
        /// Converts and print value into a String sentence.
        /// </summary>
        /// <returns>Parsed text.</returns>
        public override string ToString() {
            if (IsFree)
                return $"Empty square at {Row}, {Col}";
            else
                return $"{Occupant}";
        }
    }
}