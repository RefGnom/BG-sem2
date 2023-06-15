using Assets.Scripts.Service;
using UnityEngine;

namespace Assets.Scripts.Environment
{
    internal class EnemyInTrouble : MonoBehaviour
    {
        [SerializeField] Transform enemyTransform;
        private bool triggered;

        private void OnTriggerEnter(Collider other)
        {
            if (triggered)
                return;
            if (other.CompareTag("Player"))
            {
                DialogManager.Instance.Enable(DialogSystem.GetThirdMessage(enemyTransform));
                Settings.PlayerIsLocked = true;
                Settings.EnemiesIsPeaceful = true;
                triggered = true;
            }
        }
    }
}