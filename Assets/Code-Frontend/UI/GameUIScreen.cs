using UnityEngine.UI;

public class GameUIScreen : BasicUIScreen
{
    public Text screenLabel;
    public Text returnText;

    public override void FillText()
    {
        screenLabel.text = LanguageManager.Instance.GetWord(screenLabel.name);
        returnText.text = LanguageManager.Instance.GetWord(returnText.name);
    }
    public override void HandleError(string errorName)
    {
        throw new System.NotImplementedException("Error Name: " + errorName + " has not been handled");
    }
}
