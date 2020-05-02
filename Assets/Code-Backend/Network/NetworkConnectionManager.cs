using UnityEngine;
using Photon.Pun;
using Photon.Realtime;

public class NetworkConnectionManager : MonoBehaviourPunCallbacks
{
    public GameRoomUIScreen hostUIScreen;
    public GameRoomUIScreen clientUIScreen;
    public string PlayerName { get; set; }

    public void Start()
    {
        PlayerName = "[GameMaster]Plez";
    }

    // MASTER CONNECTIONS //
    public bool ConnectToMaster()
    {
        if (string.IsNullOrEmpty(PlayerName))
        {
            UIManager.Instance.ActivateErrorScreen("NoPlayerName");
            return false;
        }
        PhotonNetwork.NickName = PlayerName;

        PhotonNetwork.AutomaticallySyncScene = true;
        PhotonNetwork.ConnectUsingSettings();           //automatic connection based on the config file in Photon/PhotonUnityNetworking/Resources/PhotonServerSettings.asset
        return true;
    }

    public override void OnConnectedToMaster()
    {
        base.OnConnectedToMaster();
        Debug.Log("Connected to Master!");
    }

    public override void OnDisconnected(DisconnectCause cause)
    {
        base.OnDisconnected(cause);
        Debug.Log(cause);
        if(cause != DisconnectCause.DisconnectByClientLogic)
        {
            UIManager.Instance.ActivateErrorScreen("Disconnected");
        }
    }

    // USER INTERACTIONS //
    public void OnClickCreateRoom(string roomName)
    {
        if (!PhotonNetwork.IsConnected)
        {
            if (!ConnectToMaster())
                return;
        }
        PhotonNetwork.CreateRoom(roomName, new RoomOptions { MaxPlayers = (byte)Game.CURRENTGAMEMETADATA.GetMaxPlayers() });
    }
    public void OnClickConnectToRoom(string roomName)
    {
        if (!PhotonNetwork.IsConnected)
        {
            if (!ConnectToMaster())
                return;
        }
        PhotonNetwork.JoinRoom(roomName);   //Join a specific Room   - Error: OnJoinRoomFailed  
    }



    // FAILURE //
    public override void OnJoinRoomFailed(short returnCode, string message)
    {
        base.OnJoinRandomFailed(returnCode, message);
        UIManager.Instance.ActivateErrorScreen("RoomNotExist");
    }

    public override void OnCreateRoomFailed(short returnCode, string message)
    {
        base.OnCreateRoomFailed(returnCode, message);
        UIManager.Instance.ActivateErrorScreen("CreateRoomFail");
        Debug.Log(message);
    }


    // SUCCESS //
    public override void OnCreatedRoom()
    {
        base.OnCreatedRoom();
        Debug.Log("Room: " + PhotonNetwork.CurrentRoom.Name + " has been created!");
    }
    public override void OnJoinedRoom()
    {
        base.OnJoinedRoom();
        if (PhotonNetwork.IsMasterClient)
        {
            UIManager.Instance.ActivateScreen(hostUIScreen);
        }
        else
        {
            UIManager.Instance.ActivateScreen(clientUIScreen);
        }
        Debug.Log("Master: " + PhotonNetwork.IsMasterClient + " | Players In Room: " + PhotonNetwork.CurrentRoom.PlayerCount + " | RoomName: " + PhotonNetwork.CurrentRoom.Name);
    }
}
