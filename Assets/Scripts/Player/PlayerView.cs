using RealmRush.Main;
using UnityEngine;

namespace RealmRush.Player
{
    public class PlayerView : MonoBehaviour
    {
        [SerializeField] private Transform fireTransform;
        
        [SerializeField] private ParticleSystem fireParticleEffect;
        [SerializeField] private GameObject hitParticleEffect;
        [SerializeField] private int deathDistance;
        
        public Camera playerCamera;
        
        private PlayerController _playerController;
        private int currentPillarNumber;
        public CharacterController characterController { get; private set; }

        void Start()
        {
            characterController = GetComponent<CharacterController>();
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
            AddSubscribers();
            GameService.Instance.EventService.OnGameStarted.InvokeEvent();
        }

        private void AddSubscribers()
        {
            GameService.Instance.EventService.SetPlayerPosition.AddListener(SetPlayerPosition);
        }

        private void SetPlayerPosition(Vector3 position)
        {
            Vector3 pos = new Vector3(position.x, position.y + 8, position.z);
            
            transform.position = pos;
        }

        void Update()
        {
            _playerController.Update();
            KillPlayer();
        }
        
        public void PlayFireEffect() => fireParticleEffect.Play();

        public void SetPlayerController(PlayerController playerController)
        {
            _playerController = playerController;
        }
        
        public GameObject PlayHitEffect(Vector3 hitPosition)
        { 
            return Instantiate(hitParticleEffect, hitPosition, hitParticleEffect.transform.rotation);
        }

        public void DestroyHitEffect(GameObject hitParticleEffect) => Destroy(hitParticleEffect,2f);

        private void KillPlayer()
        {
            if (transform.position.y
                <= deathDistance)
            {
                Destroy(gameObject, 1f); 
            }
        }
        public PlayerController GetPlayerController()
        {
            return _playerController;
        }

        public void SetCurrentPillarNumber(int currentPillarNumber)
        {
            this.currentPillarNumber = currentPillarNumber;
        }

        public int GetCurrentPillarNumber()
        {
            return currentPillarNumber;
        }
    }
}