using UnityEngine;

public class Interactable : MonoBehaviour, IPauseHandler
{
    public float radius = 3f;
    public Transform interactionTransform;

    protected PlayerManager playerManager;

    bool isPaused;

    public void Start()
    {
        playerManager = PlayerManager.instance;
        GameManager.instance.PauseManager.Register(this);
    }

    public virtual void Interact()
    {

    }

    public void Update()
    {
        if (isPaused)
            return;
        if (Input.GetMouseButtonDown(0))
        {
            float distance = Vector3.Distance(interactionTransform.position, playerManager.player.transform.position);
            if (distance <= radius)
            {
                Interact();
            }
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
