using UnityEngine.UI;

public class PlayerNameUIScreen : BasicUIScreen
{
    public Text playerNameLabel;
    public Text playerNameScreenLabel;
    public Text playerNameDescription;
    public Text playerNameInput;
    public Button playerNameContinueButton;
    public override void FillText()
    {
        playerNameLabel.text = LanguageManager.Instance.GetWord(playerNameLabel.name);
        playerNameScreenLabel.text = LanguageManager.Instance.GetWord(playerNameScreenLabel.name);
        playerNameDescription.text = LanguageManager.Instance.GetWord(playerNameDescription.name);
        playerNameContinueButton.GetComponentInChildren<Text>().text = LanguageManager.Instance.GetWord(playerNameContinueButton.name);
    }

    public override void HandleError(string errorName)
    {
        throw new System.NotImplementedException("Error Name: " + errorName + " has not been handled");
    }

    public void OnContinueClick()
    {
        if (!string.IsNullOrEmpty(playerNameInput.text))
        {
            if (playerNameInput.text.Length > 8)
            {
                UIManager.Instance.ActivateErrorScreen("NameTooLong");
                return;
            }

            SaveFile.GetInstance().MetaData.playerName = playerNameInput.text;
            Game.NETWORK.UpdateNickname();
            UIManager.Instance.ActivateScreen(UIManager.Instance.initScreen);
        }
    }
}
