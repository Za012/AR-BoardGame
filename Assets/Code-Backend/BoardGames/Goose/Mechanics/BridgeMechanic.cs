
public class BridgeMechanic : MechanicControl 
{
    public int numberOfMoves;

    public override void DoMechanic(GoosePlayer player)
    {
        control.Move(numberOfMoves);
    }
}
