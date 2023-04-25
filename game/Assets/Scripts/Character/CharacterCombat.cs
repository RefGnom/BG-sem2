using System.Collections;
using System;
using UnityEngine;

[RequireComponent (typeof(CharacterStats))]
public class CharacterCombat : MonoBehaviour
{
    public float attackSpeed = 1f;
    private float attackCooldown = 0f;
    public float attackDelay = 1f;

    CharacterStats myStats;
    public Animator characterAnimator;

    private void Start()
    {
        myStats = GetComponent<CharacterStats>();
    }

    private void Update()
    {
        attackCooldown -= Time.deltaTime;
    }

    public void Attack(CharacterStats targetStats)
    {
        if (attackCooldown <= 0)
        {
            if (characterAnimator != null)
            {
                characterAnimator.SetTrigger("IsPunch");
            }
            StartCoroutine(DoDamage(targetStats, attackDelay));
            attackCooldown = 10 / attackSpeed;
            Debug.Log(gameObject.name + " ударил");
        }
    }

    IEnumerator DoDamage(CharacterStats stats, float delay)
    {
        yield return new WaitForSeconds(delay * Time.deltaTime);
        stats.TakeDamage(myStats.damage.GetValue());
    }
}
