using RealmRush.Main;
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
            GameService.Instance.EventService.OnKillQuest.InvokeEvent(this);
        }

        public override void OnProgress()
        {
            if (isCompleted) return;

            currentCount++;
            if (currentCount >= goalCount)
            {
                CompleteQuest();
            }
            GameService.Instance.EventService.OnKillQuest.InvokeEvent(this);
        }

        private void CompleteQuest()
        {
            isCompleted = true;
            Debug.Log("Kill Quest Completed");
        }
    }
}