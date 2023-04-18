using UnityEngine;

public class Interactable : MonoBehaviour
{
    public float radius = 3f;
    public Transform interactionTransform;

    Transform player;
    bool hasIteracted = false;

    public void Start()
    {
        player = PlayerManager.instance.player.transform;
    }

    public virtual void Interact()
    {

    }

    public void Update()
    {
        if (!hasIteracted)
        {
            float distance = Vector3.Distance(interactionTransform.position, player.position);
            if (distance <= radius)
            {
                Debug.Log("Enteract");
                hasIteracted = true;
            }
        }
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(interactionTransform.position, radius);
    }
}
