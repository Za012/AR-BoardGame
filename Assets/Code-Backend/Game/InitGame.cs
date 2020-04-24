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
        /*        Scene s = SceneManager.GetSceneByName("BoardGame");
                SceneManager.SetActiveScene(s);
                foreach (GameObject o in s.GetRootGameObjects())
                {
                    IBoardGame bGame = o.GetComponent<IBoardGame>();
                    o.SetActive(true);
                    if (bGame != null)
                    {
                        Game.CURRENTGAME = bGame;
                        break;
                    }
                }*/
        Debug.Log("Initialized");
    }
}
