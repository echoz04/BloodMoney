using System;
using System.Threading;
using Cysharp.Threading.Tasks;
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
        private float _coyoteTimeCounter;
        private bool _jumpBuffered;
        private CancellationTokenSource _jumpBufferCts;

        public CharacterJumper(CharacterController controller, CharacterData data, CharacterInput input)
        {
            _controller = controller;
            _data = data;
            _input = input;

            _input.Movement.Jump.performed += OnJumpPressed;
        }

        public void Tick()
        {
            bool isGrounded = _controller.isGrounded;

            if (isGrounded)
                _coyoteTimeCounter = _data.CoyoteTime;
            else
                _coyoteTimeCounter -= Time.deltaTime;

            if (_jumpBuffered && _coyoteTimeCounter > 0f)
            {
                PerformJump();
                _jumpBuffered = false;
                _jumpBufferCts?.Cancel();
            }

            if (isGrounded && _verticalVelocity < 0f)
                _verticalVelocity = _data.GroundStickForce;

            _verticalVelocity += _data.Gravity * Time.deltaTime;

            Vector3 verticalMove = new Vector3(0f, _verticalVelocity, 0f) * Time.deltaTime;
            _controller.Move(verticalMove);
        }

        private void OnJumpPressed(UnityEngine.InputSystem.InputAction.CallbackContext context)
        {
            if (_coyoteTimeCounter > 0f)
            {
                PerformJump();
            }
            else
            {
                _jumpBuffered = true;
                _jumpBufferCts?.Cancel();
                _jumpBufferCts = new CancellationTokenSource();
                WaitAndClearJumpBufferAsync(_jumpBufferCts.Token).Forget();
            }
        }

        private void PerformJump()
        {
            _verticalVelocity = Mathf.Sqrt(_data.JumpForce * -2f * _data.Gravity);
            _coyoteTimeCounter = 0f;
        }

        private async UniTaskVoid WaitAndClearJumpBufferAsync(CancellationToken token)
        {
            try
            {
                await UniTask.Delay(TimeSpan.FromSeconds(_data.JumpBufferTime), cancellationToken: token);
                _jumpBuffered = false;
            }
            catch (OperationCanceledException)
            {
            }
        }

        public void Dispose()
        {
            _input.Movement.Jump.performed -= OnJumpPressed;
            _jumpBufferCts?.Cancel();
            _jumpBufferCts?.Dispose();
        }
    }
}
