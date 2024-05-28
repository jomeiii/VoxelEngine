using Managers;
using UnityEngine;

namespace Player
{
    public class CameraController : MonoBehaviour
    {
        [Tooltip("Чувствительность мыши")]
        public float sensitivity = 2.0f;

        [Tooltip("Максимальный угол вращения по вертикали")]
        public float maxYAngle = 80.0f;

        public bool isCameraLocked;

        private float _rotationX = 0.0f;

        private void OnEnable()
        {
            PauseManager.PauseEvent += LockCamera;
            PauseManager.ContinueEvent += UnlockCamera;
        }

        private void OnDisable()
        {
            PauseManager.PauseEvent -= LockCamera;
            PauseManager.ContinueEvent -= UnlockCamera;
        }

        private void Update()
        {
            if (!isCameraLocked)
            {
                RotateCamera();
            }
        }

        private void RotateCamera()
        {
            float mouseX = Input.GetAxis("Mouse X");
            float mouseY = Input.GetAxis("Mouse Y");

            transform.parent.Rotate(Vector3.up, mouseX * sensitivity);

            _rotationX -= mouseY * sensitivity;
            _rotationX = Mathf.Clamp(_rotationX, -maxYAngle, maxYAngle);
            transform.localRotation = Quaternion.Euler(_rotationX, 0.0f, 0.0f);
        }

        private void LockCamera()
        {
            isCameraLocked = true;
        }

        private void UnlockCamera()
        {
            isCameraLocked = false;
        }
    }
}