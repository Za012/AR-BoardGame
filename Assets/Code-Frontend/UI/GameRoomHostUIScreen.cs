
using Photon.Pun;
using UnityEngine;
using UnityEngine.UI;

public class GameRoomHostUIScreen : GameRoomUIScreen
{
    public Text screenLabel;
    public Text returnText;
    public Text roomDescriptionText;


    public Button startGameButton;

    public override void FillText()
    {
        screenLabel.text = LanguageManager.Instance.GetWord(screenLabel.name) + PhotonNetwork.CurrentRoom.Name;
        returnText.text = LanguageManager.Instance.GetWord(returnText.name);
        roomDescriptionText.text = LanguageManager.Instance.GetWord(roomDescriptionText.name);
        startGameButton.GetComponentInChildren<Text>().text = LanguageManager.Instance.GetWord(startGameButton.name);

        startGameButton.interactable = false;
    }
    public override void HandleError(string errorName)
    {
        throw new System.NotImplementedException("Error Name: " + errorName + " has not been handled");
    }

    public void StartButtonState(bool state)
    {
        startGameButton.interactable = state;
    }

    public void OnStartGameClick()
    {
        Debug.Log("Game Start");

        Game.CURRENTROOM.StartGame();
    }
}
