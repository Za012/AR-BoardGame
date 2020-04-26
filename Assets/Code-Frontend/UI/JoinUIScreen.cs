
using UnityEngine.UI;

public class JoinUIScreen : BasicUIScreen
{
    public Text screenLabel;
    public Text returnText;
    public Text joinGameKeyText;
    public Button joinButton;

    public override void FillText()
    {
        screenLabel.text = LanguageManager.Instance.GetWord(screenLabel.name);
        returnText.text = LanguageManager.Instance.GetWord(returnText.name);
        joinGameKeyText.text = LanguageManager.Instance.GetWord(joinGameKeyText.name);

        joinButton.GetComponentInChildren<Text>().text = LanguageManager.Instance.GetWord(joinButton.name);
    }
    public override void HandleError(string errorName)
    {
        throw new System.NotImplementedException("Error Name: " + errorName + " has not been handled");
    }
}
