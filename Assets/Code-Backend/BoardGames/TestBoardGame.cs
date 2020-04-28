using UnityEngine;
using UnityEngine.SceneManagement;

public class TestBoardGame : MonoBehaviour, IBoardGame 
{
    public byte Id { get; set; }

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

    public static object Deserialize(byte[] data)
    {
        var result = new TestBoardGame();
        result.Id = data[0];
        return result;
    }

    public static byte[] Serialize(object customType)
    {
        var c = (IBoardGame)customType;
        return new byte[] { c.Id };
    }

    public string GetGameName()
    {
        return "Test";
    }
}
