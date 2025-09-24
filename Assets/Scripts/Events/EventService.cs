using RealmRush.Quest;
using UnityEngine;

namespace RealmRush.Events
{
    public class EventService
    {
        public EventController OnItemCollected { get; private set; }
        public EventController OnKilled { get; private set; }
        public EventController OnExplored { get; private set; }
        public EventController OnGameCompleted { get; private set; }
        public EventController<Vector3> SetPlayerPosition { get; private set; }
        public EventController OnGameStarted { get; private set; }
        
        public EventController<QuestSO> OnFetchQuest { get; private set; }
        public EventController<QuestSO> OnKillQuest { get; private set; }
        public EventController<QuestSO> OnExplore { get; private set; }

        public EventService()
        {
            SetPlayerPosition = new EventController<Vector3>();
            OnGameStarted = new EventController();
            OnItemCollected = new EventController();
            OnKilled = new EventController();
            OnExplored = new EventController();
            OnGameCompleted = new EventController();
            OnFetchQuest = new EventController<QuestSO>();
            OnKillQuest = new EventController<QuestSO>();
            OnExplore = new EventController<QuestSO>();
        }
    }
}