using System.Collections.Generic;
using Managers;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Vector3 = UnityEngine.Vector3;

namespace Inventory
{
    public class Inventory : MonoBehaviour
    {
        public ItemDatabase database;

        public GameObject slot;
        public GameObject item;
        public GameObject hotbarPanel;
        public GameObject inventoryPanel;

        public KeyCode inventoryButton = KeyCode.E;

        public List<GameObject> slots = new();
        public List<Item> items = new();

        [SerializeField] private int _maxAmount = 64;
        [SerializeField] private int _slotAmount = 9;
        [SerializeField] private int _storageAmount = 36;

        private void Start()
        {
            if (database == null)
            {
                // (stepa) TODO: переделать дебаг
                database = GetComponent<ItemDatabase>();
            }

            for (int i = 0; i < _slotAmount; i++)
            {
                items.Add(database.GetItemByID(-1));
                slots.Add(Instantiate(slot));
                slots[i].GetComponent<Slot>().slotNumber = i;
                slots[i].transform.parent = hotbarPanel.transform;
                slots[i].GetComponent<RectTransform>().localScale = Vector3.one;
            }

            for (int i = _slotAmount; i < _storageAmount; i++)
            {
                items.Add(database.GetItemByID(-1));
                slots.Add(Instantiate(slot));
                slots[i].GetComponent<Slot>().slotNumber = i;
                slots[i].transform.parent = inventoryPanel.transform;
                slots[i].GetComponent<RectTransform>().localScale = Vector3.one;
            }

            AddItem(1);
            AddItem(1);
            AddItem(1);
            AddItem(1);
            AddItem(1);
            AddItem(1);
            AddItem(1);
            AddItem(2);
        }

        private void Update()
        {
            if (Input.GetKeyDown(inventoryButton))
            {
                ToggleInventory();
            }
        }

        public void AddItem(int id)
        {
            var itemToAdd = database.GetItemByID(id);
            if (itemToAdd.stackable && CheckInventory(itemToAdd))
            {
                for (int i = 0; i < items.Count; i++)
                {
                    if (items[i].id == id)
                    {
                        if (slots[i].transform.GetChild(0).GetComponent<ItemData>().amount < _maxAmount)
                        {
                            ItemData data = slots[i].transform.GetChild(0).GetComponent<ItemData>();
                            data.amount++;
                            data.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = data.amount.ToString();
                        }
                    }
                }
            }
            else
            {
                for (int i = 0; i < items.Count; i++)
                {
                    if (items[i].id == -1)
                    {
                        items[i] = itemToAdd;
                        GameObject itemObject = Instantiate(item);

                        var itemData = itemObject.GetComponent<ItemData>();
                        itemData.item = itemToAdd;
                        itemData.currentSlot = i;

                        itemObject.transform.parent = slots[i].transform;
                        itemObject.transform.localPosition = Vector3.zero;
                        itemObject.name = itemToAdd.name;
                        itemObject.transform.localScale = Vector3.one;
                        itemObject.GetComponent<Image>().sprite = itemToAdd.sprite;
                        break;
                    }
                }
            }
        }

        private bool CheckInventory(Item item)
        {
            for (int i = 0; i < items.Count; i++)
                if (items[i].id == item.id)
                {
                    if (slots[i].transform.GetChild(0).GetComponent<ItemData>().amount < _maxAmount)
                    {
                        return true;
                    }
                }

            return false;
        }

        private void ToggleInventory()
        {
            inventoryPanel.SetActive(!inventoryPanel.activeSelf);
            if (inventoryPanel.activeSelf)
            {
                CursorManager.CursorEnable();
                PauseManager.OnPauseEvent();
            }
            else
            {
                CursorManager.CursorOff();
                PauseManager.OnContinueEvent();
            }
        }
    }
}