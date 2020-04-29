using System;
using UnityEngine;

public class GooseGameMetaData : IGameMetaData
{
    public byte Id { get; set; }

    public int GetMaxPlayers()
    {
        return 8;
    }

    public string GetScene()
    {
        return "GooseBoardGame";
    }
    public string GetGameName()
    {
        return LanguageManager.Instance.GetWord("GooseBoard");
    }

    public void InstantiateScene()
    {
        TryFindBoardGame().InstantiateScene();
    }
    private IBoardGame TryFindBoardGame()
    {
        try
        {
            return GameObject.Find("GooseBoardGame").GetComponent<IBoardGame>();
        }
        catch (Exception) // Make Destroy room.
        {
            Debug.Log("GooseBoardGame may have not been initialized yet.");
        }
        return null;
    }
    public IBoardGame GetBoardGame()
    {
        return TryFindBoardGame();
    }

    public static object Deserialize(byte[] data)
    {
        var result = new GooseGameMetaData();
        result.Id = data[0];
        return result;
    }

    public static byte[] Serialize(object customType)
    {
        var c = (IGameMetaData)customType;
        return new byte[] { c.Id };
    }

}
