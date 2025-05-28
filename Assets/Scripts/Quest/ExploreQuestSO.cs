using UnityEngine;

namespace RealmRush.Quest
{
    [CreateAssetMenu(fileName = "New Explore Quest", menuName = "Quests/Explore Quest")]
    public class ExploreQuestSO : QuestSO
    {
        public override void Initialize()
        {
            currentCount = 0;
            isCompleted = false;
        }

        public override void OnProgress()
        {
            if (isCompleted) return;

            currentCount = 1; 
            CompleteQuest();
        }

        private void CompleteQuest()
        {
            isCompleted = true;
            Debug.Log($"Explore Quest Completed");
        }
    }
}
