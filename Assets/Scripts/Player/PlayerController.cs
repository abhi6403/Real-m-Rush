using RealmRush.Enemy;
using Unity.VisualScripting;
using UnityEngine;

namespace RealmRush.Player
{
    public class PlayerController 
    {
        private PlayerView _playerView;
        private PlayerModel _playerModel;
        
        public PlayerController(PlayerView playerView)
        {
            _playerModel = new PlayerModel();
            _playerView = playerView;
            _playerView.SetPlayerController(this);
        }

        public void Update()
        {
            HandleMovement();
            CameraMovement();
            
            if (Input.GetKeyDown(KeyCode.Mouse0))
            {
                Shooting();
            }
        }

        private void HandleMovement()
        {
            Vector3 forward = _playerView.transform.forward;
            Vector3 right = _playerView.transform.right;
            
            bool isRunning = Input.GetKey(KeyCode.LeftShift);
            
            float speed = isRunning ? _playerModel.runSpeed : _playerModel.walkSpeed;
            
            float inputX = Input.GetAxis("Vertical");
            float inputY = Input.GetAxis("Horizontal");

            float groundY = _playerModel.moveDirection.y;
            
            _playerModel.moveDirection = (forward * inputX + right * inputY) * speed;
            _playerModel.moveDirection.y = groundY;
            
            if(Input.GetButtonDown("Jump") && _playerView._characterController.isGrounded)
            {
                _playerModel.moveDirection.y = _playerModel.jumpPower;
            }

            if (!_playerView._characterController.isGrounded)
            {
                _playerModel.moveDirection.y -= _playerModel.gravity * Time.deltaTime;
            }
            
            _playerView._characterController.Move(_playerModel.moveDirection * Time.deltaTime);
        }

        private void CameraMovement()
        {
            if (!_playerModel.canMove) return;

            float mouseX = Input.GetAxis("Mouse X") * _playerModel.lookSpeed;
            float mouseY = Input.GetAxis("Mouse Y") * _playerModel.lookSpeed;
            
            _playerModel.rotationX -= mouseY;
            _playerModel.rotationX = Mathf.Clamp(_playerModel.rotationX, -_playerModel.lookXLimit,_playerModel.lookXLimit);
            
            _playerView.playerCamera.transform.localRotation = Quaternion.Euler(_playerModel.rotationX, 0, 0);
            _playerView.transform.Rotate(Vector3.up * mouseX);
        }

        private void Shooting()
        {
            if (Input.GetMouseButtonDown(0))
            {
                Ray ray = _playerView.playerCamera.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0));
                if (Physics.Raycast(ray, out RaycastHit hit))
                {
                    _playerView.PlayFireEffect();
                    GameObject tempHit = _playerView.PlayHitEffect(hit.point);

                    if (hit.transform.TryGetComponent(out EnemyController enemy))
                    {
                        enemy.TakeDamage(_playerModel.damage);
                    }

                    _playerView.DestroyHitEffect(tempHit);
                }
            }
        }
    }
}