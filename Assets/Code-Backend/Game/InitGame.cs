using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class InitGame : MonoBehaviour
{
    private InitGame Instance;
    private void Awake()
    {
        if (Instance != null)
            return;

        Instance = this;
        Screen.sleepTimeout = SleepTimeout.NeverSleep;
        StartCoroutine(LoadGame());
        DontDestroyOnLoad(gameObject);
    }

    private IEnumerator LoadGame()
    {
        Debug.Log("Loading...");
        SceneManager.LoadScene(
            "UI", LoadSceneMode.Additive);
        yield return null;
        Debug.Log("Loaded.");
        Init();
    }

    private void Init()
    {
        Debug.Log("First Init");
        // Resources that needs to be loaded in before game starts
        Scene s = SceneManager.GetSceneByName("UI");
        SceneManager.SetActiveScene(s);
        foreach (GameObject o in s.GetRootGameObjects())
        {
            NetworkConnectionManager network = o.GetComponent<NetworkConnectionManager>();
            if (network != null)
            {
                Game.NETWORK = network;
                Game.NETWORK.ConnectToMaster();
                break;
            }
        }
        Debug.Log("Initialized");
    }
}
