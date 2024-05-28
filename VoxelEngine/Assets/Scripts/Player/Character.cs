using UnityEngine;
using UnityEngine.Serialization;

namespace Player
{
    [RequireComponent(typeof(CharacterMovement))]
    public class Character : MonoBehaviour
    {
        [Header("Components")] [SerializeField]
        private CharacterMovement _characterMovement;

        [SerializeField] private Camera _camera;
        [SerializeField] private float _maxRange = 7f;
        [SerializeField] private float _rayDirection = 1f;

        private void Awake()
        {
            if (_characterMovement == null)
            {
                // (stepa) TODO: Переделать под отдельный класс для дебага
                _characterMovement = GetComponent<CharacterMovement>();
            }

            if (_camera == null)
            {
                // (stepa) TODO: Переделать под отдельный класс для дебага
                _camera = GetComponentInChildren<Camera>();
            }
        }

        private void Update()
        {
            _characterMovement.Movement();
            _characterMovement.Jump();
            _characterMovement.Gravity();

            if (Input.GetMouseButtonDown(0))
            {
                Ray ray = new Ray(_camera.transform.position, _camera.transform.forward);
                Block block;

                if (Physics.Raycast(ray, out RaycastHit hit, _maxRange))
                {
                    Debug.DrawRay(_camera.transform.position, _camera.transform.forward * _maxRange, Color.green,
                        _rayDirection);
                    if (hit.transform.TryGetComponent(out block))
                    {
                        block.BreakBlock();
                    }
                }
                else
                {
                    Debug.DrawRay(_camera.transform.position, _camera.transform.forward * _maxRange, Color.red,
                        _rayDirection);
                }
            }
        }
    }
}