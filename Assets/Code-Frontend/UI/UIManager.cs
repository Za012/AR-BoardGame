using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    public List<BasicUIScreen> screens = null;
    private BasicUIScreen currentScreen;


    #region Singleton
    private UIManager()
    {

    }
    public static UIManager Instance;
    private void Awake()
    {
        Instance = this;
        currentScreen = screens[0];
        ActivateScreen(currentScreen);
    }
    #endregion

    public void ActivateScreen(BasicUIScreen screen)
    {
        currentScreen.gameObject.SetActive(false);
        screen.gameObject.SetActive(true);
        currentScreen = screen;
    }
}
