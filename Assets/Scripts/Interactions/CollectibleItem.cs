using RealmRush.Player;
using RealmRush.Quest;
using UnityEngine;

namespace RealmRush.Interactions
{
    public class CollectibleItem : MonoBehaviour
    {
        private void OnTriggerEnter(Collider other)
        {
            if (other.GetComponent<PlayerController>())
            {
                Destroy(gameObject);
            }
        }
    }
}
