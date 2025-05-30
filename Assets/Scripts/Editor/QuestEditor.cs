using RealmRush.Quest;
using UnityEditor;
using UnityEngine;

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
            
        }

        private void GeneralFields()
        {
            _questType = (QuestType)EditorGUILayout.EnumPopup("Quest Type", _questType);
            
            _questTitle = EditorGUILayout.TextField("Quest Name", _questTitle);
            
            _questDescription = EditorGUILayout.TextField("Description", _questDescription);
            _goalCount = EditorGUILayout.IntField("Goal", _goalCount);
        }
    }
}
