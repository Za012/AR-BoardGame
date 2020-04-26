using UnityEngine;

public class UIManager : MonoBehaviour
{
    public BasicUIScreen initScreen;
    private BasicUIScreen currentScreen;


    #region Singleton
    private UIManager()
    {

    }
    public static UIManager Instance;
    private void Awake()
    {
        Instance = this;
        currentScreen = initScreen;
        ActivateScreen(currentScreen);
    }
    #endregion

    public void ActivateScreen(BasicUIScreen screen)
    {
        currentScreen.gameObject.SetActive(false);
        screen.gameObject.SetActive(true);
        screen.FillText();
        currentScreen = screen;
    }

    public void ActivateErrorScreen(string errorName)
    {
        currentScreen.HandleError(errorName);
    }
}
