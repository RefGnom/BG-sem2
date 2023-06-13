using System.Collections;
using UnityEngine;

public class Door : Interactable
{
    [SerializeField] Transform door;
    [SerializeField] AudioSource playerFX;

    protected virtual bool IsUnlocked => true;

    public override void Init()
    {
        hintText = "������� �����";
        isSingleInteract = true;
    }

    IEnumerator ShowWarning()
    {
        ShowHint("����� �������!");
        yield return new WaitForSeconds(1);
        ShowHint("");
    }

    public override bool Interact()
    {
        if (IsUnlocked)
        {
            StartCoroutine(OpenDoor());
            if (playerFX != null)
                playerFX.Play();
            return true;
        }
        StartCoroutine(ShowWarning());
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