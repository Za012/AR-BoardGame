using UnityEngine.UI;

public class MainMenuUIScreen : BasicUIScreen
{
    public Button joinButton;
    public Button createButton;
    public Button donateButton;
    public Text screenLabel;


    public void SwitchLanguage(int id)
    {
        LanguageManager.Instance.SetLanguage((int)LanguageEnum.English == id ? LanguageEnum.English : LanguageEnum.Dutch);
        UIManager.Instance.ActivateScreen(this);
    }

    public override void FillText()
    {
        joinButton.GetComponentInChildren<Text>().text = LanguageManager.Instance.GetWord(joinButton.name);
        createButton.GetComponentInChildren<Text>().text = LanguageManager.Instance.GetWord(createButton.name);
        donateButton.GetComponentInChildren<Text>().text = LanguageManager.Instance.GetWord(donateButton.name);
        screenLabel.text = LanguageManager.Instance.GetWord(screenLabel.name);
    }

    public override void HandleError(string errorName)
    {
        throw new System.NotImplementedException("Error Name: " + errorName + " has not been handled");
    }
}
