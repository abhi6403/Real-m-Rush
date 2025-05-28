using RealmRush.Enemy;
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
            Shooting();
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
            
            if(Input.GetButtonDown("Jump") && _playerView.characterController.isGrounded)
            {
                _playerModel.moveDirection.y = _playerModel.jumpPower;
            }
            
            if (!_playerView.characterController.isGrounded)
            {
                _playerModel.moveDirection.y -= _playerModel.gravity * Time.deltaTime;
            }
            
            _playerView.characterController.Move(_playerModel.moveDirection * Time.deltaTime);
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
                RaycastHit hit;

                if (Physics.Raycast(_playerView.playerCamera.transform.position,
                        _playerView.playerCamera.transform.TransformDirection(Vector3.forward), out hit,
                        Mathf.Infinity))
                {
                    EnemyController enemy = hit.transform.GetComponent<EnemyController>();

                    _playerView.PlayFireEffect();
                    GameObject tempHit = _playerView.PlayHitEffect(hit.point);

                    if (enemy != null)
                    {
                        enemy.TakeDamage(_playerModel.damage);
                    }

                    _playerView.DestroyHitEffect(tempHit);
                }
            }
        }
    }
}