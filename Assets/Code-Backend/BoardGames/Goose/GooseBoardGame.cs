using Photon.Pun;
using Photon.Realtime;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GooseBoardGame : MonoBehaviour, IBoardGame 
{
    public Player[] playersInGame;
    public int turnNumber;
    public GooseGameUI gameScreen;
    public GameObject gameBoard;
    public GoosePlayer player;
    public PhotonView view;
    private void Awake()
    {
        DontDestroyOnLoad(gameObject); // Remove??
    }
    private void Start()
    {
        turnNumber = 0;
    }

        // INIT //  // INIT //  // INIT //
    public void InstantiateScene()
    {
        SceneManager.LoadScene("AREngine", LoadSceneMode.Additive);
        if (PhotonNetwork.IsMasterClient)
        {
            int index = 0;
            List<Player> playerKeys = Game.CURRENTROOM.playersInRoom.Keys.ToList();
            playersInGame = new Player[playerKeys.Count];
            foreach (Player player in playerKeys)
            {
                playersInGame[index] = player;
                Game.CURRENTROOM.playersInRoom[player] = false;
                index++;
            }
        }
        gameScreen.gameObject.SetActive(true);
        gameScreen.ChangeAnnouncement(LanguageManager.Instance.GetWord("PlaceBoardAnnouncement"));
        gameScreen.ChangeInstruction(LanguageManager.Instance.GetWord("PlaceBoardInstructions"));
        Debug.Log("Scene Initalized - Waiting for players to place boards");
    }

    public void PlaceBoard(Pose hitPose)
    {
        Debug.Log("Placing board");
        Instantiate(gameBoard, hitPose.position, hitPose.rotation);
        view.RPC("RPC_G_PlacedBoard", RpcTarget.MasterClient, PhotonNetwork.LocalPlayer);
    }

    [PunRPC]
    private void RPC_G_PlacedBoard(Player player)
    {
        Debug.Log("Player " + player.NickName + " Has placed their board");
        Game.CURRENTROOM.playersInRoom[player] = true;
        foreach (KeyValuePair<Player,bool> p in Game.CURRENTROOM.playersInRoom)
        {
            if (!p.Value)
            {
                return;
            }
        }
        Debug.Log("Game is ready to begin!");
        view.RPC("RPC_G_BeginGame", RpcTarget.All, playersInGame);
        view.RPC("RPC_G_PlayerTurn", RpcTarget.All, playersInGame[0], 0);

    }
    [PunRPC]
    private void RPC_G_BeginGame(Player[] playersInGame)
    {
        this.playersInGame = playersInGame;
        // Change UI Settings

        // Init Goose
        player = PhotonNetwork.Instantiate(Path.Combine("PhotonPrefabs","GoosePlayerPrefab"), gameBoard.transform.position, gameBoard.transform.rotation).GetComponent<GoosePlayer>();
        player.transform.position += new Vector3(0,0.1f,0);
        Debug.Log("Game UI Initialized");
    }
        // INIT //  // INIT //  // INIT //
   

        
        // GAME LOGIC
    [PunRPC]
    private void RPC_G_PlayerTurn(Player player, int turnNumber)
    {
        Debug.Log($"It's {player.NickName} turn! | TurnNumber {turnNumber}");
        this.turnNumber = turnNumber; 
        if(player == PhotonNetwork.LocalPlayer)
        {
            Debug.Log("My turn!");
            // Play the game
            // Activate UI Stuff and the dice
        }
    }

    public void DiceRoll()
    {
        Debug.Log("Dice has been rolled");
        // Roll the dice

        // Did player win?

        // Next player plays
        turnNumber++;
        if(turnNumber >= playersInGame.Length)
        {
            turnNumber = 0;
        }
        Debug.Log($"Next player is {playersInGame[turnNumber].NickName}");
        view.RPC("PlayerTurn", playersInGame[turnNumber], turnNumber);
    }


}
