using UnityEngine;

namespace RealmRush.Player
{
    public class PlayerModel
    {
        public float walkSpeed = 7f;
        public float runSpeed = 12f;
        public float jumpPower = 7f;
        public float gravity = 10f;
        public float lookSpeed = 2f;
        public float lookXLimit = 45f;
        public float damage = 25f;
        
        public Vector3 moveDirection = Vector3.zero;
        public float rotationX = 0;
        public bool canMove = true;
    }
}
