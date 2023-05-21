using System.Collections;
using UnityEngine;

public class Door : Interactable
{
    [SerializeField] Transform door;
    [SerializeField] AudioSource playerFX;
    public override void Init()
    {
        message = "������� �����";
        isSingleInteract = true;
    }

    IEnumerator ShowForkWarning()
    {
        ShowHint("����� �������!");
        yield return new WaitForSeconds(1);
        ShowHint("");
    }

    public override bool Interact()
    {
        if (PlayerItems.ForkIsCollected)
        {
            Debug.Log("����� �������");
            StartCoroutine(OpenDoor());
            playerFX.Play();
            PlayerItems.ForkIsCollected = false;
            return true;
        }
        StartCoroutine(ShowForkWarning());
        return false;
    }

    IEnumerator OpenDoor()
    {
        ShowHint("");
        for (int i = 0; i < 90; i++)
        {
            door.Rotate(0, 1, 0);
            yield return new WaitForSeconds(0.01f);
        }
    }
}