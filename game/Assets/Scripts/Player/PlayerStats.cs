using UnityEngine;

public class PlayerStats : CharacterStats
{
    public override void Die()
    {
        GameManager.instance.ShowDeathScreen();
        GameManager.instance.PauseManager.SetPaused(true);
    }
}
