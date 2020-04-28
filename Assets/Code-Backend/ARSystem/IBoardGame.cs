using UnityEngine;

public interface IBoardGame
{
    byte Id { get; set; }
    void PlaceBoard(Pose hitPose);
    string GetScene();
    int GetMaxPlayers();
    string GetGameName();
    void InstantiateScene();

    // BoardGame object must also include Serialize and Deserialize functions in order to be added to the PhotonNetwork.
}
