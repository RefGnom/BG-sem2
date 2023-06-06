using UnityEngine;

[RequireComponent (typeof(CharacterStats))]
public class Enemy : MonoBehaviour
{
    public float attackRadius = 3f;
    public Transform interactionTransform;

    PlayerManager playerManager;
    CharacterStats myStats;

    bool IsPaused => GameManager.instance.PauseManager.IsPaused;

    void Start()
    {
        playerManager = PlayerManager.instance;
        myStats = GetComponent<CharacterStats>();
    }

    public void Update()
    {
        if (IsPaused)
            return;
        if (Input.GetMouseButtonDown(0))
        {
            float distance = Vector3.Distance(interactionTransform.position, playerManager.player.transform.position);
            if (distance <= attackRadius)
            {
                Attack();
            }
        }
    }

    public void Attack()
    {
        var playerCombat = playerManager.player.GetComponent<CharacterCombat>();
        if (playerCombat != null)
        {
            playerCombat.Attack(myStats);
        }
    }
}