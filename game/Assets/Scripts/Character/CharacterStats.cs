using UnityEngine;

public class CharacterStats : MonoBehaviour
{
    public Stat damage;
    public int maxHealth = 100;
    public int CurrentHealth { get; private set; }
    [SerializeField] private HealthBar  healthBar;

    private void Start()
    {
        CurrentHealth = maxHealth;
    }

    public void TakeDamage(int damage)
    {
        CurrentHealth -= damage;
        healthBar.UpdateHealthBar(CurrentHealth, maxHealth);
        if (CurrentHealth <= 0)
        {
            Die();
        }
    }

    public virtual void Die()
    {
        Destroy(gameObject);
    }
}
