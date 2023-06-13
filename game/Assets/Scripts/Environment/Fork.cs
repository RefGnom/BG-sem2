using UnityEngine;

public class Fork : Interactable
{
    public override void Init()
    {
        hintText = "����� �����";
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