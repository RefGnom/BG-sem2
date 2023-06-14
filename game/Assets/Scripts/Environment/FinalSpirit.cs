using Assets.Scripts.Service;

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
            Settings.PlayerIsLocked = true;
            DialogManager.Instance.Enable(DialogSystem.GetFirstMessage());
            return true;
        }
    }
}