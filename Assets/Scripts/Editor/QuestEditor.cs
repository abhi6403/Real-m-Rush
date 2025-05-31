using System.IO;
using RealmRush.Quest;
using UnityEditor;
using UnityEngine;
using Directory = UnityEngine.Windows.Directory;

namespace RealmRush.Editables
{
    public class QuestEditor : EditorWindow
    {
        #region VariablesForCreatingNewQuest

          private string _questTitle;
          private string _questDescription;
          private int _goalCount;
          private QuestType _questType;
          private int _reward;
          
          private GameObject _collectibleObject;
  
          private GameObject _enemyObject;
  
          private GameObject _exploreZone;
          
          private const string questSavePath = "Assets/ScriptableObjects/Quests/";

        #endregion
        
        #region VariablesForExistingQuests

        private string[] existingQuestPaths;
        private QuestSO[] existingQuests;
        private int selectedQuestIndex;
        private QuestSO selectedQuest = null;
        
        #endregion
        
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
            
            if (GUILayout.Button("Save Changes") && selectedQuest != null)
            {
                SaveSelectedQuest(selectedQuest);
            }
            
            EditorGUILayout.Space();
            SetLoadExistingQuest();
        }

        #region MethodsForCreatingNewQuest
        
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
                        EditorUtility.DisplayDialog("Empty Collectible Object","Please add an game object","Do it now");
                        return false;
                    }
                    break;
                case QuestType.KILL:
                    if (!_enemyObject)
                    {
                        EditorUtility.DisplayDialog("Empty Enemy Object","Please add an game object","Do it now");
                        return false;
                    }
                    break;
                case QuestType.EXPLORE:
                    if (!_exploreZone)
                    {
                        EditorUtility.DisplayDialog("Empty Exploring Object","Please add an game object","Do it now");
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
        
        #endregion

        #region MethodsForLoadingExistingQuest()
        
        private void SetLoadExistingQuest()
        {
            EditorGUILayout.Space();
            GUILayout.Label("Load Existing Quest", EditorStyles.boldLabel);

            if (GUILayout.Button("Load Quest list"))
            {
                LoadExistingQuest();
            }

            if (existingQuests != null && existingQuests.Length > 0)
            {
                selectedQuestIndex = EditorGUILayout.Popup("Selected Quest", selectedQuestIndex, existingQuestPaths);

                if (GUILayout.Button("Load Selected Quest"))
                {
                    LoadExistingQuestData(existingQuests[selectedQuestIndex]);
                }
            }
        }

        private void LoadExistingQuest()
        {
            string[] guids = AssetDatabase.FindAssets($"t:QuestSO",new[]{"Assets/ScriptableObjects/Quests"});
            
            existingQuests = new QuestSO[guids.Length];
            existingQuestPaths = new string[guids.Length];

            for (int i = 0; i < guids.Length; i++)
            {
                string path = AssetDatabase.GUIDToAssetPath(guids[i]);
                existingQuests[i] = AssetDatabase.LoadAssetAtPath<QuestSO>(path);
                existingQuestPaths[i] = Path.GetFileNameWithoutExtension(path);
            }
        }
        
        private void LoadExistingQuestData(QuestSO questSO)
        {
            selectedQuest = questSO;
            _questTitle = questSO.questName;
            _questDescription = questSO.questDescription;
            _goalCount = questSO.goalCount;
            _questType = questSO.questType;
            _reward = questSO.reward;

            switch (_questType)
            {
                case QuestType.FETCH:
                    var fetch = questSO as FetchQuestSO;
                    _collectibleObject = fetch.collectiblePrefab;
                    break;
                case QuestType.KILL:
                    var kill = questSO as KillQuestSO;
                    _enemyObject = kill.killPrefab;
                    break;
                case QuestType.EXPLORE:
                    var explore = questSO as ExploreQuestSO;
                    _exploreZone = explore.exploreTrigger;
                    break;
            }
            
            Repaint();
        }

        private void SaveSelectedQuest(QuestSO questSO)
        {
            questSO.questName = _questTitle;
            questSO.questDescription = _questDescription;
            questSO.goalCount = _goalCount;
            questSO.questType = _questType;
            questSO.reward = _reward;

            switch (_questType)
            {
                case QuestType.FETCH:
                    var fetch = questSO as FetchQuestSO;
                    fetch.collectiblePrefab = _collectibleObject;
                    break;
                case QuestType.KILL:
                    var kill = questSO as KillQuestSO;
                    kill.killPrefab = _enemyObject;
                    break;
                case QuestType.EXPLORE:
                    var explore = questSO as ExploreQuestSO;
                    explore.exploreTrigger = _exploreZone;
                    break;
            }
            
            EditorUtility.DisplayDialog("Update Successfull", "Changes was succussfully saved", "OK");
            EditorUtility.SetDirty(questSO);
            AssetDatabase.SaveAssets();
        }
        #endregion
    }
}
