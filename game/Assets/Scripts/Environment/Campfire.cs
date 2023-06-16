using Assets.Scripts.Service;
using UnityEngine;

namespace Assets.Scripts.Environment
{
    internal class Campfire : MonoBehaviour
    {
        [SerializeField] Transform campfire;
        private bool triggered;

        private void OnTriggerEnter(Collider other)
        {
            if (triggered)
                return;
            if (other.CompareTag("Player"))
            {
                DialogManager.Instance.Enable(DialogSystem.GetSecondMessage());



                var player = GameManager.Instance.Player.transform;
                var direction = (campfire.position - player.position).normalized;
                var lookRotation = Quaternion.LookRotation(new Vector3(direction.x, 0, direction.z));
                player.rotation = Quaternion.Slerp(player.rotation, lookRotation, 1);

                var distance = Vector3.Distance(campfire.position, player.position);
                player.position += direction * (distance - 2);

                var delta = player.position.y - campfire.position.y;
                player.position -= new Vector3(0, delta, 0);



                Settings.PlayerIsLocked = true;
                Settings.EnemiesIsPeaceful = true;
                triggered = true;
            }
        }
    }
}