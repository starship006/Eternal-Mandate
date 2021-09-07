using UnityEngine;

namespace Assets.Game.Scripts.Player_Systems {
    [RequireComponent(typeof(CharacterController))]
    public class Movement : MonoBehaviour {
        [SerializeField] private float _speed = 12f;
        [SerializeField] private float _gravity = -10f;
        [SerializeField] private float _jumpHeight = 2f;

        [SerializeField] private Transform _groundCheck;
        [SerializeField] private float _groundDistance = 0.4f;
        [SerializeField] private LayerMask _groundMask;

        private CharacterController _controller;
        private Vector2 _moveDir;
        private Vector3 _velocity;
        private float _rotation;
        private bool _isGrounded;

        public bool CanJump => _isGrounded;

        private void Awake()
        {
            _controller = GetComponent<CharacterController>();
        }

        private void Update()
        {
            _isGrounded = Physics.CheckSphere(_groundCheck.position, _groundDistance, _groundMask);

            if (_isGrounded && _velocity.y < 0) {
                _velocity.y = -2f;
            }

            Vector3 move = transform.forward * _moveDir.y;
            _controller.Move(move * (_speed * Time.deltaTime));

            _velocity.y += _gravity * Time.deltaTime;
            _controller.Move(_velocity * Time.deltaTime);

            _rotation += _moveDir.x;
            transform.rotation = Quaternion.Euler(0, _rotation, 0);
        }

        public void Jump()
        {
            if (!CanJump) return;
            _velocity.y = Mathf.Sqrt(_jumpHeight * -2f * _gravity);
        }

        public void SetMovementDirection(Vector2 dir)
        {
            _moveDir = dir.normalized;
        }
    }
}