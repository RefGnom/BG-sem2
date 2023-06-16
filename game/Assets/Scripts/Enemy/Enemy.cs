using UnityEngine;

[RequireComponent (typeof(CharacterStats))]
public class Enemy : MonoBehaviour
{
    public float attackRadius = 3f;
    public Transform interactionTransform;

    private GameManager gameManager;
    private CharacterStats myStats;

    private bool IsPaused => GameManager.Instance.PauseManager.IsPaused;

    void Start()
    {
        gameManager = GameManager.Instance;
        myStats = GetComponent<CharacterStats>();
    }

    public void Update()
    {
        if (IsPaused)
            return;
        if (Input.GetMouseButtonDown(0))
        {
            float distance = Vector3.Distance(interactionTransform.position, gameManager.Player.transform.position);
            if (distance <= attackRadius)
            {
                Attack();
            }
        }
    }

    public void Attack()
    {
        var playerCombat = gameManager.Player.GetComponent<CharacterCombat>();
        if (playerCombat != null)
        {
            playerCombat.Attack(myStats);
        }
    }
}