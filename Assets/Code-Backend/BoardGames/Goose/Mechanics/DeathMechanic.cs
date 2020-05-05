using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeathMechanic : MechanicControl
{
    public override void DoMechanic(GoosePlayer player)
    {
        // play death animation
        player.CurrentPosition = 0;
        // reposition player by running positioning on 0 moves
    }
}
