using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "LevelDataSO", menuName = "Scriptable Objects/LevelDataSO")]
public class LevelDataSO : ScriptableObject
{
        public List<LevelData> LevelDatas;
}

[System.Serializable]
public struct LevelData
{
        public int MapID;
        public PillarView PillarPrefab;
        public int TargetNumber;
        public Vector3 startPosition;
}
