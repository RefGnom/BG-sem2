using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    #region Singleton
    public static GameManager instance;

    void Awake()
    {
        instance = this;
    }
    #endregion

    [SerializeField] GameObject deathScreen;
    bool gameEnded;

    public void HideDeathScreen()
    {
        deathScreen.SetActive(false);
    }

    public void ShowDeathScreen()
    {
        deathScreen.SetActive(true);
        gameEnded = true;
    }

    private void Update()
    {
        if (gameEnded)
        {
            if (Input.GetButtonDown("GameRestart"))
            {
                //Time.timeScale = 1;
                SceneManager.LoadScene(SceneManager.GetSceneByName("TestScene").buildIndex);
                gameEnded = false;
            }
        }
    }
}
