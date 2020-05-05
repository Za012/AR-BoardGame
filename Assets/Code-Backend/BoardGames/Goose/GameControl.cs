using System.Collections;
using UnityEngine;

public class GameControl : MonoBehaviour
{
    public GameObject[] waypoints;
    public GoosePlayer Player { get; set; }
    public GooseAnimator Animator { get; set; }
    [SerializeField]
    public float movementSpeed = 1f;
    
    public float timeToMove = 0.5f;

    public void Move(int moves)
    {
        Player.transform.position = waypoints[Player.CurrentPosition].transform.transform.position;
        Debug.Log(moves);
        StartCoroutine(MovePlayer(moves));
    }
    private IEnumerator MovePlayer(int moves)
    {
        Animator.ToggleWalk();
        for (int i = 0; i < moves; i++)
        {
            Debug.Log(waypoints.Length - 1 + "//////" + Player.CurrentPosition);
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
            }
        }

        Animator.ToggleWalk();


        Debug.Log("Stop Walking");
    }
}
