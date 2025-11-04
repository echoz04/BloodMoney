using UnityEngine;
using VContainer;

namespace Sources.Runtime.Gameplay.Character
{
    public sealed class CharacterRoot : MonoBehaviour
    {
        [SerializeField] private CharacterController _controller;
        [SerializeField] private CharacterHealthView _healthView;
        [SerializeField] private Animator _animator;
        [SerializeField] private AudioSource _audioSource;
        
        private CharacterData _data;
        
        private CharacterInput _input;
        private CharacterMover _mover;
        private CharacterJumper _jumper;
        private CharacterHealth _health;
        private CharacterView _view;

        private void OnValidate()
        {
            _controller ??= GetComponent<CharacterController>();
            _healthView ??= GetComponent<CharacterHealthView>();
            _animator ??= GetComponent<Animator>();
        }

        [Inject]
        private void Construct(CharacterInput input)
        {
            _input = input;
            
            _input.Enable();
        }
        
        public void Initialize(CharacterData data)
        {
            _data = data;

            _mover = new CharacterMover(_input, _data, _controller, transform, _audioSource);
            _jumper = new CharacterJumper(_controller, _data, _input, _audioSource);
            _health = new CharacterHealth(_data);
            _view = new CharacterView(_animator, _mover, _jumper);
            
            _healthView.Initialize(_health, _data);
        }

        private void Update()
        {
            if(_controller.enabled == false)
                return;
            
            _mover.Tick();
            _jumper.Tick();
            _view.Tick();
        }
        
        public void ApplyDamage(int damage) =>
            _health.ApplyDamage(damage);

        private void OnDestroy()
        {
            _input.Disable();
        }
    }
}