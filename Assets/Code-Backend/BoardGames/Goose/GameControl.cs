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
        Animator.ToggleWalk();
        Debug.Log("Walking");
        for (int i = 0; i < moves; i++)
        {
            if (Player.CurrentPosition <= waypoints.Length - 1)
            {
                Debug.Log("Performing Move: "+i);

                Player.gameObject.transform.position = Vector3.Lerp(Player.gameObject.transform.position, waypoints[Player.CurrentPosition+1].transform.position, 0.5f);

              // if (Player.gameObject.transform.position == waypoints[Player.CurrentPosition].transform.position)
             //  {
                    Player.CurrentPosition += 1;
             //  }
            }
        }
        Animator.ToggleWalk();
        yield return new WaitForSeconds(1);

        Debug.Log("Stop Walking");
    }
}
