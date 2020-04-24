using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestBoardGame : MonoBehaviour, IBoardGame 
{
    public void PlaceBoard(Pose hitPose)
    {
        Instantiate(gameObject, hitPose.position, hitPose.rotation);
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
