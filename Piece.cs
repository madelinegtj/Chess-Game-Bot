namespace Advance {

    /// <summary>
    /// Represents a player Piece, based on their type (Zombie, Jester, etc.)
    /// </summary>
    public abstract class Piece {

        /// <summary>
        /// Gets the Player object.
        /// </summary>
        public Player Player { get; }
        /// <summary>
        /// Gets and privately sets the Square object.
        /// </summary>
        public Square Square { get; private set; }

        /// <summary>
        /// Defines Piece object constructor for instantiation.
        /// </summary>
        /// <param name="player">Current player.</param>
        /// <param name="initialSquare">Initial square's coordinates of the Piece.</param>
        public Piece(Player player, Square initialSquare) {
            Player = player;
            Square = initialSquare;

            //If initialSquare is given, then we can place the Piece on it.
            if (initialSquare != null)
            {
                 initialSquare.Place(this);
            }
            else
            {
                throw new Exception("Piece is not on the board");
            }
            
        }

        /// <summary>
        /// Converts and print values into a String sentence.
        /// </summary>
        /// <returns>Parsed text.</returns>
        public override string ToString() {
            return $"{Player.Colour} {GetType().Name} at {Square.Row}, {Square.Col}";
        }

        /// <summary>
        /// Checks if a Piece is on the board.
        /// </summary>
        /// <returns>Boolean true if a Piece has a square position/coordinates.</returns>
        public bool OnBoard {
            get {
                return Square != null;
            }
        }

        /// <summary>
        /// Removes Piece from the board.
        /// </summary>
        public virtual void LeaveBoard() {
            if (Square == null) throw new ArgumentNullException("Piece cannot be removed if it is not on the board");
            Square.Remove();
            Square = null;
        }

        /// <summary>
        /// Moves to its new square position.
        /// </summary>
        /// <param name="newSquare">New square coordinates.</param>
        /// <returns>Boolean true if Piece is successfully moved.</returns>
        public virtual bool MoveTo(Square newSquare) 
        {
            // Check if piece can move to that square
            if (!CanMoveTo(newSquare)) return false;
            // and check if the new square is occupied yet
            if (newSquare.IsOccupied) return false;
            // Check if the initial square is null
            if (Square == null) throw new Exception("Cannot move piece that is off the board");

            // Perform the movement by placing the piece to the new square
            LeaveBoard();
            newSquare.Place(this);
            // Change the Piece's square position to a new one
            Square = newSquare;
            //EnterBoard(newSquare);
            return true;
        }

        /// <summary>
        /// Checks for a Piece's legal movements, based on its characteristics.
        /// </summary>
        /// <param name="newSquare">New square coordinates.</param>
        /// <returns>Boolean true if Piece is allowed to move to the new square.</returns>
        public abstract bool CanMoveTo(Square newSquare);

        /// <summary>
        /// One Piece attacks another Piece on a certain square.
        /// </summary>
        /// <param name="targetSquare">Targeted Piece's square coordinates.</param>
        /// <returns>Boolean true if Piece successfully attacked the square.</returns>
        public virtual bool Attack(Square targetSquare) 
        {
            // Check if Piece can attack that square
            if (!CanAttack(targetSquare)) return false;
            // Check if the target square contains an enemy piece
            if (targetSquare.IsFree) return false;
            if (targetSquare == Square) return false;
            // Check if the initial square is null
            if (Square == null) throw new Exception("Cannot attack with piece that is off the board");

            // Perform the attack by removing the enemy piece from the board
            targetSquare.Occupant?.LeaveBoard();

            // Catapult can attack without moving
            if(Square.Occupant is not Catapult)
            {
                // Move the attacking piece to the target square
                LeaveBoard();
                targetSquare.Place(this);
                // Change the Piece's square position to its current position
                Square = targetSquare;
            }
            else
            {
                return true;
            }
            
            return true;
        }

        /// <summary>
        /// Checks for a Piece's legal attacks, based on its characteristics.
        /// </summary>
        /// <param name="newSquare">Targeted Piece's square coordinates.</param>
        /// <returns>Boolean true if Piece is allowed to attack its targeted Square.</returns>
        public abstract bool CanAttack(Square newSquare);

        /// <summary>
        /// Change the color of a Piece to its opponent color.
        /// </summary>
        internal void Defect() {
            // TODO; applicable for Jester
            // Placeholder for pieces that have to change sides

            // p.Defect() should cause p to change sides (moving to the opponent's army
            // and adopting the opponent as the Player).

            // Note that Defect() is its own inverse -- calling it twice should put the
            // piece back in the original army.
        }

        /// <summary>
        /// Gets Piece symbol/icon lettercase based on its color.
        /// </summary>
        /// <returns>The Piece's own symbol.</returns>
        public virtual char Icon {
            get {
                // Get the symbol/icon
                var res = GetType().Name[0];

                return Player.Colour == Colour.White ? Char.ToUpper(res) : Char.ToLower(res);
            }
        }

    }
}