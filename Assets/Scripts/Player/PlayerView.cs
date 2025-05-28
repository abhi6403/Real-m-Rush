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
        public CharacterController characterController { get; private set; }

        void Start()
        {
            characterController = GetComponent<CharacterController>();
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
        }

        void Update() => _playerController.Update();
        
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
    }
}