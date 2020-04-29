using UnityEngine;

public interface IBoardGame
{
    void PlaceBoard(Pose hitPose);
    void InstantiateScene();

    // BoardGame object must also include Serialize and Deserialize functions in order to be added to the PhotonNetwork.
}
