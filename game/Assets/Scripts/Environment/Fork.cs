using UnityEngine;

public class Fork : Interactable
{
    public override void Init()
    {
        message = "����� �����";
        isSingleInteract = true;
    }

    public override bool Interact()
    {
        Debug.Log("����� �����");
        PlayerItems.ForkIsCollected = true;
        Destroy(gameObject);
        return true;
    }
}