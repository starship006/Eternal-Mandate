using UnityEngine;
using UnityEngine.Events;

namespace Assets.Game.Scripts.Player_Systems {
    public class UserInput : MonoBehaviour {
        [Header("Key Inputs")]
        [SerializeField] private KeyCode _jumpKey = KeyCode.Space;

        [Header("Axis Inputs")]
        [SerializeField] private string _moveForward = "Vertical";
        [SerializeField] private string _moveRight = "Horizontal";
        [SerializeField] private string _lookUp = "Mouse X";
        [SerializeField] private string _lookRight = "Mouse Y";

        [Header("Events")]
        [SerializeField] private UnityEvent<Vector2> _moveInput = new UnityEvent<Vector2>();
        [SerializeField] private UnityEvent<Vector2> _lookInput = new UnityEvent<Vector2>();
        [SerializeField] private UnityEvent _jumpInput = new UnityEvent();

        private void Update()
        {
            Vector2 moveDir = new Vector2(Input.GetAxis(_moveRight), Input.GetAxis(_moveForward));
            _moveInput?.Invoke(moveDir);

            Vector2 lookDir = new Vector2(Input.GetAxis(_lookUp), Input.GetAxis(_lookRight));
            _lookInput?.Invoke(lookDir);

            if (Input.GetKeyDown(_jumpKey))
            {
                _jumpInput?.Invoke();
            }
        }
    }
}
