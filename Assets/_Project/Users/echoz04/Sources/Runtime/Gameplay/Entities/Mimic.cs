using Sources.Runtime.Gameplay.Character;
using UnityEngine;

namespace Sources.Runtime.Gameplay.Entities
{
    public class Mimic : MonoBehaviour
    {
        private const string ZoneTag = "CoinZone";
        
        private Vector3 _startPosition;
        private CharacterRoot _target;

        private void Start()
        {
            _startPosition = transform.position;
        }

        private void Update()
        {
            if (_target == null)
                return;
            
            transform.position = Vector3.MoveTowards(transform.position, 
                _target.transform.position, 1f * Time.deltaTime);
        }

        public void AllowTarget(CharacterRoot target) =>
            _target = target;

        public void Reset()
        {
            _target = null;
            
            transform.position = _startPosition;
        }
        
        private void OnTriggerEnter(Collider other)
        {
            if (other.gameObject.TryGetComponent(out CharacterRoot character))
                character.ApplyDamage(1);
        }
    }
}
