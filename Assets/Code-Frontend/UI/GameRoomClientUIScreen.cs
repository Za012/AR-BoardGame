
using Photon.Pun;
using UnityEngine.UI;

public class GameRoomClientUIScreen : GameRoomUIScreen
{
    public Text screenLabel;
    public Text returnText;
    public Text roomDescriptionText;
    public Text boardGameName;

    public Button readyButton;


    public override void FillText()
    {
        screenLabel.text = LanguageManager.Instance.GetWord(screenLabel.name) + PhotonNetwork.CurrentRoom.Name;
        returnText.text = LanguageManager.Instance.GetWord(returnText.name);
        roomDescriptionText.text = LanguageManager.Instance.GetWord(roomDescriptionText.name);
        boardGameName.text = Game.CURRENTGAME.GetGameName();

        readyButton.GetComponentInChildren<Text>().text = LanguageManager.Instance.GetWord(readyButton.name);
    }
    public override void HandleError(string errorName)
    {
        throw new System.NotImplementedException("Error Name: " + errorName + " has not been handled");
    }

    public void OnReadyClick()
    {
        Game.CURRENTROOM.OnReadyClick();
    }
}
