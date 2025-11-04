using Sources.Runtime.Gameplay.Character;
using Sources.Runtime.Services.Loaders.Scene;
using UnityEngine;
using VContainer;

namespace Sources.Runtime.Gameplay.Zones
{
    public class HurtZone : MonoBehaviour
    {
        [SerializeField] private float _hurtCooldown = 1;
        
        private float _hurtTimer = 0;
        
        private void OnTriggerStay(Collider other)
        {
            if (other.gameObject.TryGetComponent(out CharacterRoot character))
            {
                if (_hurtTimer <= _hurtCooldown)
                {
                    _hurtTimer += Time.deltaTime;
                }
                else
                {
                    _hurtTimer = 0;
                    character.ApplyDamage(1);
                }
            }
        }
    }
}
