using System;
using UnityEngine;

namespace Sources.Runtime.Gameplay.Character
{
    public sealed class CharacterJumper : IDisposable
    {
        public bool IsGrounded => _controller.isGrounded;

        private readonly CharacterController _controller;
        private readonly CharacterData _data;
        private readonly CharacterInput _input;

        private float _verticalVelocity;

        public CharacterJumper(CharacterController controller, CharacterData data, CharacterInput input)
        {
            _controller = controller;
            _data = data;
            _input = input;

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
        }

        private void Jump()
        {
            if (IsGrounded == true)
                _verticalVelocity = Mathf.Sqrt(_data.JumpForce * -2f * _data.Gravity);
        }
        
        public void Dispose() => 
            _input.Movement.Jump.performed -= context => Jump();
    }
}