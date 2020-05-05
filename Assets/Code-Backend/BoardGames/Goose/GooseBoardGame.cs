using Photon.Pun;
using Photon.Realtime;
using System.Collections;
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
    private GameControl control;
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
        UIManager.Instance.ActivateScreen(gameScreen);
        gameScreen.ChangeAnnouncement(LanguageManager.Instance.GetWord("PlaceBoardAnnouncement"));
        gameScreen.ChangeInstruction(LanguageManager.Instance.GetWord("PlaceBoardInstructions"));
        Debug.Log("Scene Initalized - Waiting for players to place boards");
    }

    public void PlaceBoard(Pose hitPose)
    {
        Debug.Log("Placing board");
        GameObject boardObject = Instantiate(gameBoard, hitPose.position, hitPose.rotation);
        control = boardObject.transform.Find("GameControl").GetComponent<GameControl>();
        if (control == null)
        {
            Debug.LogError("GameControl not found");
            throw new System.Exception();
        }
        gameScreen.ChangeAnnouncement("Waiting for other players..");
        gameScreen.ChangeInstruction("");
        view.RPC("RPC_G_PlacedBoard", RpcTarget.MasterClient, PhotonNetwork.LocalPlayer);
    }

    [PunRPC]
    private void RPC_G_PlacedBoard(Player player)
    {
        Debug.Log("Player " + player.NickName + " Has placed their board");
        Game.CURRENTROOM.playersInRoom[player] = true;
        foreach (KeyValuePair<Player, bool> p in Game.CURRENTROOM.playersInRoom)
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


    // Client Entry Point
    [PunRPC]
    private void RPC_G_BeginGame(Player[] playersInGame)
    {
        gameScreen.ChangeAnnouncement("Game is starting...");
        this.playersInGame = playersInGame;
        // Change UI Settings
        gameScreen.InstantiatePlayerList(playersInGame);
        // DEBUG
        // GameObject boardObject = Instantiate(gameBoard, transform.position, transform.rotation);
        // control = boardObject.transform.Find("GameControl").GetComponent<GameControl>();

        // Init Goose
        player = PhotonNetwork.Instantiate(Path.Combine("PhotonPrefabs", "GoosePlayerPrefab"), control.waypoints[0].transform.position, control.waypoints[0].transform.rotation).GetComponent<GoosePlayer>();
        control.Player = player;
        control.Animator = player.GetComponent<GooseAnimator>();
        Debug.Log("Game UI Initialized");
    }
    // INIT //  // INIT //  // INIT //



    // GAME LOGIC
    [PunRPC]
    private void RPC_G_PlayerTurn(Player player, int turnNumber)
    {
        Debug.Log($"It's {player.NickName} turn! | TurnNumber {turnNumber}");
        this.turnNumber = turnNumber;
        gameScreen.PlayerTurn(turnNumber, playersInGame.Length - 1);
        if (player == PhotonNetwork.LocalPlayer)
        {

            Debug.Log("My turn!");
            gameScreen.ChangeAnnouncement("It's your turn!");
            StartCoroutine("DiceRoll");
        }
        else
        {
            gameScreen.ChangeAnnouncement("It's " + player.NickName + "'s turn!");
        }
    }

    public IEnumerator DiceRoll()
    {
        if (control.skipNextTurn)
        {
            control.skipNextTurn = false;
            gameScreen.ChangeAnnouncement("Turn Skipped!");
            gameScreen.ChangeInstruction("You're stuck in a place");
            yield return new WaitForSeconds(4);
            gameScreen.ChangeAnnouncement("");
            gameScreen.ChangeInstruction("");
        }
        else
        {
            // Roll the dice
            int randomDiceSide1 = 0;
            int randomDiceSide2 = 0;
            for (int i = 0; i <= 20; i++)
            {
                randomDiceSide1 = Random.Range(0, 6);
                randomDiceSide2 = Random.Range(0, 6);
                gameScreen.ChangeDiceImage(randomDiceSide1, randomDiceSide2);
                yield return new WaitForSeconds(0.1f);
            }
            yield return new WaitForSeconds(0.8f);
            gameScreen.HideDiceImage();

            int diceRoll = (randomDiceSide1 + 1) + (randomDiceSide2 + 1);

            gameScreen.ChangeAnnouncement("Dice has been rolled");
            control.Move(diceRoll);
            yield return new WaitForSeconds(5.8f);
        }

        if (control.Player.CurrentPosition == 63)
        {
            view.RPC("RPC_G_GameOver", RpcTarget.All, PhotonNetwork.LocalPlayer);
        }
        else
        {
            NextPlayer();
        }
    }

    [PunRPC]
    public void RPC_G_GameOver(Player winner)
    {
        gameScreen.ChangeAnnouncement("GAME OVER");
        gameScreen.ChangeInstruction("Winner is: " + winner.NickName);
    }

    public void NextPlayer()
    {
        // Next player plays
        turnNumber++;
        if (turnNumber > playersInGame.Length - 1)
        {
            turnNumber = 0;
        }
        Debug.Log($"Next player is {playersInGame[turnNumber].NickName}");
        view.RPC("RPC_G_PlayerTurn", RpcTarget.All, playersInGame[turnNumber], turnNumber);
    }
}
