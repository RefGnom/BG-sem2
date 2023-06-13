using Assets.Scripts.Service;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Assets.Scripts.Environment
{
    internal class FinalSpirit : Interactable
    {
        public override void Init()
        {
            hintText = "Поговорить";
            isSingleInteract = true;
        }

        public override bool Interact()
        {
            DialogManager.Instance.Enable(DialogSystem.GetFirstMessage());
            DialogManager.Instance.OnEndDialog = () =>
            {
                SceneManager.LoadScene("Second", LoadSceneMode.Single);
            };
            return true;
        }
    }
}