using Assets.Scripts.Service;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    #region Singleton
    public static PlayerManager instance;

    void Awake()
    {
        instance = this;
    }
    #endregion

    public GameObject player;
    [SerializeField] bool isPrologue;

    void Start()
    {
        if (isPrologue)
        {
            player.GetComponent<PlayerMovement>().StartPray();
            DialogManager.Instance.Enable(DialogSystem.GetInitMessage());
            Settings.PlayerIsLocked = true;
        }
    }
}