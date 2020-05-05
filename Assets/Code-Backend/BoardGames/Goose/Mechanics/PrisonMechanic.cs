
public class PrisonMechanic : MechanicControl
{
    public bool isAllowed = false;
    public override void DoMechanic(GoosePlayer player)
    {
        isAllowed = false;
    }
    public override void PassThroughMechanic()
    {
        isAllowed = true;
    }

    public override bool IsAllowed()
    {
        return isAllowed;
    }
}
