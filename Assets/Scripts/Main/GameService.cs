using RealmRush.Events;
using RealmRush.Quest;
using UnityEngine;

namespace RealmRush.Main
{
    public class GameService : MonoBehaviour
    {
        private static GameService _instance;

        public static GameService Instance
        {
            get { return _instance; }
        }

        protected virtual void Awake()
        {
            if (_instance == null)
            {
                _instance = this;
            }
            else
            {
                Destroy(gameObject);
            }
        }
        public EventService EventService { get; private set; }
        public QuestManager QuestManager;

        public void Start()
        {
            EventService = new EventService();
            QuestManager.AddListeners();
        }
    }
}