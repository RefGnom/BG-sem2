using System;
using UnityEngine;
using UnityEngine.AI;

public class EnemyContollor : MonoBehaviour
{
    public float lookRadius = 10f;

    Transform target;
    NavMeshAgent agent;
    CharacterCombat combat;

    bool IsPaused => GameManager.instance.PauseManager.IsPaused;

    void Start()
    {
        target = PlayerManager.instance.player.transform;
        combat = GetComponent<CharacterCombat>();
        agent = GetComponent<NavMeshAgent>();
    }

    void Update()
    {
        if (IsPaused)
            return;
        var distance = Vector3.Distance(transform.position, target.position);
        if (distance < lookRadius)
        {
            agent.SetDestination(target.position);

            if (distance <= agent.stoppingDistance)
            {
                var targetStats = target.GetComponent<CharacterStats>();
                if (targetStats != null )
                {
                    combat.Attack(targetStats);
                }
                FaceTarget();
            }
        }
    }

    void FaceTarget()
    {
        var direction = (target.position - transform.position).normalized;
        var lookRotation = Quaternion.LookRotation(new Vector3(direction.x, 0, direction.z));
        transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * 5);
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, lookRadius);
    }
}
