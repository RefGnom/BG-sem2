using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    public PauseManager PauseManager { get; private set; }

    [SerializeField] GameObject deathScreen;

    void Awake()
    {
        instance = this;
        PauseManager = new();
    }

    public void HideDeathScreen()
    {
        deathScreen.SetActive(false);
    }

    public void ShowDeathScreen()
    {
        deathScreen.SetActive(true);
    }

    public void Restart()
    {
        Debug.Log("Рестарт!");
        PauseManager.SetPaused(false);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}