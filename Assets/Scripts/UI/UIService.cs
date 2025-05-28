using RealmRush.Main;
using RealmRush.Quest;
using TMPro;
using UnityEngine;

namespace RealmRush.UI
{
    public class UIService : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI _collectiblesQuestText;
        [SerializeField] private TextMeshProUGUI _killsQuestText;
        [SerializeField] private TextMeshProUGUI _exploredQuestText;
        [SerializeField] private TextMeshProUGUI _gameCompletedText;
        
        public void Initialize()
        {
            AddListensers();
        }
        private void AddListensers()
        {
            GameService.Instance.EventService.OnFetchQuest.AddListener(SetCollectiblesQuestText);
            GameService.Instance.EventService.OnKillQuest.AddListener(SetKillsQuestText);
            GameService.Instance.EventService.OnExplore.AddListener(SetExploredQuestText);
            GameService.Instance.EventService.OnGameCompleted.AddListener(EnableGameCompletedText);
        }

        ~UIService()
        {
            GameService.Instance.EventService.OnFetchQuest.RemoveListener(SetCollectiblesQuestText);
            GameService.Instance.EventService.OnKillQuest.RemoveListener(SetKillsQuestText);
            GameService.Instance.EventService.OnExplore.RemoveListener(SetExploredQuestText);
            GameService.Instance.EventService.OnGameCompleted.RemoveListener(EnableGameCompletedText);
        }
        private void SetCollectiblesQuestText(QuestSO quest)
        {
            _collectiblesQuestText.text = "Collect Cubes : " + quest.currentCount + " / " + quest.goalCount ;
        }

        private void SetKillsQuestText(QuestSO quest)
        {
            _killsQuestText.text = "Kill Enemies : " + quest.currentCount + " / " + quest.goalCount;
        }

        private void SetExploredQuestText(QuestSO quest)
        {
            _exploredQuestText.text = "Explore Area : " + quest.currentCount + " / " + quest.goalCount;
        }

        private void EnableGameCompletedText() => _gameCompletedText.gameObject.SetActive(true);
    }
}