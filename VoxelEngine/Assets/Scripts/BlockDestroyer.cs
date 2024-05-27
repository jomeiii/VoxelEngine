using UnityEngine;

public class BlockDestroyer : MonoBehaviour
{
    private void OnMouseDown()
    {
        Destroy(this.gameObject);
    }
}