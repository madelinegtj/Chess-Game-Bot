namespace Advance {
    
    /// <summary>
    /// Represents a Player, white or black.
    /// </summary>
    public class Player {

        /// <summary>
        /// Gets the Colour enum.
        /// </summary>
        public Colour Colour { get; }
        /// <summary>
        /// Gets the Army object.
        /// </summary>
        public Army Army { get; }
        /// <summary>
        /// Gets the Game object.
        /// </summary>
        public Game Game { get; }

        /// <summary>
        /// Defines Player object constructor for instantiation.
        /// </summary>
        /// <param name="colour">Current player's colour.</param>
        /// <param name="game">Current game.</param>
        public Player(Colour colour, Game game) {
            Colour = colour;
            Game = game;
            Army = new Army(this, Game.Board);
        }

        /// <summary>
        /// Determines a Piece's opponent color, black or white.
        /// </summary>
        /// <returns>Piece's opponent color.</returns>
        public Player Opponent {
            get {
                if (Game.White.Colour == this.Colour)
                    return Game.Black;
                else
                    return Game.White;
            }
        }

        /// <summary>
        /// Determines a Piece's movement direction, upwards or downwards.
        /// </summary>
        /// <returns>Piece's movement direction.</returns>
        public int Direction {
            get {
                //Black moves downwards; White moves upwards.
                return Colour == Colour.Black ? +1 : -1;
            }
        }

        /// <summary>
        /// Converts and print value into a String sentence.
        /// </summary>
        /// <returns>Parsed text.</returns>
        public override string ToString() {
            return $"Player {Colour}";
        }

        // Initialize our pseudorandom num generator
        static Random rand = new Random();

        /// <summary>
        /// Picks one action from a list of Piece's possible legal actions (move and attack).
        /// </summary>
        /// <param name="actions">Elements of Action list.</param>
        /// <returns>Randomly chosen action to be executed.</returns>
        public Action ChooseMove(List<Action>? actions = null) {
            // Initialize a new Action list
            if (actions == null) actions = new List<Action>();

            // List all possible actions of the Piece
            FindPossibleActions(actions);

            // Return a random action
            if (actions.Count() == 0)
                return null;
            else
                return actions[rand.Next(actions.Count())];
        }

        /// <summary>
        /// Pass each actions into FindPossibleMoves and FindPossibleAttacks methods.
        /// </summary>
        /// <param name="actions">Elements of Action list.</param>
        public void FindPossibleActions(List<Action> actions) {
            FindPossibleMoves(actions);
            FindPossibleAttacks(actions);
        }

        /// <summary>
        /// Identifies and adds all possible moves of a Piece into a list.
        /// </summary>
        /// <param name="actions">Elements of Action list.</param>
        public void FindPossibleMoves(List<Action> actions) 
        {
            foreach (var piece in Army.Pieces) 
            {
                //Iterate through all squares to check for possible moves
                foreach (var square in Game.Board.Squares) 
                {
                    //If a Square is free and it is legal for Piece to move to that square,
                    //add a set of Piece and that square to the list
                    if (square.IsFree && piece.CanMoveTo(square)) 
                    {
                        actions.Add(new Move(piece, square));
                    }
                }
            }
        }

        /// <summary>
        /// Identifies and adds all possible attacks of a Piece into a list.
        /// </summary>
        /// <param name="actions">Elements of Action list.</param>
        public void FindPossibleAttacks(List<Action> actions) 
        {
            foreach (var piece in Army.Pieces) 
            {
                //Iterate through all squares to check for possible attacks
                foreach (var square in Game.Board.Squares) 
                {
                    //If a Square contains enemy piece and it is legal for Piece to move to that square
                    //add a set of Piece and that square to the list
                    if (square.IsOccupied
                        && square.Occupant.Player != this
                        && piece.CanMoveTo(square)
                    ) {
                        actions.Add(new Attack(piece, square));
                    }
                    // Do something for Builder here.
                }
            }
        }
    }
}