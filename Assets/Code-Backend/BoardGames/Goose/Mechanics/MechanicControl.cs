
using UnityEngine;

public class MechanicControl : MonoBehaviour
{
    public GameControl control { get; set; }
    public virtual void DoMechanic(GoosePlayer player)
    {
        // No Override = No Mechanic Implemented
    }
    public virtual void DoMechanic(GoosePlayer player, int moves = 0)
    {
        // No Override = No Mechanic Implemented
    }
    public virtual void PassThroughMechanic()
    {
        // No Override = No Mechanic Implemented
    }

    public virtual bool IsAllowed()
    {
        return true;
        // No Override = No Mechanic Implemented
    }

    public void Awake()
    {
        control = GameObject.Find("GameControl").GetComponent<GameControl>();
    }
}
