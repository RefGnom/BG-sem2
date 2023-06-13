using Assets.Scripts.Service;
using UnityEngine;

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
            DialogManager.Instance.Enable(DialogSystem.GetMessage());
            return true;
        }
    }
}