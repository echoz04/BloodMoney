using System;
using Cysharp.Threading.Tasks;
using Sources.Runtime.Gameplay.Character;
using Sources.Runtime.Services.Loaders.Scene;
using UnityEngine;

namespace Sources.Runtime.Gameplay.Interactables
{
    [RequireComponent(typeof(Collider))]
    public class CaveTeleporter : MonoBehaviour
    {
        private const float Delay = 1f;
        private const float Cooldown = 0.5f;

        private static bool _isTeleporting;

        [SerializeField] private CurtainViewRoot _curtainViewRoot;
        [SerializeField] private CaveTeleporter _other;
        [SerializeField] private Transform _spawnPoint;

        private async void OnTriggerEnter(Collider other)
        {
            if (_isTeleporting == true)
                return;

            if (other.TryGetComponent(out CharacterController controller))
            {
                _isTeleporting = true;

                _curtainViewRoot.Show();

                await UniTask.Delay(TimeSpan.FromSeconds(Delay));

                await TeleportAsync(controller);
                
                await UniTask.Delay(TimeSpan.FromSeconds(Cooldown));
                
                _curtainViewRoot.Hide();

                _isTeleporting = false;
            }
        }

        private async UniTask TeleportAsync(CharacterController controller)
        {
            controller.enabled = false;

            Vector3 targetPos = _other.GetSpawnPoint().position;
            controller.transform.position = targetPos;

            await UniTask.Yield();

            controller.enabled = true;
        }

        public Transform GetSpawnPoint() => _spawnPoint;
    }
}