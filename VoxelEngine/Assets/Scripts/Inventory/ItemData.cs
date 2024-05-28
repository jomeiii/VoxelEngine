using UnityEngine;
using UnityEngine.EventSystems;

namespace Inventory
{
    public class ItemData : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
    {
        public Inventory inventory;

        public Item item;
        public int amount;
        public int currentSlot;

        private void Awake()
        {
            // (stepa) TODO: переделать
            inventory = GameObject.FindGameObjectWithTag("InventoryManager").GetComponent<Inventory>();
            transform.position = inventory.slots[currentSlot].transform.position;
        }

        public void OnBeginDrag(PointerEventData eventData)
        {
            if (item.id != -1)
            {
                transform.parent = transform.parent.parent.parent;
                gameObject.GetComponent<CanvasGroup>().blocksRaycasts = false;
                transform.position = eventData.position;
            }
        }

        public void OnDrag(PointerEventData eventData)
        {
            if (item.id != -1)
            {
                transform.position = eventData.position;
            }
        }

        public void OnEndDrag(PointerEventData eventData)
        {
            transform.parent = inventory.slots[currentSlot].transform;
            transform.position = inventory.slots[currentSlot].transform.position;
            gameObject.GetComponent<CanvasGroup>().blocksRaycasts = true;
        }
    }
}