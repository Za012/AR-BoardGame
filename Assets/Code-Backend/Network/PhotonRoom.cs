using ExitGames.Client.Photon;
using Photon.Pun;
using Photon.Realtime;
using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PhotonRoom : MonoBehaviourPunCallbacks, IInRoomCallbacks
{
    public PhotonView view;

    public GameRoomHostUIScreen hostScreen;
    public GameRoomClientUIScreen clientScreen;

    private int playersReadyToStart;
    private int playersInGame;

    private void Awake()
    {
        if (Game.CURRENTROOM == null)
        {
            Game.CURRENTROOM = this;
        }
        else
        {
            if (Game.CURRENTROOM != this)
            {
                Destroy(Game.CURRENTROOM.gameObject);
                Game.CURRENTROOM = this;
            }
        }
        DontDestroyOnLoad(gameObject);
    }

    public override void OnEnable()
    {
        base.OnEnable();
        PhotonNetwork.AddCallbackTarget(this);
        SceneManager.sceneLoaded += OnSceneFinishedLoading;
    }

    private void OnSceneFinishedLoading(Scene arg0, LoadSceneMode arg1)
    {
        view.RPC("RPC_PlayerLoaded", RpcTarget.MasterClient);
    }

    public override void OnDisable()
    {
        base.OnDisable();
        PhotonNetwork.RemoveCallbackTarget(this);
        SceneManager.sceneLoaded -= OnSceneFinishedLoading;
    }

    private void Start()
    {
        view = GetComponent<PhotonView>();
        // Registration of Boardgames  | Every boardgame must be registered.
        PhotonPeer.RegisterType(typeof(TestBoardGame), (byte)'G', TestBoardGame.Serialize, TestBoardGame.Deserialize);

        playersInGame = 0;
        playersReadyToStart = 0;
    }

    public override void OnJoinedRoom()
    {
        base.OnJoinedRoom();
        if (PhotonNetwork.IsMasterClient)
        {
            foreach (Player player in PhotonNetwork.PlayerList)
            {
                hostScreen.AddPlayer(player);
            }
            hostScreen.UpdatePlayerList();
        }
        else
        {
            foreach (Player player in PhotonNetwork.PlayerList)
            {
                clientScreen.AddPlayer(player);
            }
            clientScreen.UpdatePlayerList();
        }
    }

    private void RoomAccess()
    {
        if (PhotonNetwork.PlayerList.Length == Game.CURRENTGAME.GetMaxPlayers())
        {
            PhotonNetwork.CurrentRoom.IsOpen = false;
        }
        else
        {
            PhotonNetwork.CurrentRoom.IsOpen = true;
        }
    }
    public override void OnPlayerEnteredRoom(Player newPlayer)
    {
        base.OnPlayerEnteredRoom(newPlayer);
        RoomAccess();
        if (PhotonNetwork.IsMasterClient)
        {
            hostScreen.AddPlayer(newPlayer);
            view.RPC("RPC_RoomBoardgame", newPlayer, Game.CURRENTGAME);
        }
        else
        {
            clientScreen.AddPlayer(newPlayer);
        }
    }
    public override void OnPlayerLeftRoom(Player otherPlayer)
    {
        base.OnPlayerLeftRoom(otherPlayer);
        if (PhotonNetwork.IsMasterClient)
        {
            hostScreen.RemovePlayer(otherPlayer);
        }
        else
        {
            clientScreen.RemovePlayer(otherPlayer);
        }
        RoomAccess();
    }

    public void StartGame()
    {
        if (!PhotonNetwork.IsMasterClient)
            return;

        PhotonNetwork.CurrentRoom.IsOpen = false;
        Debug.Log("LOADING GAME SCENE");
        PhotonNetwork.LoadLevel(Game.CURRENTGAME.GetScene());
    }

    public void OnReadyToPlay()
    {
        view.RPC("RPC_PlayerLoaded", RpcTarget.All);
    }
    public void OnReadyClick()
    {
        view.RPC("RPC_PlayerReady", RpcTarget.All, PhotonNetwork.LocalPlayer);
    }

    [PunRPC]
    private void RPC_RoomBoardgame(IBoardGame boardGame)
    {
        Game.CURRENTGAME = boardGame;
    }

    [PunRPC]
    private void RPC_PlayerReady(Player player)
    {
        playersReadyToStart++;
        if (playersReadyToStart == PhotonNetwork.PlayerList.Length - 1)
        {
            hostScreen.StartButtonState(true);
        }
        else
        {
            hostScreen.StartButtonState(false);
        }
        if (PhotonNetwork.IsMasterClient)
        {
            hostScreen.SetPlayerReady(player);
        }
        else
        {
            clientScreen.SetPlayerReady(player);
        }
    }

    [PunRPC]
    private void RPC_PlayerLoaded()
    {
        playersInGame++;
        Debug.Log("Player Loaded!");
        if (playersInGame == PhotonNetwork.PlayerList.Length)
        {
            Debug.Log("All Players Loaded!");
            view.RPC("RPC_CreatePlayer", RpcTarget.All);
        }
    }

    [PunRPC]
    private void RPC_CreatePlayer()
    {
        // Placement of board;
        Debug.Log("Initializing Game");
        Game.CURRENTGAME.InstantiateScene();
        // Also instantiates the player prefab...
    }
}
