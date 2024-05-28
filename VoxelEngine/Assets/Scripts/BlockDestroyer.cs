using Managers;
using UnityEngine;

public class BlockDestroyer : MonoBehaviour
{
    private void OnMouseDown()
    {
        if (!CursorManager.IsCursorEnabled)
        {
            Destroy(this.gameObject);
        }
    }
}