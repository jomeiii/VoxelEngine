using UnityEngine;

namespace Managers
{
    public class CursorManager : MonoBehaviour
    {
        public KeyCode toggleCursorButton = KeyCode.Tab;
        private static bool _isCursorEnabled = true;

        private void Start()
        {
            CursorOff();
        }

        private void Update()
        {
            if (Input.GetKeyDown(toggleCursorButton)) ToggleCursor();
        }

        public static void CursorOff()
        {
            Cursor.visible = false;
            Cursor.lockState = CursorLockMode.Locked;
            _isCursorEnabled = false;
        }

        public static void CursorEnable()
        {
            Cursor.visible = true;
            Cursor.lockState = CursorLockMode.None;
            _isCursorEnabled = true;
        }

        private void ToggleCursor()
        {
            if (_isCursorEnabled)
                CursorOff();
            else
                CursorEnable();
        }
    }
}