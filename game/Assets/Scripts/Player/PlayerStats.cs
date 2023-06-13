using UnityEngine;

public class PlayerStats : CharacterStats
{
    public override void Die()
    {
        GameManager.Instance.ShowDeathScreen();
        GameManager.Instance.PauseManager.SetPaused(true);
    }
}
