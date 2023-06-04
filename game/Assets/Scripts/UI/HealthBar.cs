using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class HealthBar : MonoBehaviour
{
    private Image healthBar;
    private void Awake()
    {
        healthBar=GetComponent<Image>();
    }

    public void UpdateHealthBar(int currentHealth, int maxHealth)
    {
        healthBar.fillAmount = (float)currentHealth/maxHealth;
    }
}
