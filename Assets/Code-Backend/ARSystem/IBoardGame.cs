using UnityEngine;

public interface IBoardGame
{
    byte Id { get; set; }
    void PlaceBoard(Pose hitPose);
    string GetScene();
    int GetMaxPlayers();
    void InstantiateScene();
}
