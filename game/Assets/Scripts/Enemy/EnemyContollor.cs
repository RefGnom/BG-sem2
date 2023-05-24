using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyContollor : MonoBehaviour
{
    public static readonly float sneakLookRadius = 3f;
    public static readonly float defaultLookRadius = 12f;
    public static float lookRadius;
    [SerializeField] private int maxCountUpdatesIterations = 1000;
    private int countUpdatesIterations;
    [SerializeField] private List<Transform> points;
    private int currentPointIndex;
    Transform target;
    NavMeshAgent agent;
    CharacterCombat combat;

    bool IsPaused => GameManager.instance.PauseManager.IsPaused;

    void Start()
    {
        target = PlayerManager.instance.player.transform;
        combat = GetComponent<CharacterCombat>();
        agent = GetComponent<NavMeshAgent>();
        lookRadius = defaultLookRadius;
    }

    void Update()
    {
        if (IsPaused)
            return;
        var distance = Vector3.Distance(transform.position, target.position);
        if (distance < lookRadius)
        {
            agent.speed = 4;
            agent.SetDestination(target.position);

            if (distance <= agent.stoppingDistance)
            {
                var targetStats = target.GetComponent<CharacterStats>();
                if (targetStats != null)
                {
                    combat.Attack(targetStats);
                }
                FaceTarget();
            }
        }
        else
        {
            agent.speed = 2;
            countUpdatesIterations++;
            var currentPoint = points[currentPointIndex];
            agent.SetDestination(currentPoint.position);
            var distance2 = Vector3.Distance(transform.position, currentPoint.position);        
            if (distance2 <= 3 && countUpdatesIterations >= maxCountUpdatesIterations) 
            {
                currentPointIndex = (currentPointIndex + 1) % points.Count;
                countUpdatesIterations = 0;
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
