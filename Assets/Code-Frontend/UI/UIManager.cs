using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    public BasicUIScreen initScreen;
    private BasicUIScreen currentScreen;
    public BasicUIScreen playerNameScreen;
    private List<BasicUIScreen> overlayScreen;

    #region Singleton
    private UIManager()
    {

    }
    public static UIManager Instance;
    private void Awake()
    {
        Instance = this;
        currentScreen = initScreen;
        overlayScreen = new List<BasicUIScreen>();
        ActivateScreen(currentScreen);
        DontDestroyOnLoad(gameObject);
    }
    #endregion



    public void ActivateScreenOverlayed(BasicUIScreen screen)
    {
        if (overlayScreen.Find(x => x == screen))
            return;

        screen.gameObject.SetActive(true);
        try
        {
            screen.FillText();
        }
        catch (System.NotImplementedException e)
        {
            Debug.Log(e.Message);
        }
        overlayScreen.Add(screen);
    }


    public void DeactivateOverlayed(BasicUIScreen screen)
    {
        BasicUIScreen foundScreen = overlayScreen.Find(x => x == screen);
        if (foundScreen != null)
        {

            foundScreen.gameObject.SetActive(false);
        overlayScreen.Remove(screen);
        }
    }



    public void ActivateScreen(BasicUIScreen screen)
    {
        if(currentScreen != null)
            currentScreen.gameObject.SetActive(false);
        
        screen.gameObject.SetActive(true);
        try
        {
            screen.FillText();
        }
        catch (System.NotImplementedException e)
        {
            Debug.Log(e.Message);
        }
        currentScreen = screen;
    }

    public void ActivateErrorScreen(string errorName)
    {
        currentScreen.HandleError(errorName);
    }
}
