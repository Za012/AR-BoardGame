using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;


[RequireComponent(typeof(ARRaycastManager))]
public class PlayingBoardPlacement : MonoBehaviour
{
    private ARRaycastManager arRaycastManager;
    [System.Diagnostics.CodeAnalysis.SuppressMessage("Code Quality", "IDE0052:Remove unread private members", Justification = "<Pending>")]
    private Vector2 touchPosition;

    static List<ARRaycastHit> hits = new List<ARRaycastHit>();

    void Awake()
    {
        Debug.Log("AREngine Starting up..");
        arRaycastManager = GetComponent<ARRaycastManager>();
        touchPosition = default;
        Debug.Log("AREngine Online");
    }

    private bool TryGetTouchPosition(out Vector2 touchPosition)
    {
        if(Input.touchCount > 0)
        {
            touchPosition = Input.GetTouch(0).position;
            return true;
        }

        touchPosition = default;
        return false;
    }

    // Update is called once per frame
    void Update()
    {
        if (!TryGetTouchPosition(out Vector2 touchPosition))
            return;

        if (arRaycastManager.Raycast(touchPosition, hits, TrackableType.PlaneWithinPolygon)){
            var hitPose = hits[0].pose;
            Game.CURRENTGAME.PlaceBoard(hitPose);
        }
    }
}
