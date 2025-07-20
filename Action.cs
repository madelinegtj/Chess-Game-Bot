namespace Advance {

    /// <summary>
    /// Represents the Action a piece can do.
    /// </summary>
    public abstract class Action {

        /// <summary>
        /// Gets the player Piece.
        /// </summary>
        public Piece Actor { get; }
        /// <summary>
        /// Gets the targeted Square.
        /// </summary>
        public Square Target { get; }

        /// <summary>
        /// Gets Player's scores and put them as a dictionary.
        /// </summary>
        public Dictionary<Colour, double> Scores { get; }
        protected bool success = false;

        /// <summary>
        /// Defines Action object constructor.
        /// </summary>
        /// <param name="actor">Current player Piece.</param>
        /// <param name="target">The targeted square of an Action.</param>
        protected Action(Piece actor, Square target) {
            Actor = actor;
            Target = target;
            Scores = new Dictionary<Colour, double>();
            Scores[Colour.Black] = double.MinValue;
            Scores[Colour.White] = double.MinValue;
        }

        /// <summary>
        /// Executes an Action.
        /// </summary>
        /// <returns>Boolean true if Action is successfully carried out.</returns>
        public abstract bool DoAction();

        //public abstract void UndoAction();
    }

    // There are 2 types of Action: Move and Attack.

    /// <summary>
    /// Represents a movement.
    /// </summary>
    public class Move : Action 
    {
        //Initialization
        private Square previousSquare;

        /// <summary>
        /// Prepares for movement.
        /// </summary>
        /// <param name="actor">Current player Piece.</param>
        /// <param name="target">Targeted square coordinates.</param>
        public Move(Piece actor, Square target) : base(actor, target) 
        {
            //Set Piece's initial square as previous square
            previousSquare = actor.Square;
        }

        /// <summary>
        /// Executes Move as an Action
        /// </summary>
        /// <returns>Boolean true if Action is successfully executed, i.e. Piece is moved.</returns>
        public override bool DoAction() {
            success = Actor.MoveTo(Target);
            return success;
        }
    }

    /// <summary>
    /// Represents an attacking action.
    /// </summary>
    public class Attack : Action 
    {
        //Initialization
        Square? originalSquare;
        Piece? opponentPiece;
        Player? opponent;

        /// <summary>
        /// Prepares to attack.
        /// </summary>
        /// <param name="actor">Current player Piece.</param>
        /// <param name="target">Targeted square coordinates.</param>
        public Attack(Piece actor, Square target) : base(actor, target) 
        {
            //Set Piece's initial square as original square
            originalSquare = actor.Square;

            //Set the targeted square occupant as opponent
            opponentPiece = target.Occupant;
            opponent = opponentPiece?.Player;
        }

        /// <summary>
        /// Executes Attack as an Action
        /// </summary>
        /// <returns>Boolean true if Action is successfully executed, i.e. Piece attacked.</returns>
        public override bool DoAction() {
            return Actor.Attack(Target);
        }
    }

}
