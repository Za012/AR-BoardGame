using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class InitGame : MonoBehaviour
{
    private void Awake()
    {
        StartCoroutine(LoadGame());
    }

    private IEnumerator LoadGame()
    {
        Debug.Log("Loading...");
        SceneManager.LoadScene(
            "UI", LoadSceneMode.Additive);
        /* SceneManager.LoadScene(
             "AREngine", LoadSceneMode.Additive);
         SceneManager.LoadScene(
             "BoardGame", LoadSceneMode.Additive);*/
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
