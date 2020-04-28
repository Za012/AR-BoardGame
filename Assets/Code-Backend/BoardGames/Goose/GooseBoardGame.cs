using Photon.Pun;
using Photon.Realtime;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GooseBoardGame : MonoBehaviour, IBoardGame 
{
    public byte Id { get; set; }
    public Dictionary<int, Player> playersInGame = new Dictionary<int, Player>();
    public int turnNumber; 
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

        // INIT //  // INIT //  // INIT //
    public void InstantiateScene()
    {
        SceneManager.LoadScene("AREngine", LoadSceneMode.Additive);
        if (PhotonNetwork.IsMasterClient)
        {
            int index = 0;
            foreach (KeyValuePair<Player,bool> player in Game.CURRENTROOM.playersInRoom)
            {
                playersInGame.Add(index, player.Key);
                Game.CURRENTROOM.playersInRoom[player.Key] = false;
            }
        }
    }

    public void PlaceBoard(Pose hitPose)
    {
        Debug.Log("Placing board");
        Instantiate(gameObject, hitPose.position, hitPose.rotation);
        Game.CURRENTROOM.view.RPC("PlacedBoard", RpcTarget.MasterClient, PhotonNetwork.LocalPlayer);
    }

    [PunRPC]
    private void PlacedBoard(Player player)
    {
        Game.CURRENTROOM.playersInRoom[player] = true;
        foreach (KeyValuePair<Player,bool> p in Game.CURRENTROOM.playersInRoom)
        {
            if (!p.Value)
            {
                return;
            }
        }
        Game.CURRENTROOM.view.RPC("BeginGame", RpcTarget.All, playersInGame);
        Game.CURRENTROOM.view.RPC("PlayerTurn", RpcTarget.All, playersInGame[0], 0);

    }
    [PunRPC]
    private void BeginGame(Dictionary<int, Player> playersInGame)
    {
        this.playersInGame = playersInGame;
        // Change UI Settings
        // Init Gooses
    }
        // INIT //  // INIT //  // INIT //
   

        
        // GAME LOGIC
    [PunRPC]
    private void PlayerTurn(Player player, int turnNumber)
    {
        this.turnNumber = turnNumber;
        if(player == PhotonNetwork.LocalPlayer)
        {
            // Play the game
            // Activate UI Stuff and the dice
        }
    }

    public void DiceRoll()
    {
        // Roll the dice

        // Did player win?

        // Next player plays
        turnNumber++;
        if(turnNumber >= playersInGame.Count)
        {
            turnNumber = 0;
        }
        Game.CURRENTROOM.view.RPC("PlayerTurn", playersInGame[turnNumber], turnNumber);
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
}
