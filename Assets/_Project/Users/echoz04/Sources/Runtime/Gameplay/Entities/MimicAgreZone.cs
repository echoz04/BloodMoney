using Sources.Runtime.Gameplay.Character;
using UnityEngine;

namespace Sources.Runtime.Gameplay.Entities
{
    public class MimicAgreZone : MonoBehaviour
    {
        [SerializeField] private Mimic _mimic;
        
        private void OnTriggerEnter(Collider other)
        {
            if (other.gameObject.TryGetComponent(out CharacterRoot character))
                _mimic.AllowTarget(character);
        }
        
        private void OnTriggerExit(Collider other)
        {
            if (other.gameObject.TryGetComponent(out CharacterRoot character))
                _mimic.Reset();
        }
    }
}
