
using Photon.Pun;
using UnityEngine.UI;

public class GameRoomClientUIScreen : BasicUIScreen
{
    public Text screenLabel;
    public Text returnText;
    public Text roomDescriptionText;
    public Text playerListTitle;
    public Text playerList;

    public Button readyButton;

    private int currentPlayerCount;

    public void Update()
    {
        if(currentPlayerCount != PhotonNetwork.PlayerList.Length)
        {
            UpdatePlayerList();
        }
    }

    public void DisconnectAndReturn()
    {
        PhotonNetwork.Disconnect();
        UIManager.Instance.ActivateScreen(UIManager.Instance.initScreen);
        Game.NETWORK.ConnectToMaster();
    }
    private void UpdatePlayerList()
    {
        playerList.text = "";
        playerListTitle.text = LanguageManager.Instance.GetWord(playerListTitle.name) + PhotonNetwork.PlayerList.Length + "/" + PhotonNetwork.CurrentRoom.MaxPlayers;
        foreach (var player in PhotonNetwork.PlayerList)
        {
            playerList.text += player.NickName + "\n";
        }
    }
    public override void FillText()
    {
        screenLabel.text = LanguageManager.Instance.GetWord(screenLabel.name) + PhotonNetwork.CurrentRoom.Name;
        returnText.text = LanguageManager.Instance.GetWord(returnText.name);
        roomDescriptionText.text = LanguageManager.Instance.GetWord(roomDescriptionText.name);
        readyButton.GetComponentInChildren<Text>().text = LanguageManager.Instance.GetWord(readyButton.name);

        UpdatePlayerList();
        currentPlayerCount = PhotonNetwork.PlayerList.Length;
    }
    public override void HandleError(string errorName)
    {
        throw new System.NotImplementedException("Error Name: " + errorName + " has not been handled");
    }
}
