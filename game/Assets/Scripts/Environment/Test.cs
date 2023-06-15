using Assets.Scripts.Service;
using UnityEngine;

namespace Assets.Scripts.Environment
{
    internal class Test : MonoBehaviour
    {
        private bool triggered;

        private void OnTriggerEnter(Collider other)
        {
            if (triggered)
                return;
            if (other.CompareTag("Player"))
            {
                DialogManager.Instance.Enable(DialogSystem.GetTest());
                triggered = true;
                Settings.EnemiesIsPeaceful = true;
            }
        }
    }
}