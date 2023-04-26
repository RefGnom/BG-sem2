using UnityEngine;

public class Interactable : MonoBehaviour
{
    public float radius = 3f;
    public Transform interactionTransform;

    protected PlayerManager playerManager;

    public void Start()
    {
        playerManager = PlayerManager.instance;
    }

    public virtual void Interact()
    {

    }

    public void Update()
    {
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
}
