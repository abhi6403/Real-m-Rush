using RealmRush.Quest;
using UnityEditor;
using UnityEngine;
using UnityEngine.Windows;

namespace RealmRush.Editables
{
    public class QuestEditor : EditorWindow
    {
        private string _questTitle;
        private string _questDescription;
        private int _goalCount;
        private QuestType _questType;
        private int _reward;
        
        private GameObject _collectibleObject;

        private GameObject _enemyObject;

        private GameObject _exploreZone;
        
        private const string questSavePath = "Assets/ScriptableObjects/Quests/";

        [MenuItem("RealmRush Tools/ Quest Editor")]
        public static void OpenQuestWindow()
        {
            GetWindow<QuestEditor>("Quest Editor");
        }

        private void OnGUI()
        {
            GUILayout.Label("Create New Quest", EditorStyles.boldLabel);
            EditorGUILayout.Space();
            
            GeneralFields();
            QuestTypeFields();
            
            EditorGUILayout.Space();
            SetCreateButton();
        }

        private void GeneralFields()
        {
            _questType = (QuestType)EditorGUILayout.EnumPopup("Quest Type", _questType);
            
            _questTitle = EditorGUILayout.TextField("Quest Name", _questTitle);
            
            _questDescription = EditorGUILayout.TextField("Description", _questDescription);
            _goalCount = EditorGUILayout.IntField("Goal", _goalCount);
        }

        private void QuestTypeFields()
        {
            EditorGUILayout.Space();

            switch (_questType)
            {
                case QuestType.FETCH:
                    _collectibleObject = (GameObject)EditorGUILayout.ObjectField("Collectible", _collectibleObject, typeof(GameObject));
                    _reward = EditorGUILayout.IntField("Reward", _reward);
                    break;
                case QuestType.KILL:
                    _enemyObject = (GameObject)EditorGUILayout.ObjectField("Enemy Object",_enemyObject, typeof(GameObject));
                    _reward = EditorGUILayout.IntField("Reward", _reward);
                    break;
                case QuestType.EXPLORE:
                    _exploreZone = (GameObject)EditorGUILayout.ObjectField("Explore Zone", _exploreZone, typeof(GameObject));
                    _reward = EditorGUILayout.IntField("Reward", _reward);
                    break;
            }
        }

        private void SetCreateButton()
        {
            if (GUILayout.Button("Create New Quest"))
            {
                if (CheckForAllFilledFields())
                {
                    CreateQuest();
                }
            }
        }

        private void CreateQuest()
        {
            QuestSO questSO = null;

            switch (_questType)
            {
                case QuestType.FETCH:
                    var fetchQuestSO = ScriptableObject.CreateInstance<FetchQuestSO>();
                    fetchQuestSO.collectiblePrefab = _collectibleObject;
                    fetchQuestSO.reward = _reward;
                    questSO = fetchQuestSO;
                    break;
                case QuestType.KILL:
                    var killQuestSO = ScriptableObject.CreateInstance<KillQuestSO>();
                    killQuestSO.killPrefab = _enemyObject;
                    killQuestSO.reward = _reward;
                    questSO = killQuestSO;
                    break;
                case QuestType.EXPLORE:
                    var exploreQuestSO = ScriptableObject.CreateInstance<ExploreQuestSO>();
                    exploreQuestSO.exploreTrigger = _exploreZone;
                    exploreQuestSO.reward = _reward;
                    questSO = exploreQuestSO;
                    break;
            }
            
            questSO.questName = _questTitle;
            questSO.questDescription = _questDescription;
            questSO.goalCount = _goalCount;
            questSO.questType = _questType;
            
            Directory.CreateDirectory(questSavePath);
            
            string saveTitle = _questTitle.Replace(" ", "_");
            string assetPath = $"{questSavePath}{saveTitle}_{_questType}.asset";
            
            AssetDatabase.CreateAsset(questSO, assetPath);
            AssetDatabase.SaveAssets();
        }

        private bool CheckForAllFilledFields()
        {
            if (string.IsNullOrWhiteSpace(_questTitle))
            {
                EditorUtility.DisplayDialog("Quest title is empty.","Please enter the title","Do it now");
                return false;
            }

            if (string.IsNullOrWhiteSpace(_questDescription))
            {
                EditorUtility.DisplayDialog("Quest description is empty.","Please enter description","Do it now");
                return false;
            }
            
            if (_goalCount < 1)
            {
                EditorUtility.DisplayDialog("Invalid goal count.","Please enter a valid goal count","Do it now");
                return false;
            }
            
            switch (_questType)
            {
                case QuestType.FETCH:
                    if (!_collectibleObject)
                    {
                        EditorUtility.DisplayDialog("Empty Object","Please add an game object","Do it now");
                        return false;
                    }
                    break;
                case QuestType.KILL:
                    if (!_enemyObject)
                    {
                        EditorUtility.DisplayDialog("Empty Object","Please add an game object","Do it now");
                        return false;
                    }
                    break;
                case QuestType.EXPLORE:
                    if (!_exploreZone)
                    {
                        EditorUtility.DisplayDialog("Empty Object","Please add an game object","Do it now");
                        return false;
                    }
                    break;
            }

            if (_reward < 1)
            {
                EditorUtility.DisplayDialog("Quest reward is empty.","Please enter reward","Do it now");
                return false;
            }
            
           return true;
        }
    }
}
