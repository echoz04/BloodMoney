using UnityEngine;

namespace Sources.Runtime.Gameplay.Character
{
    public sealed class CharacterView
    {
        private readonly Animator _animator;
        private readonly CharacterMover _mover;
        private readonly CharacterJumper _jumper;
        
        private float _animationSmoothTime = 0.1f; 
        private float _currentMoveSpeed;
        private float _moveSpeedVelocity;

        public CharacterView(Animator animator, CharacterMover mover, CharacterJumper jumper)
        {
            _animator = animator;
            _mover = mover;
            _jumper = jumper;

            _mover.MovementDirectionComputed += SetMoveAnimations;
        }

        private void SetMoveAnimations(Vector3 moveDirection)
        {
            float targetSpeed = moveDirection.magnitude;
            _currentMoveSpeed = Mathf.SmoothDamp(_currentMoveSpeed, targetSpeed, ref _moveSpeedVelocity, _animationSmoothTime);
            _animator.SetFloat("MoveSpeed", _currentMoveSpeed);
        }

        public void Tick()
        {
            _animator.SetBool("Grounded", _jumper.IsGrounded);
        }
    }
}