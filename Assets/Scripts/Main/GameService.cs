using System.Collections.Generic;
using RealmRush.Events;
using RealmRush.Player;
using RealmRush.Quest;
using RealmRush.UI;
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
        public QuestManager QuestManager { get; private set; }
        
        [SerializeField]private UIService _uiService;
        public UIService UIService => _uiService;
        
        [SerializeField] private PlayerView playerView;
        [SerializeField] private List<QuestSO> quests = new List<QuestSO>();

        public void Start()
        {
            EventService = new EventService();
            QuestManager = new QuestManager(quests);
            PlayerController playerController = new PlayerController(playerView);
            _uiService.AddListensers();
        }
    }
}