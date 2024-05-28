using UnityEngine;

namespace Player
{
    [RequireComponent(typeof(CharacterController))]
    public class CharacterMovement : MonoBehaviour
    {
        [Header("Components")] [SerializeField]
        private CharacterController _characterController;

        [Header("Settings")] [SerializeField] private float _speed = 12f;

        [Header("Jump settings")] [SerializeField]
        private float _jumpHeight = 1f;

        [SerializeField] private float _gravity = -9.81f;

        [Header("Ground check settings")] [SerializeField]
        private float _groundDistance = 0.4f;

        [SerializeField] private LayerMask _groundMask;
        [SerializeField] private bool _isGrounded;

        private Vector3 _velocity;
        private Vector3 _movement;

        private void FixedUpdate()
        {
            CheckGround();
        }

        public void Movement()
        {
            float x = Input.GetAxis("Horizontal");
            float z = Input.GetAxis("Vertical");

            _movement = transform.right * x + transform.forward * z;
            _characterController.Move(_movement * (_speed * Time.deltaTime));
        }

        public void Jump()
        {
            if (Input.GetButtonDown("Jump") && _isGrounded)
            {
                _velocity.y = Mathf.Sqrt(_jumpHeight * -2f * _gravity);
            }
        }

        public void Gravity()
        {
            if (_isGrounded && _velocity.y < 0)
                _velocity.y = -2f;
            else
                _velocity.y += _gravity * Time.deltaTime;

            _characterController.Move(_velocity * Time.deltaTime);
        }

        private void CheckGround()
        {
            _isGrounded = Physics.Raycast(transform.position, Vector3.down, _groundDistance, _groundMask);

            Debug.DrawRay(transform.position, Vector3.down * _groundDistance, _isGrounded ? Color.green : Color.red);
        }
    }
}