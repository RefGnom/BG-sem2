using Assets.Scripts.Service;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    [SerializeField] GameObject deathScreen;
    [SerializeField] TextMeshProUGUI playerHint;
    [SerializeField] bool isPrologue;

    public PauseManager PauseManager { get; private set; }
    public TextMeshProUGUI PlayerHint => playerHint;
    public GameObject Player;

    public static GameManager Instance;

    void Awake()
    {
        Instance = this;
        PauseManager = new();
        if (isPrologue)
        {
            Player.GetComponent<PlayerMovement>().StartPray();
            DialogManager.Instance.Enable(DialogSystem.GetInitMessage());
            Settings.PlayerIsLocked = true;
        }
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