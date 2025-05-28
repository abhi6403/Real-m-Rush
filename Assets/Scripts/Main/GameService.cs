using System.Collections.Generic;
using RealmRush.Events;
using RealmRush.Player;
using RealmRush.Quest;
using RealmRush.UI;
using RealmRush.Utilities;
using UnityEngine;

namespace RealmRush.Main
{
    public class GameService : GenericMonoSingleton<GameService>
    {
        public EventService EventService { get; private set; }
        public QuestManager QuestManager { get; private set; }
        public PlayerService PlayerService { get; private set; }
        
        [SerializeField]private UIService _uiService;
        public UIService UIService => _uiService;
        
        [SerializeField] private PlayerView playerView;
        [SerializeField] private List<QuestSO> quests = new List<QuestSO>();

        public void Start()
        {
            EventService = new EventService();
            QuestManager = new QuestManager(quests);
            PlayerService = new PlayerService(playerView);
            _uiService.Initialize();
        }
    }
}