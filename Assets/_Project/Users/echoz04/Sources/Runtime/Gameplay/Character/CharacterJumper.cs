using System;
using UnityEngine;

namespace Sources.Runtime.Gameplay.Character
{
    public sealed class CharacterJumper : IDisposable
    {
        private const float JumpInAirTime = 0.1f;
        
        public bool IsGrounded => _controller.isGrounded;

        private readonly CharacterController _controller;
        private readonly CharacterData _data;
        private readonly CharacterInput _input;
        private readonly AudioSource _audioSource;

        private float _verticalVelocity;
        private bool _canJumpInAir = false;

        private float _currentJumpTimer = 0f;

        public CharacterJumper(CharacterController controller, CharacterData data, CharacterInput input, AudioSource audioSource)
        {
            _controller = controller;
            _data = data;
            _input = input;
            _audioSource = audioSource;

            _input.Movement.Jump.performed += context => Jump();
        }

        public void Tick()
        {
            bool isGrounded = _controller.isGrounded;

            if (isGrounded == true && _verticalVelocity < 0f)
                _verticalVelocity = _data.GroundStickForce;

            _verticalVelocity += _data.Gravity * Time.deltaTime;

            Vector3 verticalMove = new Vector3(0f, _verticalVelocity, 0f) * Time.deltaTime;
            
            _controller.Move(verticalMove);

            if (_controller.isGrounded == false)
            {
                if (_currentJumpTimer > 0)
                {
                    _canJumpInAir = true;
                    
                    _currentJumpTimer -= Time.deltaTime;
                }
                else
                {
                    _canJumpInAir = false;
                }
            }
            else
            {
                _currentJumpTimer = JumpInAirTime;
            }
        }

        private void Jump()
        {
            if (IsGrounded == true || _canJumpInAir == true)
            {
                _verticalVelocity = Mathf.Sqrt(_data.JumpForce * -2f * _data.Gravity);
                _audioSource.PlayOneShot(_data.JumpClip);
            }
        }
        
        public void Dispose() => 
            _input.Movement.Jump.performed -= context => Jump();
    }
}