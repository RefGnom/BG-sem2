using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyContollor : MonoBehaviour
{
    [SerializeField][Range(0, 360)] float viewAngle = 75f;
    [SerializeField] private float detectionRadius = 3f;
    public static readonly float sneakLookRadius = 7f;
    public static readonly float defaultLookRadius = 15f;
    public static float lookRadius;

    [SerializeField] private int maxCountUpdatesIterations = 1000;
    [SerializeField] private List<Transform> points;
    private int countUpdatesIterations;
    private int currentPointIndex;

    [SerializeField] private Transform eye;
    private Transform target;
    private NavMeshAgent agent;
    private CharacterCombat combat;

    bool IsPaused => GameManager.Instance.PauseManager.IsPaused;

    void Start()
    {
        target = GameManager.Instance.Player.transform;
        combat = GetComponent<CharacterCombat>();
        agent = GetComponent<NavMeshAgent>();
        lookRadius = defaultLookRadius;
    }

    void Update()
    {
        if (IsPaused)
            return;
        DrawViewField();
        var distance = Vector3.Distance(transform.position, target.position);
        if (distance < detectionRadius || TargetDetected())
        {
            ChaseState(distance);
        }
        else
        {
            IdleState();
        }
    }

    private void ChaseState(float distance)
    {
        agent.SetDestination(target.position);

        if (distance <= agent.stoppingDistance)
        {
            if (target.TryGetComponent<CharacterStats>(out var targetStats))
            {
                combat.Attack(targetStats);
            }
            FaceTarget();
        }
    }

    private void IdleState()
    {
        if (points.Count == 0)
            return;
        agent.speed = 2;
        var currentPoint = points[currentPointIndex];
        agent.SetDestination(currentPoint.position);
        var distance = Vector3.Distance(transform.position, currentPoint.position);
        if (distance <= 3)
        {
            countUpdatesIterations++;
        }
        if (countUpdatesIterations >= maxCountUpdatesIterations)
        {
            currentPointIndex = (currentPointIndex + 1) % points.Count;
            countUpdatesIterations = 0;
        }
    }

    private bool TargetDetected()
    {
        float actualAngle = Vector3.Angle(eye.forward, target.position - eye.position);
        var targetPostion = target.position;
        targetPostion.y += 1.5f;
        var rayTarget = targetPostion - eye.position;
        //rayTarget.y = 0;
        Debug.DrawRay(eye.position, rayTarget, Color.blue);
        if (Physics.Raycast(eye.position, rayTarget, out var hit, lookRadius))
        {
            if (actualAngle <= viewAngle / 2)
            {
                if (hit.collider.CompareTag("Player"))
                {
                    return true;
                }
            }
        }
        return false;
    }

    private void DrawViewField()
    {
    }

    void FaceTarget()
    {
        var direction = (target.position - transform.position).normalized;
        var lookRotation = Quaternion.LookRotation(new Vector3(direction.x, 0, direction.z));
        transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime);
    }

    private void OnDrawGizmosSelected()
    {
        var left = eye.position + Quaternion.Euler(new Vector3(0, viewAngle / 2, 0)) * (eye.forward * lookRadius);
        var right = eye.position + Quaternion.Euler(-new Vector3(0, viewAngle / 2, 0)) * (eye.forward * lookRadius);
        Gizmos.color = Color.yellow;
        Gizmos.DrawLine(eye.position, left);
        Gizmos.DrawLine(eye.position, right);
        Gizmos.DrawWireSphere(eye.position, detectionRadius);
    }
}
