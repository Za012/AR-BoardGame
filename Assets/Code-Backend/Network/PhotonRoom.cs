using ExitGames.Client.Photon;
using Photon.Pun;
using Photon.Realtime;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PhotonRoom : MonoBehaviourPunCallbacks, IInRoomCallbacks
{
    public PhotonView view;

    public GameRoomHostUIScreen hostScreen;
    public GameRoomClientUIScreen clientScreen;

    private int playersReadyToStart;
    private int playersInGame;
    public bool isMaster;
    public Dictionary<Player, bool> playersInRoom = new Dictionary<Player, bool>();

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

    public void Wipe()
    {
        playersInGame = 0;
        playersReadyToStart = 0;
        playersInRoom = new Dictionary<Player, bool>();
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
        PhotonPeer.RegisterType(typeof(GooseGameMetaData), (byte)'G', GooseGameMetaData.Serialize, GooseGameMetaData.Deserialize);

        isMaster = false;
        playersInGame = 0;
        playersReadyToStart = 0;
    }

    public override void OnJoinedRoom()
    {
        base.OnJoinedRoom();
        isMaster = false;
        foreach (Player player in PhotonNetwork.PlayerList)
        {
            playersInRoom.Add(player, false);
        }
        UpdatePlayerUI();
    }

    private void RoomAccess()
    {
        if (PhotonNetwork.PlayerList.Length == Game.CURRENTGAMEMETADATA.GetMaxPlayers())
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
        playersInRoom.Add(newPlayer, false);
        if (PhotonNetwork.IsMasterClient)
        {
            view.RPC("RPC_RoomBoardgame", newPlayer, Game.CURRENTGAMEMETADATA);
        }
        UpdatePlayerUI();
    }
    public override void OnPlayerLeftRoom(Player otherPlayer)
    {
        base.OnPlayerLeftRoom(otherPlayer);
        Debug.Log("Player left room: " + otherPlayer.NickName);
        playersInRoom.Remove(otherPlayer);
        if (PhotonNetwork.IsMasterClient && !isMaster)
        {
            Debug.Log("This Player is now master");
            UIManager.Instance.ActivateScreen(hostScreen);
            UIManager.Instance.ActivateErrorScreen("MasterClientLeave");
            return;
        }
        UpdatePlayerUI();
        RoomAccess();
    }

    public void StartGame()
    {
        if (!PhotonNetwork.IsMasterClient)
            return;

        PhotonNetwork.CurrentRoom.IsOpen = false;
        Debug.Log("LOADING GAME SCENE");
        PhotonNetwork.LoadLevel(Game.CURRENTGAMEMETADATA.GetScene());
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
    private void RPC_RoomBoardgame(IGameMetaData boardGame)
    {
        Game.CURRENTGAMEMETADATA = boardGame;
        clientScreen.AssignBoardToUI(); ;
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
        playersInRoom[player] = true;
        UpdatePlayerUI();
    }
    private void UpdatePlayerUI()
    {
        if (PhotonNetwork.IsMasterClient)
        {
            hostScreen.UpdatePlayerList();
        }
        else
        {
            clientScreen.UpdatePlayerList();
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
        Game.CURRENTGAMEMETADATA.InstantiateScene();
        // Also instantiates the player prefab...
    }
}
