using Managers;
using UnityEngine;

public class Block : MonoBehaviour
{
    private Inventory.Inventory _inventory;
    public int id;

    private void Awake()
    {
        _inventory = GameObject.FindGameObjectWithTag("InventoryManager").GetComponent<Inventory.Inventory>();
    }

    public void BreakBlock()
    {
        if (!CursorManager.IsCursorEnabled)
        {
            _inventory.AddItem(id);
            Destroy(this.gameObject);
        }
    }
}