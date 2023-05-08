using UnityEngine;

public class Fork : Interactable
{
    public override void Init()
    {
        message = "Взять вилку";
        isSingleInteract = true;
    }

    public override bool Interact()
    {
        Debug.Log("Взяли вилку");
        PlayerItems.ForkIsCollected = true;
        Destroy(gameObject);
        return true;
    }
}