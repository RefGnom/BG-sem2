using UnityEngine.SceneManagement;

namespace Assets.Scripts.Environment
{
    internal class Boat : Interactable
    {
        public override void Init()
        {
            hintText = "В путь";
            isSingleInteract = true;
        }

        public override bool Interact()
        {
            SceneManager.LoadScene("Third", LoadSceneMode.Single);
            return true;
        }
    }
}