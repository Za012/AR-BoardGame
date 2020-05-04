using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
using System.Collections;

public class NetworkConnectionManager : MonoBehaviourPunCallbacks
{
    public GameRoomUIScreen hostUIScreen;
    public GameRoomUIScreen clientUIScreen;
    public BasicUIScreen connectionScreen;

    public bool isAttemptingConnection = false;

    private void Update()
    {
        if (!PhotonNetwork.IsConnected && !string.IsNullOrEmpty(SaveFile.GetInstance().MetaData.playerName))
        {
            Debug.Log("State: Disconnected");
            if (isAttemptingConnection)
                return;
            Debug.Log("Retrying Connection");
            UIManager.Instance.ActivateScreenOverlayed(connectionScreen);
            if (Game.CURRENTROOM.playersInRoom.Count > 0 && !PhotonNetwork.IsMasterClient)
            {
                PhotonNetwork.ReconnectAndRejoin();
            }
            else
            {
                if (!PhotonNetwork.Reconnect())
                {
                    ConnectToMaster();
                }
            }

        }
        else
        {
            Debug.Log("State: Connected");
        }
    }

    public IEnumerator ConnectionAttemptTimeout()
    {
        isAttemptingConnection = true;
        yield return new WaitForSeconds(4);
        isAttemptingConnection = false;
    }
    // MASTER CONNECTIONS //
    public bool ConnectToMaster()
    {
        Debug.Log("Attempting connection..");
        StartCoroutine("ConnectionAttemptTimeout");
  
        if (string.IsNullOrEmpty(SaveFile.GetInstance().MetaData.playerName))
        {
            UIManager.Instance.ActivateErrorScreen("NoPlayerName");
            return false;
        }
        PhotonNetwork.NickName = SaveFile.GetInstance().MetaData.playerName;

        PhotonNetwork.AutomaticallySyncScene = true;
        try
        {
            if (!PhotonNetwork.ConnectUsingSettings())
                isAttemptingConnection = false;
        }
        catch (System.Exception)
        {
            isAttemptingConnection = false;
        }
        isAttemptingConnection = false;
        return true;
    }

    public override void OnConnectedToMaster()
    {
        base.OnConnectedToMaster();
        isAttemptingConnection = false;
        UIManager.Instance.DeactivateOverlayed(connectionScreen);
        Debug.Log("Connected to Master!");
    }

    public override void OnDisconnected(DisconnectCause cause)
    {
        base.OnDisconnected(cause);
        Debug.Log(cause);
        if(cause != DisconnectCause.DisconnectByClientLogic)
        {
            UIManager.Instance.ActivateErrorScreen("Disconnected");
            isAttemptingConnection = false;
        }
    }
    public void UpdateNickname()
    {
        PhotonNetwork.NickName = SaveFile.GetInstance().MetaData.playerName;
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
        Debug.Log(message);
        UIManager.Instance.ActivateErrorScreen("RoomNotExist");
    }

    public override void OnCreateRoomFailed(short returnCode, string message)
    {
        base.OnCreateRoomFailed(returnCode, message);
        Debug.Log(message);
        UIManager.Instance.ActivateErrorScreen("CreateRoomFail");
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
