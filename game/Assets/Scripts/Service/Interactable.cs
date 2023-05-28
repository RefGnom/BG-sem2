using System;
using TMPro;
using UnityEngine;

public class Interactable : MonoBehaviour, IPauseHandler
{
    [SerializeField] float radius = 3f;
    [SerializeField] Transform interactionTransform;
    [SerializeField] TMP_Text hint;


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
               ShowHint($"{message} \"E\"");
            messageIsVisible = true;
            if (Input.GetKeyDown(KeyCode.E))
            {
                var aue = Interact();
                if (aue && isSingleInteract)
                {
                    Debug.Log("isBreak = true");
                    isBreak = true;
                }
                Debug.Log($"{aue} {isSingleInteract}");
            }
        }
        else if (messageIsVisible)
        {
            ShowHint("");
            messageIsVisible = false;
        }
    }

   protected void ShowHint(string message)
    {
        hint.text = message;
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
