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
            
            // (stepa) TODO: Переделать под отдельный класс для дебага
        }

        public static void CursorEnable()
        {
            Cursor.visible = true;
            Cursor.lockState = CursorLockMode.None;
            IsCursorEnabled = true;
            
            // (stepa) TODO: Переделать под отдельный класс для дебага
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