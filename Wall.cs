namespace Advance
{
    public class Wall : Piece
    {
        public Wall(Player player, Square initialSquare) : base(player, initialSquare)
        {
        }

        public override bool CanMoveTo(Square newSquare)
        {
            return false;
        }

        public override bool CanAttack(Square newSquare)
        {
            return false;
        }

        public override char Icon
        {
            get
            {
                return '#';
            }
        }
    }
}
