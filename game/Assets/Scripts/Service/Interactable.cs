using UnityEngine;

public abstract class Interactable : MonoBehaviour, IPauseHandler
{
    [SerializeField] float radius = 3f;
    [SerializeField] Transform interactionTransform;

    protected PlayerManager playerManager;

    protected string hintText;
    protected bool isSingleInteract;

    private bool isPaused;
    private bool hintIsVisible;
    private bool isBreak;

    public void Start()
    {
        playerManager = PlayerManager.instance;
        GameManager.Instance.PauseManager.Register(this);
        Init();
    }

    public abstract void Init();

    /// <returns>return true if interact successful else false</returns>
    public abstract bool Interact();
    
    public void Update()
    {
        if (isPaused || isBreak)
            return;
        var distance = Vector3.Distance(interactionTransform.position, playerManager.player.transform.position);
        if (distance <= radius)
        {
            if (!hintIsVisible)
               ShowHint($"{hintText} \"E\"");
            hintIsVisible = true;
            if (Input.GetKeyDown(KeyCode.E))
            {
                if (Interact() && isSingleInteract)
                {
                    isBreak = true;
                    ShowHint("");
                }
            }
        }
        else if (hintIsVisible)
        {
            ShowHint("");
            hintIsVisible = false;
        }
    }

   protected void ShowHint(string message)
    {
        GameManager.Instance.PlayerHint.text = message;
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(interactionTransform.position, radius);
    }

    public void SetPaused(bool isPaused)
    {
        this.isPaused = isPaused;
    }
}