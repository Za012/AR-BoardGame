using Photon.Pun;
using Photon.Realtime;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public abstract class GameRoomUIScreen : BasicUIScreen
{
    public Text playerListTitle;
    public Text playerList;
    public override void FillText()
    {
        throw new NotImplementedException();
    }

    public override void HandleError(string errorName)
    {
        throw new NotImplementedException();
    }
    public override void IsUserReturning()
    {
        if (Input.GetKey(KeyCode.Escape))
        {
            DisconnectAndReturn();
        }
    }

    public void DisconnectAndReturn()
    {
        PhotonNetwork.LeaveRoom();
        UIManager.Instance.ActivateScreen(UIManager.Instance.initScreen);
        Game.CURRENTROOM.Wipe();
    }

    public void UpdatePlayerList()
    {
        playerList.text = "";
        playerListTitle.text = LanguageManager.Instance.GetWord(playerListTitle.name) + PhotonNetwork.PlayerList.Length + "/" + PhotonNetwork.CurrentRoom.MaxPlayers;
        foreach (KeyValuePair<Player, bool> pair in Game.CURRENTROOM.playersInRoom)
        {
            Debug.Log("Player: " + pair.Key.NickName);
            if (pair.Value)
            {
                playerList.text += pair.Key.NickName + "\t" + "Ready" + "\n";
            }
            else
            {
                if (pair.Key.IsMasterClient)
                {
                    playerList.text += pair.Key.NickName + "\t" + "Leader" + "\n";

                }
                else
                {
                    playerList.text += pair.Key.NickName + "\t" + "\n";
                }
            }
        }
    }
}
