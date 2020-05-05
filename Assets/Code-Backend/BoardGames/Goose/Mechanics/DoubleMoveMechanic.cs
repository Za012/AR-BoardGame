
public class DoubleMoveMechanic : MechanicControl
{
    public override void DoMechanic(GoosePlayer player, int moves)
    {
        control.Move(moves);
    }
}
