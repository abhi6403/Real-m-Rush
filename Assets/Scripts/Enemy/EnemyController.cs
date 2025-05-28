using RealmRush.Main;
using UnityEngine;

namespace RealmRush.Enemy
{
    public class EnemyController : MonoBehaviour
    {
        private float _Health = 100f;

        public void TakeDamage(float damage)
        {
            if (_Health <= 0)
            {
                GameService.Instance.EventService.OnKilled.InvokeEvent();
                Destroy(gameObject);
            }
            else
            {
                _Health -= damage;
            }
        }
    }
}
