using UnityEngine;
using UnityEngine.InputSystem;

namespace Assets.Game.Scripts.Player_Systems
{
    [RequireComponent(typeof(CharacterController))]
    public class PlayerController : MonoBehaviour
    {
        [SerializeField] private InputActionReference _movementControl = null;
        [SerializeField] private InputActionReference _jumpControl = null;

        [SerializeField] private float _playerSpeed = 2.0f;
        [SerializeField] private float _jumpHeight = 1.0f;
        [SerializeField] private float _gravityValue = -9.81f;
        [SerializeField] private float _rotationSpeed = 4.0f;

        private CharacterController _controller;
        private Vector3 _playerVelocity;
        private bool _groundedPlayer;
        private Transform _cameraMain;

        private void Start()
        {
            _controller = gameObject.GetComponent<CharacterController>();
            _cameraMain = Camera.main.transform;
        }

        private void OnEnable()
        {
            _movementControl.action.Enable();
            _jumpControl.action.Enable();
        }


        private void OnDisable()
        {
            _movementControl.action.Disable();
            _jumpControl.action.Disable();
        }

        private void Update()
        {
            _groundedPlayer = _controller.isGrounded;
            if (_groundedPlayer && _playerVelocity.y < 0) {
                _playerVelocity.y = 0f;
            }

            Vector2 movement = _movementControl.action.ReadValue<Vector2>();
            Vector3 move = new Vector3(movement.x, 0, movement.y);
            move = _cameraMain.forward * move.z + _cameraMain.right * move.x;
            move.y = 0;
            _controller.Move(move * Time.deltaTime * _playerSpeed);

            if (_jumpControl.action.triggered && _groundedPlayer) {
                _playerVelocity.y += Mathf.Sqrt(_jumpHeight * -3.0f * _gravityValue);
            }

            _playerVelocity.y += _gravityValue * Time.deltaTime;
            _controller.Move(_playerVelocity * Time.deltaTime);

            if (movement != Vector2.zero) {
                float targetAngle = Mathf.Atan2(movement.x, movement.y) * Mathf.Rad2Deg + _cameraMain.eulerAngles.y;
                Quaternion rot = Quaternion.Euler(0, targetAngle, 0);
                transform.rotation = Quaternion.Lerp(transform.rotation, rot, Time.deltaTime * _rotationSpeed);
            }
        }
    }
}