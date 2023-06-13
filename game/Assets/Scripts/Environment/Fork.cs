using UnityEngine;

public class Fork : Interactable
{
    public override void Init()
    {
        hintText = "Взять вилку";
        isSingleInteract = true;
    }

    public override bool Interact()
    {
        PlayerItems.ForkIsCollected = true;
        ShowHint("");
        Destroy(gameObject);
        return true;
    }
}