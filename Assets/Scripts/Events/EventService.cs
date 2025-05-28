using UnityEngine;

namespace RealmRush.Events
{
    public class EventService
    {
        public EventController OnItemCollected { get; private set; }
        public EventController OnKilled { get; private set; }
        public EventController OnExplored { get; private set; }

        public EventService()
        {
            OnItemCollected = new EventController();
            OnKilled = new EventController();
            OnExplored = new EventController();
        }
    }
}
