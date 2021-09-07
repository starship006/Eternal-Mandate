using UnityEngine;
using UnityEngine.Events;

namespace Assets.Game.Scripts.Player_Systems {
    public class UserInputMouse : MonoBehaviour {
        [Header("Axis Inputs")]
        [SerializeField] private string _lookUp = "Mouse X";
        [SerializeField] private string _lookRight = "Mouse Y";

        [Header("Events")]
        [SerializeField] private UnityEvent<Vector2> _lookInput = new UnityEvent<Vector2>();

        private void Update()
        {
            Vector2 lookDir = new Vector2(Input.GetAxis(_lookUp), Input.GetAxis(_lookRight));
            _lookInput?.Invoke(lookDir);
        }
    }
}