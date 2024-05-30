using UnityEngine;

namespace Player
{
    public class PlayerInputs : MonoBehaviour
    {
        public float itemIndex;
        
        [SerializeField] private Inventory.Inventory _inventory;
        [SerializeField] private Character _character;

        private void Awake()
        {
            if (_inventory == null)
            {
                if (_inventory == null)
                    _inventory = GameObject.FindGameObjectWithTag("InventoryManager")
                        .GetComponent<Inventory.Inventory>();

                Dynamic.DynamicDebug.DebugError(nameof(PlayerInputs), nameof(Awake), "Inventory не выбран");
            }

            if (_character == null)
            {
                _character = GetComponent<Character>();
                Dynamic.DynamicDebug.DebugError(nameof(PlayerInputs), nameof(Awake), "Character не выбран");
            }
        }

        private void Update()
        {
            CheckKeys();
        }

        private void CheckKeys()
        {
            if (Input.GetKeyDown(KeyCode.Alpha0))
                itemIndex = 0;
            else if (Input.GetKeyDown(KeyCode.Alpha1))
                itemIndex = 1;
            else if (Input.GetKeyDown(KeyCode.Alpha2))
                itemIndex = 2;
            else if (Input.GetKeyDown(KeyCode.Alpha3))
                itemIndex = 3;
            else if (Input.GetKeyDown(KeyCode.Alpha4))
                itemIndex = 4;
            else if (Input.GetKeyDown(KeyCode.Alpha5))
                itemIndex = 5;
            else if (Input.GetKeyDown(KeyCode.Alpha6))
                itemIndex = 6;
            else if (Input.GetKeyDown(KeyCode.Alpha7))
                itemIndex = 7;
            else if (Input.GetKeyDown(KeyCode.Alpha8))
                itemIndex = 8;
            else if (Input.GetKeyDown(KeyCode.Alpha9))
                itemIndex = 9;

            itemIndex -= Input.GetAxis("Mouse ScrollWheel") * 5;
            if (itemIndex < 0)
                itemIndex = 8;
            else if (itemIndex > 8)
                itemIndex = 0;

            _inventory.hoverIndex = (int)itemIndex;
        }
    }
}