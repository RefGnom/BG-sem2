using System.Collections;
using UnityEngine;

public class Door : Interactable
{
    [SerializeField] Transform door;
    [SerializeField] AudioSource playerFX;
    [SerializeField] bool isLocked;
    public override void Init()
    {
        message = "Открыть дверь";
        isSingleInteract = true;
    }

    IEnumerator ShowForkWarning()
    {
        ShowHint("Дверь Заперта!");
        yield return new WaitForSeconds(1);
        ShowHint("");
    }

    public override bool Interact()
    {
        if (PlayerItems.ForkIsCollected || !isLocked)
        {
            Debug.Log("1");
            StartCoroutine(OpenDoor());
            playerFX.Play();
            PlayerItems.ForkIsCollected = false;
            return true;
        }
        StartCoroutine(ShowForkWarning());
        return true;
    }

    IEnumerator OpenDoor()
    {
        Debug.Log("Start");
        ShowHint("");
        for (int i = 0; i < 90; i++)
        {
            door.Rotate(0, 1, 0);
            yield return new WaitForSeconds(0.01f);
        }
        Debug.Log("End");
    }
}