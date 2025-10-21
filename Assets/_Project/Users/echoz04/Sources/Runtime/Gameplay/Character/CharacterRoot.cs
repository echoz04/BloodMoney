using UnityEngine;

namespace Sources.Runtime.Gameplay.Character
{
    public sealed class CharacterRoot : MonoBehaviour
    {
        [SerializeField] private CharacterController _controller;
        
        private CharacterData _data;
        private CharacterMover _mover;

        private void OnValidate()
        {
            _controller ??= GetComponent<CharacterController>();
        }
        
        public void Initialize(CharacterData data)
        {
            var input = new CharacterInput();
            
            _mover = new CharacterMover(input, data, _controller, transform);
        }

        private void Update()
        {
            _mover.Tick();
        }
    }
}