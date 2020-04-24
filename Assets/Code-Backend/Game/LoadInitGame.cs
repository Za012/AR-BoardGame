using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadInitGame : MonoBehaviour
{
    void Awake()
    {
        Scene levelGame = SceneManager.GetSceneByName("Game");
        if (!levelGame.isLoaded)
        {
            SceneManager.LoadScene("Game", LoadSceneMode.Single);
        }
    }
}
