using System;
using System.Collections.Generic;
using System.Linq;
using RealmRush.Main;
using UnityEngine;

namespace RealmRush.Quest
{
    public class QuestManager : MonoBehaviour
    {
        public List<QuestSO> activeQuests = new List<QuestSO>();

        private void Start()
        {
            foreach (QuestSO quest in activeQuests)
            {
                quest.Initialize();
            }
        }

        public void AddListeners()
        {
            GameService.Instance.EventService.OnItemCollected.AddListener(HandleItemCollected);
            GameService.Instance.EventService.OnKilled.AddListener(HandleEnemyKilled);
            GameService.Instance.EventService.OnExplored.AddListener(HandleAreaExplored);
        }
        
        private void HandleItemCollected()
        {
            foreach (var quest in activeQuests.OfType<FetchQuestSO>())
            {
                if (!quest.isCompleted) quest.OnProgress();
            }

            CheckForCompletion();
        }

        private void HandleEnemyKilled()
        {
            foreach (var quest in activeQuests.OfType<KillQuestSO>())
            {
                if (!quest.isCompleted) quest.OnProgress();
            }

            CheckForCompletion();
        }

        private void HandleAreaExplored()
        {
            foreach (var quest in activeQuests.OfType<ExploreQuestSO>())
            {
                if (!quest.isCompleted) quest.OnProgress();
            }

            CheckForCompletion();
        }

        private void CheckForCompletion()
        {
            bool allComplete = activeQuests.All(q => q.isCompleted);
            if (allComplete)
            {
                Debug.Log("🎉 All quests completed!");
            }
        }
    }
}
