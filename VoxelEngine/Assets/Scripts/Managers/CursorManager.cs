using UnityEngine;

namespace Managers
{
    public class CursorManager : MonoBehaviour
    {
        public KeyCode toggleCursorButton = KeyCode.Tab;
        public static bool IsCursorEnabled = true;

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
            IsCursorEnabled = false;
        }

        public static void CursorEnable()
        {
            Cursor.visible = true;
            Cursor.lockState = CursorLockMode.None;
            IsCursorEnabled = true;
        }

        private void ToggleCursor()
        {
            if (IsCursorEnabled)
                CursorOff();
            else
                CursorEnable();
        }
    }
}