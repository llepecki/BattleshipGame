namespace Com.Lepecki.BattleshipGame.Engine.Model
{
    public class EmptyField : Field
    {
        public override Occupant GetOwnerView()
        {
            return Occupant.Empty;
        }

        public override Occupant GetOpponentView()
        {
            if (WasFiredAt)
            {
                return Occupant.Missed;
            }

            return Occupant.Hidden;
        }
    }
}
