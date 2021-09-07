using UnityEngine;
using UnityEngine.Serialization;

namespace Assets.Game.Scripts.Player_Systems {
    public class ThirdPersonFollow : MonoBehaviour
    {
        [Header("References")]
        [SerializeField] private Transform _followTransform;

        [Header("Main Settings")]
        [SerializeField] private float _mouseSensitivity = 0.5f;
        [SerializeField] private Vector3 _mPositionOffset = new Vector3(0, 2, -2.5f);
        [SerializeField] private Vector3 _mAngleOffset = new Vector3(0, 0, 0);
        [SerializeField] private float _mDamping = 4;

        [Header("Up Down Rotation")]
        [SerializeField] private float _mMinPitch = -30.0f;
        [SerializeField] private float _mMaxPitch = 30.0f;
        private float _angleX;

        private Vector2 _lookDir;

        private void Start()
        {
            Cursor.lockState = CursorLockMode.Locked;
        }

        private void LateUpdate()
        {
            Quaternion offsetRotation = Quaternion.Euler(_mAngleOffset);

            _angleX -= _lookDir.y;
            _angleX = Mathf.Clamp(_angleX, _mMinPitch, _mMaxPitch);

            Quaternion desiredRotation = _followTransform.rotation * Quaternion.Euler(_angleX, 0, 0) * offsetRotation;

            Quaternion rot = Quaternion.Lerp(transform.rotation, desiredRotation, Time.deltaTime * _mDamping);
            //Quaternion rot = Quaternion.RotateTowards(transform.rotation, initialRotation, _mDamping * Time.deltaTime);

            transform.rotation = rot;

            Vector3 forward = rot * Vector3.forward * _mPositionOffset.z;
            Vector3 right = rot * Vector3.right * _mPositionOffset.x;
            Vector3 up = rot * Vector3.up * _mPositionOffset.y;

            Vector3 desiredPosition = _followTransform.position + forward + right + up;

            transform.position = Vector3.Lerp(transform.position, desiredPosition, Time.deltaTime * _mDamping);
        }

        public void SetMouseInput(Vector2 lookDir)
        {
            _lookDir = lookDir * _mouseSensitivity;
        }
    }
}
