using UnityEngine;

namespace RealmRush.Quest
{
    public abstract class QuestSO : ScriptableObject
    {
        public string questName;
        public string questDescription;
        public int goalCount;
        public string rewardText;
        
        [HideInInspector] public int currentCount;
        [HideInInspector] public bool isCompleted;
        
        public abstract void Initialize();
        public abstract void OnProgress();
    }
}