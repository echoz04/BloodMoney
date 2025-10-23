using UnityEngine;

namespace Sources.Runtime.Gameplay.Character
{
    public sealed class CharacterRoot : MonoBehaviour
    {
        [SerializeField] private CharacterController _controller;
        [SerializeField] private Animator _animator;
        
        private CharacterData _data;
        
        private CharacterInput _input;
        private CharacterMover _mover;
        private CharacterJumper _jumper;
        private CharacterView _view;

        private void OnValidate()
        {
            _controller ??= GetComponent<CharacterController>();
            _animator ??= GetComponent<Animator>();
        }
        
        public void Initialize(CharacterData data)
        {
            _data = data;
            
            _input = new CharacterInput();
            _input.Enable();

            _mover = new CharacterMover(_input, _data, _controller, transform);
            _jumper = new CharacterJumper(_controller, _data, _input);
            _view = new CharacterView(_animator, _mover, _jumper);
        }

        private void Update()
        {
            _mover.Tick();
            _jumper.Tick();
            _view.Tick();
        }

        private void OnDestroy()
        {
            _input?.Disable();
        }
    }
}