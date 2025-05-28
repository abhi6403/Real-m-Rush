using RealmRush.Main;
using UnityEngine;

namespace RealmRush.Quest
{
    [CreateAssetMenu(fileName = "New Fetch Quest", menuName = "Quests/Fetch Quest")]
    
    public class FetchQuestSO : QuestSO
    {
        public override void Initialize()
        {
            currentCount = 0;
            isCompleted = false;
            GameService.Instance.EventService.OnFetchQuest.InvokeEvent(this);
        }

        public override void OnProgress()
        {
            if(isCompleted) return;
            currentCount++;

            if (currentCount >= goalCount)
            {
                CompleteQuest();
            }
            GameService.Instance.EventService.OnFetchQuest.InvokeEvent(this);
        }

        private void CompleteQuest()
        {
            isCompleted = true;
            Debug.Log("Quest complete");
        }
    }
}
