using Sources.Runtime.Gameplay.Character;
using Sources.Runtime.Services.Loaders.Scene;
using UnityEngine;
using VContainer;

namespace Sources.Runtime.Gameplay.Zones
{
    public class HurtZone : MonoBehaviour
    {
        private void OnTriggerEnter(Collider other)
        {
            if (other.gameObject.TryGetComponent(out CharacterRoot character))
                character.ApplyDamage(1);
        }
    }
}
