using RealmRush.Main;
using RealmRush.Player;
using RealmRush.Quest;
using UnityEngine;

namespace RealmRush.Interactions
{
    public class CollectibleItem : MonoBehaviour
    {
        private void OnTriggerEnter(Collider other)
        {
            if (other.GetComponent<PlayerView>())
            {
                GameService.Instance.EventService.OnItemCollected.InvokeEvent();
                Destroy(gameObject);
            }
        }
    }
}
