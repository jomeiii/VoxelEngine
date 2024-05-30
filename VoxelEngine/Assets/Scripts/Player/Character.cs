using UnityEngine;
using UnityEngine.Serialization;

namespace Player
{
    [RequireComponent(typeof(CharacterMovement))]
    public class Character : MonoBehaviour
    {
        [Header("Components")]
        [SerializeField] private CharacterMovement _characterMovement;
        [SerializeField] private Inventory.Inventory _inventory;
        [SerializeField] private Camera _camera;
        [FormerlySerializedAs("_playerPosition")] [SerializeField] private Transform _playerTransform;
        
        [Header("Settings")]
        [SerializeField] private float _maxRange = 7f;
        [SerializeField] private float _rayDuration = 1f;

        private void Awake()
        {
            InitializeComponents();
        }

        private void InitializeComponents()
        {
            _characterMovement ??= GetComponent<CharacterMovement>();
            _camera ??= GetComponentInChildren<Camera>();
            _inventory ??= GameObject.FindGameObjectWithTag("InventoryManager").GetComponent<Inventory.Inventory>();
            _playerTransform ??= GetComponent<Transform>();
        }

        private void Update()
        {
            HandleMovement();
            HandleMouseInput();
        }

        private void HandleMovement()
        {
            _characterMovement.Movement();
            _characterMovement.Jump();
            _characterMovement.Gravity();
        }

        private void HandleMouseInput()
        {
            if (Input.GetMouseButtonDown(0))
            {
                HandleLeftClick();
            }

            if (Input.GetMouseButtonDown(1))
            {
                HandleRightClick();
            }
        }

        private void HandleLeftClick()
        {
            Ray ray = new Ray(_camera.transform.position, _camera.transform.forward);

            if (Physics.Raycast(ray, out RaycastHit hit, _maxRange))
            {
                Debug.DrawRay(_camera.transform.position, _camera.transform.forward * _maxRange, Color.green, _rayDuration);
                if (hit.transform.TryGetComponent(out Block block))
                {
                    block.BreakBlock();
                }
            }
            else
            {
                Debug.DrawRay(_camera.transform.position, _camera.transform.forward * _maxRange, Color.red, _rayDuration);
            }
        }

        private void HandleRightClick()
        {
            if (!_inventory.items[_inventory.hoverIndex].placeable) return;

            Ray ray = new Ray(_camera.transform.position, _camera.transform.forward);

            if (Physics.Raycast(ray, out RaycastHit hit, _maxRange))
            {
                Vector3 spawnPosition = GetSpawnPosition(hit);

                if (CheckBlockCollision(spawnPosition, _playerTransform)) return;
                
                Instantiate(_inventory.items[_inventory.hoverIndex].gameObject, spawnPosition, Quaternion.identity);
                _inventory.RemoveItem();

                Debug.DrawRay(_camera.transform.position, _camera.transform.forward * _maxRange, Color.green, _rayDuration);
            }
            else
            {
                Debug.DrawRay(_camera.transform.position, _camera.transform.forward * _maxRange, Color.red, _rayDuration);
            }
        }

        private Vector3 GetSpawnPosition(RaycastHit hit)
        {
            Vector3 spawnPosition = hit.point;
            Vector3 normal = hit.normal;

            spawnPosition += normal * 0.5f;

            spawnPosition.x = Mathf.Round(spawnPosition.x);
            spawnPosition.y = Mathf.Round(spawnPosition.y);
            spawnPosition.z = Mathf.Round(spawnPosition.z);

            return spawnPosition;
        }

        private bool CheckBlockCollision(Vector3 blockPosition, Transform playerTransform)
        {
            Collider[] hitColliders = Physics.OverlapBox(blockPosition, Vector3.one * 0.5f, Quaternion.identity);
            foreach (var hitCollider in hitColliders)
            {
                if (hitCollider.transform == playerTransform)
                {
                    return true;
                }
            }
            return false;
        }
    }
}
