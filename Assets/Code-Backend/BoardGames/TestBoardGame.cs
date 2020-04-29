using UnityEngine;
using UnityEngine.SceneManagement;

public class TestBoardGame : MonoBehaviour, IBoardGame 
{
    public int GetMaxPlayers()
    {
        return 4;
    }

    public string GetScene()
    {
        return "GooseBoardGame"; 
    }

    public void InstantiateScene()
    {
        SceneManager.LoadScene("AREngine", LoadSceneMode.Additive);
    }

    public void PlaceBoard(Pose hitPose)
    {
        Debug.Log("Placing board");
        Instantiate(gameObject, hitPose.position, hitPose.rotation);
    }
}
