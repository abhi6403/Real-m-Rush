using RealmRush.Enemy;
using UnityEngine;

namespace RealmRush.Player
{
    public class PlayerController : MonoBehaviour
    {
        [SerializeField] private Transform _fireTransform;
        [SerializeField] private GameObject _fireParticle;
        [SerializeField] private GameObject _hitParticle;
        [SerializeField] private Camera playerCamera;
        [SerializeField] private float walkSpeed = 6f;
        [SerializeField] private float runSpeed = 12f;
        [SerializeField] private float jumpPower = 7f;
        [SerializeField] private float gravity = 10f;
        [SerializeField] private float lookSpeed = 2f;
        [SerializeField] private float lookXLimit = 45f;

        private Vector3 moveDirection = Vector3.zero;
        private float rotationX = 0;
        private float damage = 25f;
        private CharacterController characterController;

        private bool canMove = true;

        void Start()
        {
            characterController = GetComponent<CharacterController>();
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
        }

        void Update()
        {
            Movement();
            MouseRotation();
            if (Input.GetKeyDown(KeyCode.Mouse0))
            {
                Shooting();
            }
        }

        private void Movement()
        {
            Vector3 forward = transform.TransformDirection(Vector3.forward);
            Vector3 right = transform.TransformDirection(Vector3.right);

            bool isRunning = Input.GetKey(KeyCode.LeftShift);
            
            float curSpeedX = canMove ? (isRunning ? runSpeed : walkSpeed) * Input.GetAxis("Vertical") : 0;
            float curSpeedY = canMove ? (isRunning ? runSpeed : walkSpeed) * Input.GetAxis("Horizontal") : 0;
            
            float movementDirectionY = moveDirection.y;
            
            moveDirection = (forward * curSpeedX) + (right * curSpeedY);
            
            if (Input.GetButton("Jump") && canMove && characterController.isGrounded)
            {
                moveDirection.y = jumpPower;
            }
            else
            {
                moveDirection.y = movementDirectionY;
            }

            if (!characterController.isGrounded)
            {
                moveDirection.y -= gravity * Time.deltaTime;
            }
            
            characterController.Move(moveDirection * Time.deltaTime);
        }

        private void MouseRotation()
        {
            if (canMove)
            {
                rotationX += -Input.GetAxis("Mouse Y") * lookSpeed;
                rotationX = Mathf.Clamp(rotationX, -lookXLimit, lookXLimit);
                playerCamera.transform.localRotation = Quaternion.Euler(rotationX, 0, 0);
                transform.rotation *= Quaternion.Euler(0, Input.GetAxis("Mouse X") * lookSpeed, 0);
            }
        }

        private void Shooting()
        {
            RaycastHit hit;
            
            if (Physics.Raycast(playerCamera.transform.position, playerCamera.transform.TransformDirection(Vector3.forward), out hit,
                    Mathf.Infinity))
            {
                Debug.DrawRay(playerCamera.transform.position, transform.TransformDirection(Vector3.forward) * hit.distance, Color.yellow);
                EnemyController enemy = hit.transform.GetComponent<EnemyController>();

                if (enemy != null)
                {
                    enemy.TakeDamage(damage);
                }
                Instantiate(_fireParticle,_fireTransform.position, Quaternion.identity);
                Instantiate(_hitParticle, hit.point, Quaternion.identity);
                
                
            }
        }
    }
}