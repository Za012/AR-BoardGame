
using UnityEngine.UI;

public class BoardSelectUIScreen : BasicUIScreen
{
    public Text screenLabel;
    public Button gooseGameButton;
    public Button chessGameButton;
    public Text returnText;
    public Text inputField;
    public Text createGameKeyText;

    public Button openRoomButton;

    public override void FillText()
    {
        gooseGameButton.GetComponentInChildren<Text>().text = LanguageManager.Instance.GetWord(gooseGameButton.name);
        openRoomButton.GetComponentInChildren<Text>().text = LanguageManager.Instance.GetWord(openRoomButton.name);
        chessGameButton.GetComponentInChildren<Text>().text = LanguageManager.Instance.GetWord(chessGameButton.name);

        screenLabel.text = LanguageManager.Instance.GetWord(screenLabel.name);
        createGameKeyText.text = LanguageManager.Instance.GetWord(createGameKeyText.name);
        returnText.text = LanguageManager.Instance.GetWord(returnText.name);
        inputField.text = "";
        gooseGameButton.interactable = true;

    }
    public override void HandleError(string errorName)
    {
        throw new System.NotImplementedException("Error Name: " + errorName + " has not been handled");
    }

    public void OnClickGooseGame()
    {
        Game.CURRENTGAMEMETADATA = new GooseGameMetaData();
        gooseGameButton.interactable = false;
    }

    public void OnClickOpenRoom()
    {
        if (!string.IsNullOrEmpty(inputField.text) && Game.CURRENTGAMEMETADATA != null)
        {
            Game.NETWORK.OnClickCreateRoom(inputField.text);
        }
    }
}
