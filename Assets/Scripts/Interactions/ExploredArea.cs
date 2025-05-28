using RealmRush.Main;
using RealmRush.Player;
using UnityEngine;

namespace RealmRush.Interactions
{
    public class ExploredArea : MonoBehaviour
    {
        private void OnTriggerEnter(Collider other)
        {
            if (other.GetComponent<PlayerController>())
            {
                GameService.Instance.EventService.OnExplored.InvokeEvent();
                gameObject.SetActive(false);
            }
        }
    }
}
