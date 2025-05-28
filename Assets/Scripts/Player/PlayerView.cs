using UnityEngine;

namespace RealmRush.Player
{
    public class PlayerView : MonoBehaviour
    {
        [SerializeField] private Transform fireTransform;
        
        [SerializeField] private ParticleSystem fireParticleEffect;
        [SerializeField] private GameObject hitParticleEffect;
        
        public Camera playerCamera;
        
        private PlayerController _playerController;
        public CharacterController _characterController { get; private set; }

        void Start()
        {
            _characterController = GetComponent<CharacterController>();
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
        }

        void Update()
        {
            _playerController.Update();
        }

        public void SetPlayerController(PlayerController playerController)
        {
            _playerController = playerController;
        }
        
        public void PlayFireEffect()
        {
            fireParticleEffect.Play();
        }

        public GameObject PlayHitEffect(Vector3 hitPosition)
        {
                return Instantiate(hitParticleEffect, hitPosition, hitParticleEffect.transform.rotation);
        }

        public void DestroyHitEffect(GameObject hitParticleEffect)
        {
            Destroy(hitParticleEffect,2f);
        }
    }
}
