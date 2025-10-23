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
        
        private float _currentTargetRotation;
        private float _timeToReachTargetRotation = 0.14f;
        private float _dampedTargetRotationCurrentVelocity;
        private float _dampedTargetRotationPassedTime;
        
        public CharacterMover(CharacterInput input, CharacterData data, CharacterController controller, Transform transform)
        {
            _data = data;
            _controller = controller;
            _input = input;
            _transform = transform;
            _camera = Camera.main;
        }

        public void Tick()
        {
            Vector2 inputDirection = _input.Movement.Move.ReadValue<Vector2>();
            Vector3 convertedDirection = new Vector3(inputDirection.x, 0, inputDirection.y);
            MovementDirectionComputed?.Invoke(convertedDirection);

            float inputAngleDirection = GetDirectionAngleFrom(convertedDirection);
            inputAngleDirection = AddCameraAngleTo(inputAngleDirection);

            if (convertedDirection != Vector3.zero)
            {
                Rotate(inputAngleDirection);
                Vector3 move = Quaternion.Euler(0, inputAngleDirection, 0) * Vector3.forward * (_data.MoveSpeed * Time.deltaTime);
                _controller.Move(move);
            }
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
            float currentYAngle = _transform.rotation.eulerAngles.y;

            float smoothedYAngle = Mathf.SmoothDampAngle(
                currentYAngle,
                _currentTargetRotation,
                ref _dampedTargetRotationCurrentVelocity,
                _timeToReachTargetRotation - _dampedTargetRotationPassedTime);

            _dampedTargetRotationPassedTime += Time.deltaTime;

            _transform.rotation = Quaternion.Euler(0, smoothedYAngle, 0);
        }

        private float AddCameraAngleTo(float angle)
        {
            angle += _camera.transform.eulerAngles.y;
            if (angle > FullRotation)
                angle -= FullRotation;
            return angle;
        }

        private float GetDirectionAngleFrom(Vector3 inputMoveDirection)
        {
            if (inputMoveDirection == Vector3.zero)
                return _transform.eulerAngles.y;
            
            float directionAngle = Mathf.Atan2(inputMoveDirection.x, inputMoveDirection.z) * Mathf.Rad2Deg;
            if (directionAngle < 0)
                directionAngle += FullRotation;
            return directionAngle;
        }
    }
}
