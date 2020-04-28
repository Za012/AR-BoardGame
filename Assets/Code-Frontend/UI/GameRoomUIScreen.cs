using Photon.Pun;
using Photon.Realtime;
using System;
using System.Collections.Generic;
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

    public Dictionary<Player, bool> playersInRoom = new Dictionary<Player, bool>();
    public void DisconnectAndReturn()
    {
        PhotonNetwork.Disconnect();
        UIManager.Instance.ActivateScreen(UIManager.Instance.initScreen);
        Game.NETWORK.ConnectToMaster();
    }

    public void UpdatePlayerList()
    {
        playerList.text = "";
        playerListTitle.text = LanguageManager.Instance.GetWord(playerListTitle.name) + PhotonNetwork.PlayerList.Length + "/" + PhotonNetwork.CurrentRoom.MaxPlayers;
        foreach (KeyValuePair<Player, bool> pair in playersInRoom)
        {
            if (pair.Value)
            {
                playerList.text += pair.Key.NickName + "\t" + "Ready" + "\n";
            }
            else
            {
                playerList.text += pair.Key.NickName + "\t" + "\n";
            }

        }
    }

    public void RemovePlayer(Player player)
    {
        playersInRoom.Remove(player);
        UpdatePlayerList();
    }

    public void SetPlayerReady(Player player)
    {
        playersInRoom[player] = true;
        UpdatePlayerList();
    }

    public void AddPlayer(Player player)
    {
        playersInRoom.Add(player, false);
        UpdatePlayerList();
    }
}
