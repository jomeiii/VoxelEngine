using UnityEngine;
using UnityEngine.EventSystems;

namespace Inventory
{
    public class Slot : MonoBehaviour, IDropHandler
    {
        public Inventory inventory;
        public int slotNumber;
        public string itemName;
        
        private void Awake()
        {
            // (stepa) TODO: переделать
            inventory = GameObject.FindGameObjectWithTag("InventoryManager").GetComponent<Inventory>();
        }

        private void Update()
        {
            itemName = inventory.items[slotNumber].name;
        }

        public void OnDrop(PointerEventData eventData)
        {
            ItemData droppedItem = eventData.pointerDrag.GetComponent<ItemData>();

            if (inventory.items[slotNumber].id == -1)
            {
                inventory.items[droppedItem.currentSlot] = inventory.database.GetItemByID(-1);
                inventory.items[slotNumber] = droppedItem.item;
                droppedItem.currentSlot = slotNumber;
            }
            else if (droppedItem.currentSlot != slotNumber)
            {
                Transform item = transform.GetChild(0);
                item.transform.parent = inventory.slots[droppedItem.currentSlot].transform;
                inventory.transform.position = inventory.slots[droppedItem.currentSlot].transform.position;

                inventory.items[droppedItem.currentSlot] = item.GetComponent<ItemData>().item;
                inventory.items[slotNumber] = droppedItem.item;

                droppedItem.currentSlot = slotNumber;
                droppedItem.transform.parent = transform;
                droppedItem.transform.position = transform.position;
            }
        }
    }
}