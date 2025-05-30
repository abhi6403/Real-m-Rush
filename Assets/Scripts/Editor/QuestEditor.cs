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
            
        }
    }
}
