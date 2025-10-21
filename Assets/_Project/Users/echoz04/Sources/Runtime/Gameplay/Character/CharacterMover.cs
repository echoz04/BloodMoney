using System;
using UnityEngine;

namespace Sources.Runtime.Gameplay.Character
{
    public sealed class CharacterMover
    {
        public event Action<Vector3> MovementDirectionComputed;
        
        private const float FullRotation = 360;
        
        private readonly CharacterData _data;
        private readonly CharacterController _controller;
        private readonly CharacterInput _input;
        private readonly Transform _transform;
        private readonly Camera _camera;

        private readonly float _moveSpeed;
        private readonly float _gravityScale;
        private readonly float _jumpForce;
        private readonly float _groundStickForce;
        
        private float _verticalVelocity;
        
        private float _currentTargetRotation;
        private float _timeToReachTargetRotation = 0.14f;
        private float _dampedTargetRotationCurrentVelocity;
        private float _dampedTargetRotationPassedTime;
        
        public CharacterMover(CharacterInput input, CharacterData data, CharacterController controller, Transform transform)
        {
            _data = data;
            _controller = controller;

            _input = input;
            _input.Enable();

            _moveSpeed = _data.MoveSpeed;
            _gravityScale = _data.GravityScale;
            _jumpForce = _data.JumpForce;
            _groundStickForce = _data.GroundStickForce;
            
            _transform = transform;
            _camera = Camera.main;
        }

        public void Tick()
        {
            Vector2 inputDirection = ReadMovementInput();
            Vector3 convertedDirection = GetConvertedInputDirection(inputDirection);

            MovementDirectionComputed?.Invoke(convertedDirection);

            float inputAngleDirection = GetDirectionAngleFrom(convertedDirection);
            inputAngleDirection = AddCameraAngleTo(inputAngleDirection);

            if (convertedDirection != Vector3.zero)
            {
                Rotate(inputAngleDirection);
                Move(Quaternion.Euler(0, inputAngleDirection, 0) * Vector3.forward);
            }
            else
            {
                ApplyVerticalMovement(Vector3.zero);
            }
        }

        private void Move(Vector3 inputDirection)
        {
            float scaledMoveSpeed = GetScaledMoveSpeed();

            Vector3 normalizedInputDirection = inputDirection.normalized;
            Vector3 horizontalMove = normalizedInputDirection * scaledMoveSpeed;

            ApplyVerticalMovement(horizontalMove);
        }

        private void ApplyVerticalMovement(Vector3 horizontalMove)
        {
            bool isGrounded = _controller.isGrounded;

            if (isGrounded && _verticalVelocity < 0)
                _verticalVelocity = _groundStickForce;

            if (isGrounded && _input.Movement.Jump.triggered)
                _verticalVelocity = Mathf.Sqrt(_jumpForce * -2f * _gravityScale);

            _verticalVelocity += _gravityScale * Time.deltaTime;

            Vector3 move = horizontalMove + new Vector3(0f, _verticalVelocity, 0f) * Time.deltaTime;
            _controller.Move(move);
        }

        private void Rotate(float inputAngleDirection)
        {
            if (inputAngleDirection != _currentTargetRotation)
                UpdateTargetRotationData(inputAngleDirection);

            RotateTowardsTargetRotation();
        }

        private void UpdateTargetRotationData(float targetAngle)
        {
            _currentTargetRotation = targetAngle;
            _dampedTargetRotationPassedTime = 0f;
        }

        private void RotateTowardsTargetRotation()
        {
            float currentYAngle = GetCurrentRotationAngle();

            if (currentYAngle == _currentTargetRotation)
                return;

            float smoothedYAngle = Mathf.SmoothDampAngle(
                currentYAngle,
                _currentTargetRotation,
                ref _dampedTargetRotationCurrentVelocity,
                _timeToReachTargetRotation - _dampedTargetRotationPassedTime);

            _dampedTargetRotationPassedTime += Time.deltaTime;

            Quaternion targetRotation = Quaternion.Euler(0, smoothedYAngle, 0);
            _transform.rotation = targetRotation;
        }

        private float GetCurrentRotationAngle() => _transform.rotation.eulerAngles.y;

        private float AddCameraAngleTo(float angle)
        {
            angle += _camera.transform.eulerAngles.y;
            if (angle > FullRotation)
                angle -= FullRotation;
            return angle;
        }

        private float GetDirectionAngleFrom(Vector3 inputMoveDirection)
        {
            float directionAngle = Mathf.Atan2(inputMoveDirection.x, inputMoveDirection.z) * Mathf.Rad2Deg;
            if (directionAngle < 0)
                directionAngle += FullRotation;
            return directionAngle;
        }

        private float GetScaledMoveSpeed() => _moveSpeed * Time.deltaTime;

        private Vector2 ReadMovementInput() => _input.Movement.Move.ReadValue<Vector2>();

        private Vector3 GetConvertedInputDirection(Vector2 direction) => new Vector3(direction.x, 0, direction.y);
    }
}
