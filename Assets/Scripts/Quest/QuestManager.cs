using System.Collections.Generic;
using System.Linq;
using RealmRush.Main;
using UnityEngine;

namespace RealmRush.Quest
{
    public class QuestManager
    {
        public List<QuestSO> _activeQuests;
        public QuestManager(List<QuestSO> activeQuests)
        {
            _activeQuests = activeQuests;
            
            foreach (QuestSO quest in activeQuests)
            {
                quest.Initialize();
            }
            
            AddListeners();
        }

        private void AddListeners()
        {
            GameService.Instance.EventService.OnItemCollected.AddListener(HandleItemCollected);
            GameService.Instance.EventService.OnKilled.AddListener(HandleEnemyKilled);
            GameService.Instance.EventService.OnExplored.AddListener(HandleAreaExplored);
        }

        ~QuestManager()
        {
            GameService.Instance.EventService.OnItemCollected.RemoveListener(HandleItemCollected);
            GameService.Instance.EventService.OnKilled.RemoveListener(HandleEnemyKilled);
            GameService.Instance.EventService.OnExplored.RemoveListener(HandleAreaExplored);
        }
        
        private void HandleItemCollected()
        {
            foreach (var quest in _activeQuests.OfType<FetchQuestSO>())
            {
                if (!quest.isCompleted) quest.OnProgress();
            }

            CheckForCompletion();
        }

        private void HandleEnemyKilled()
        {
            foreach (var quest in _activeQuests.OfType<KillQuestSO>())
            {
                if (!quest.isCompleted) quest.OnProgress();
            }

            CheckForCompletion();
        }

        private void HandleAreaExplored()
        {
            foreach (var quest in _activeQuests.OfType<ExploreQuestSO>())
            {
                if (!quest.isCompleted) quest.OnProgress();
            }

            CheckForCompletion();
        }

        private void CheckForCompletion()
        {
            bool allComplete = _activeQuests.All(q => q.isCompleted);
            
            if (allComplete)
            {
                Debug.Log("All quests completed!");
                GameService.Instance.EventService.OnGameCompleted.InvokeEvent();
                Time.timeScale = 0;
            }
        }
    }
}