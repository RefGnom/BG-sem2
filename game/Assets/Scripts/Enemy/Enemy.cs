using UnityEngine;

[RequireComponent (typeof(CharacterStats))]
public class Enemy : Interactable
{
    CharacterStats myStats;

    private new void Start()
    {
        base.Start();
        myStats = GetComponent<CharacterStats>();
    }

    public override void Interact()
    {
        CharacterCombat playerCombat = playerManager.player.GetComponent<CharacterCombat>();
        if (playerCombat != null)
        {
            playerCombat.Attack(myStats);
        }
    }
}
