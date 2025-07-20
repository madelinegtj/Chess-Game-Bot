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

        /// <summary>
        /// Undoes the action.
        /// </summary>
        public abstract void UndoAction();
    }

    // There are 2 types of Action: Move and Attack.

    /// <summary>
    /// Represents a movement.
    /// </summary>
    public class Move : Action 
    {
        private Square? previousSquare;
        private Piece? swappedPiece;
        private Square? swappedPieceOriginalSquare;

        /// <summary>
        /// Prepares for movement.
        /// </summary>
        /// <param name="actor">Current player Piece.</param>
        /// <param name="target">Targeted square coordinates.</param>
        public Move(Piece actor, Square target) : base(actor, target)
        {
            previousSquare = actor.Square;

            // When current piece is a Jester and if the target is its friendly piece, swap position
            if (target.IsOccupied
                && target.Occupant?.Player == actor.Player
                && !(target.Occupant is Jester))
            {
                swappedPiece = target.Occupant;
                swappedPieceOriginalSquare = swappedPiece.Square;
            }
        }

        /// <summary>
        /// Executes Move as an Action
        /// </summary>
        /// <returns>Boolean true if Action is successfully executed, i.e. Piece is moved.</returns>
        public override bool DoAction() {
            success = Actor.MoveTo(Target);
            return success;
        }

        /// <inheritdoc/>
        public override void UndoAction()
        {
            if (success && Actor != null && previousSquare != null)
            {
                Actor.LeaveBoard();

                if (swappedPiece != null && swappedPieceOriginalSquare != null)
                {
                    swappedPiece.LeaveBoard();
                    swappedPiece.EnterBoard(swappedPieceOriginalSquare);
                }
                Actor.EnterBoard(previousSquare);
            }
        }

        /// <inheritdoc/>
        public override string ToString()
        {
            return $"{previousSquare.Occupant} Moving to {Target.Row + 1}, {Target.Col + 1}";
        }
    }

    /// <summary>
    /// Represents an attacking action.
    /// </summary>
    public class Attack : Action 
    {
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

        /// <inheritdoc/>
        public override void UndoAction()
        {
            Actor.LeaveBoard();

            if (opponentPiece != null)
            {
                if (opponentPiece.OnBoard)
                {
                    opponentPiece.LeaveBoard();
                }

                opponentPiece.EnterBoard(Target);

                if (opponentPiece.Player != opponent)
                {
                    opponentPiece.Defect();
                }
            }
            // otherwise, u must attacked the wall  

            Actor.EnterBoard(originalSquare);
        }

        /// <inheritdoc/>
        public override string ToString()
        {
            return $"{Actor.Icon} attacking {opponentPiece.Icon}";
        }
    }

}
