
using UnityEngine.UI;

public class GameRoomHostUIScreen : BasicUIScreen
{
    public Text screenLabel;
    public Text returnText;
    public Text roomDescriptionText;
    public Text playerListTitle;
    public Button startGameButton;


    public override void FillText()
    {
        screenLabel.text = LanguageManager.Instance.GetWord(screenLabel.name);
        returnText.text = LanguageManager.Instance.GetWord(returnText.name);
        roomDescriptionText.text = LanguageManager.Instance.GetWord(roomDescriptionText.name);
        playerListTitle.text = LanguageManager.Instance.GetWord(playerListTitle.name);
        startGameButton.GetComponentInChildren<Text>().text = LanguageManager.Instance.GetWord(startGameButton.name);
    }
}
