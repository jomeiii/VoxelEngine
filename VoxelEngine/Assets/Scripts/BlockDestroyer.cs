using Managers;
using UnityEngine;

public class BlockDestroyer : MonoBehaviour
{
    private Inventory.Inventory _inventory;
    public int id;

    private void Awake()
    {
        _inventory = GameObject.FindGameObjectWithTag("InventoryManager").GetComponent<Inventory.Inventory>();
    }

    private void OnMouseDown()
    {
        if (!CursorManager.IsCursorEnabled)
        {
            _inventory.AddItem(id);
            Destroy(this.gameObject);
        }
    }
}