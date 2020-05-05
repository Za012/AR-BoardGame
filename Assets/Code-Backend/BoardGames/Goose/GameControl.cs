using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameControl : MonoBehaviour
{
    public GameObject[] waypoints;
    public GoosePlayer Player { get; set; }
    public GooseAnimator Animator { get; set; }
    [SerializeField]
    public float movementSpeed = 1f;
    
    public float timeToMove = 0.5f;
    public List<int> skipTurn;
    public bool skipNextTurn;
    public void Move(int moves)
    {
        //Player.transform.position = waypoints[Player.CurrentPosition].transform.transform.position;
       // MechanicControl allowanceMechanic = waypoints[Player.CurrentPosition].GetComponent<MechanicControl>();
       // if (allowanceMechanic != null)
       // {
            if (skipNextTurn)
            {
                // UI STUFF
                return;
            }
       // }

        Debug.Log(moves);
        StartCoroutine(MovePlayer(moves));
    }
    private IEnumerator MovePlayer(int moves)
    {
        Animator.ToggleWalk();
        for (int i = 0; i < moves; i++)
        {
            if (Player.CurrentPosition <= waypoints.Length - 1)
            {
                var currentPos = Player.transform.position;
                var t = 0f;
                while (t < 1)
                {
                    t += Time.deltaTime / timeToMove;
                    Player.transform.position = Vector3.Lerp(currentPos, waypoints[Player.CurrentPosition+1].transform.position, t);
                    Player.transform.rotation = Quaternion.Lerp(Player.transform.rotation, waypoints[Player.CurrentPosition].transform.rotation, t);
                    yield return null;
                }
                Player.CurrentPosition = Player.CurrentPosition + 1;
                if (Player.CurrentPosition == 63)
                {
                    //Animator.ToggleWalk();
                    //if (moves - (i - 1) != 0)
                    //{
                    //    StartCoroutine(MoveBackwards(i));
                        yield break;
                    //}
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
        //yield return new WaitForSeconds(1);
        if (skipTurn.IndexOf(Player.CurrentPosition) != -1)
        {
            skipNextTurn = true;
        }

        Debug.Log("Stop Walking");
    }

    private IEnumerator MoveBackwards(int moves)
    {
        // Toggle Rotation
        Animator.ToggleWalk();

        for (int i = 0; i < moves; i++)
        {
            var currentPos = Player.transform.position;
            var destinationRotation = waypoints[Player.CurrentPosition].transform.rotation;
            destinationRotation.SetFromToRotation(Vector3.forward, Vector3.back);
            var t = 0f;
            while (t < 1)
            {
                t += Time.deltaTime / timeToMove;
                Player.transform.position = Vector3.Lerp(currentPos, waypoints[Player.CurrentPosition - 1].transform.position, t);
                Player.transform.rotation = Quaternion.Lerp(Player.transform.rotation, destinationRotation, t);
                yield return null;
            }
        }
        // Toggle Rotation
        Animator.ToggleWalk();
    }
}
