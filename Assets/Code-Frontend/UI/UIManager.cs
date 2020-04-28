using UnityEngine;
using UnityEngine.SceneManagement;

public class UIManager : MonoBehaviour
{
    public BasicUIScreen initScreen;
    public BasicUIScreen gameScreen;
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
        screen.FillText();
        currentScreen = screen;
    }
    public void GameMode()
    {   
/*        if (!SceneManager.GetSceneByName("UI").isLoaded)
            SceneManager.LoadScene("UI", LoadSceneMode.Additive);*/

        if (gameScreen != null)
        {
            ActivateScreen(gameScreen);
        }
    }
    public void ActivateErrorScreen(string errorName)
    {
        currentScreen.HandleError(errorName);
    }
}
