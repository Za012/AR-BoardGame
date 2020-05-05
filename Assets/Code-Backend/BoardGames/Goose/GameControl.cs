using System.Collections;
using UnityEngine;

public class GameControl : MonoBehaviour
{
    public GameObject[] waypoints;
    public GoosePlayer Player { get; set; }
    public GooseAnimator Animator { get; set; }
    public float movementSpeed = 1f;


    public void Move(int moves)
    {
        StartCoroutine(MovePlayer(moves));
    }
    private IEnumerator MovePlayer(int moves)
    {
        if (!waypoints[Player.CurrentPosition].GetComponent<MechanicControl>().IsAllowed())
        {
            // UI STUFF
            yield return null;
        }
        Animator.ToggleWalk();
        Debug.Log("Walking");
        for (int i = 0; i < moves; i++)
        {
            if (Player.CurrentPosition <= waypoints.Length - 1)
            {
                Debug.Log("Performing Move: " + i);

                Player.gameObject.transform.position = Vector3.Lerp(Player.gameObject.transform.position, waypoints[Player.CurrentPosition + 1].transform.position, 0.5f);

                Player.CurrentPosition += 1;
                if (Player.CurrentPosition == 63)
                {
                    Animator.ToggleWalk();
                    if (moves - (i - 1) != 0)
                    {
                        StartCoroutine(MoveBackwards(i));
                        yield return null;
                    }
                }
                MechanicControl passThruMechanic = waypoints[Player.CurrentPosition].GetComponent<MechanicControl>();
                if (passThruMechanic != null)
                {
                    passThruMechanic.PassThroughMechanic();
                }
            }
        }
        Animator.ToggleWalk();

        MechanicControl mechanic = waypoints[Player.CurrentPosition].GetComponent<MechanicControl>();
        if (mechanic != null)
        {
            mechanic.DoMechanic(Player, moves);
        }
        yield return new WaitForSeconds(1);

        Debug.Log("Stop Walking");
    }

    private IEnumerator MoveBackwards(int moves)
    {
        // Toggle Rotation
        Animator.ToggleWalk();


        for (int i = 0; i < moves; i++)
        {
            Player.gameObject.transform.position = Vector3.Lerp(Player.gameObject.transform.position, waypoints[Player.CurrentPosition - 1].transform.position, 0.5f);
            yield return new WaitForSeconds(1);
        }
        // Toggle Rotation
        Animator.ToggleWalk();
    }
}
