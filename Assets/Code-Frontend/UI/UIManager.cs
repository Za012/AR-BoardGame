using UnityEngine;
using UnityEngine.SceneManagement;

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
        DontDestroyOnLoad(gameObject);
    }
    #endregion

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
