using System.Collections;
using UnityEngine;

public class Door : Interactable
{
    [SerializeField] Transform door;

    public override void Init()
    {
        message = "Открыть дверь";
        isSingleInteract = true;
    }

    public override bool Interact()
    {
        if (PlayerItems.ForkIsCollected)
        {
            Debug.Log("Дверь открыта");
            StartCoroutine(OpenDoor());
            return true;
        }
        Debug.Log("Нужна вилка!");
        return false;
    }

    IEnumerator OpenDoor()
    {
        for (int i = 0; i < 90; i++)
        {
            door.Rotate(0, 1, 0);
            yield return new WaitForSeconds(0.01f);
        }
    }
}