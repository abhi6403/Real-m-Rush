using UnityEngine;

namespace RealmRush.Quest
{
    [CreateAssetMenu(fileName = "New Kill Quest", menuName = "Quests/Kill Quest")]
    
    public class KillQuestSO : QuestSO
    {
        public override void Initialize()
        {
            currentCount = 0;
            isCompleted = false;
        }

        public override void OnProgress()
        {
            if (isCompleted) return;

            currentCount++;
            if (currentCount >= goalCount)
            {
                CompleteQuest();
            }
        }

        private void CompleteQuest()
        {
            isCompleted = true;
            Debug.Log($"Kill Quest Completed");
        }
    }
}
