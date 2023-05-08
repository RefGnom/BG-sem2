using System;
using UnityEngine;

public class Interactable : MonoBehaviour, IPauseHandler
{
    [SerializeField] float radius = 3f;
    [SerializeField] Transform interactionTransform;

    protected PlayerManager playerManager;

    private bool isPaused;
    private bool messageIsVisible;
    private bool isBreak;
    protected string message;
    protected bool isSingleInteract;

    public void Start()
    {
        playerManager = PlayerManager.instance;
        GameManager.instance.PauseManager.Register(this);
        Init();
    }

    public virtual void Init() { }

    /// <returns> return true if interact successful else false </returns>
    public virtual bool Interact()
    {
        return false;
    }

    public void Update()
    {
        if (isPaused || isBreak)
            return;
        var distance = Vector3.Distance(interactionTransform.position, playerManager.player.transform.position);
        if (distance <= radius)
        {
            if (!messageIsVisible)
                Debug.Log($"{message} - \"E\"");
            messageIsVisible = true;
            if (Input.GetKeyDown(KeyCode.E))
            {
                if (Interact() && isSingleInteract)
                    isBreak = true;
            }
        }
        else
        {
            messageIsVisible = false;
        }
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
