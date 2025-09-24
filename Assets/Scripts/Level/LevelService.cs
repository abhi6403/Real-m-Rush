using UnityEngine;

public class LevelService
{
    private LevelDataSO _levelDataSO;
    private Vector3 _startPosition;
    
    public LevelService(LevelDataSO levelDataSO)
    {
        _levelDataSO = levelDataSO;
        _startPosition = levelDataSO.LevelDatas[0].startPosition;
        SpawnPillars();
    }

    private void SpawnPillars()
    {
        for (int i = 0; i <= _levelDataSO.LevelDatas[0].TargetNumber; i++)
        {
            PillarView obj = Object.Instantiate(_levelDataSO.LevelDatas[0].PillarPrefab, _startPosition, Quaternion.identity);
            obj.AsignPillarNumber(i);
            _startPosition = new Vector3(_startPosition.x, _startPosition.y, _startPosition.z - 6);
        }
    }
}