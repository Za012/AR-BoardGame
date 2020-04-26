
using UnityEngine.UI;

public class BoardSelectUIScreen : BasicUIScreen
{
    public Text screenLabel;
    public Button gooseBoard;
    public Text returnText;

    public override void FillText()
    {
        gooseBoard.GetComponentInChildren<Text>().text = LanguageManager.Instance.GetWord(gooseBoard.name);
        screenLabel.text = LanguageManager.Instance.GetWord(screenLabel.name);
        returnText.text = LanguageManager.Instance.GetWord(returnText.name);
    }
    public override void HandleError(string errorName)
    {
        throw new System.NotImplementedException("Error Name: " + errorName + " has not been handled");
    }
}
