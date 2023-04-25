using UnityEngine;

public class PlayerStats : CharacterStats
{
    public override void Die()
    {
        GameManager.instance.ShowDeathScreen();
        //Time.timeScale = 0;
    }
}
