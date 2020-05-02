
using UnityEngine;
using UnityEngine.UI;

public class BoardSelectUIScreen : BasicUIScreen
{
    public Text screenLabel;
    public Button gooseBoard;
    public Text returnText;
    public Text inputField;
    public Text inputFieldLabel;

    public override void FillText()
    {
        gooseBoard.GetComponentInChildren<Text>().text = LanguageManager.Instance.GetWord(gooseBoard.name);
        screenLabel.text = LanguageManager.Instance.GetWord(screenLabel.name);
        returnText.text = LanguageManager.Instance.GetWord(returnText.name);
        inputField.text = "";
        inputFieldLabel.text = LanguageManager.Instance.GetWord(inputFieldLabel.name);
    }
    public override void HandleError(string errorName)
    {
        throw new System.NotImplementedException("Error Name: " + errorName + " has not been handled");
    }
    public void OnClickGooseGame()
    {
        Game.CURRENTGAMEMETADATA = new GooseGameMetaData();
        if (!string.IsNullOrEmpty(inputField.text))
        {
            Game.NETWORK.OnClickCreateRoom(inputField.text);
        }
    }
}
