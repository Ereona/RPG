using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattlefieldController : MonoBehaviour
{
    [SerializeField]
    private BattlefieldSettings _settings;
    [SerializeField]
    private float _maxXSize;
    [SerializeField]
    private float _maxYSize;
    [SerializeField]
    private Fabric _fabric;

    private float _cellSize;
    private float _planeXSize;
    private float _planeYSize;

    void Start()
    {
        float cellXSize = _maxXSize / _settings.M;
        float cellYSize = _maxYSize / _settings.N;
        _cellSize = Mathf.Min(cellXSize, cellYSize);
        _planeXSize = _cellSize * _settings.M;
        _planeYSize = _cellSize * _settings.N;

        for (int i = 0; i < _settings.M; i++)
        {
            for (int j = 0; j < _settings.N; j++)
            {
                CreatePlaneTile(new Vector3Int(i, j, 0));
            }
        }
    }

    private void CreatePlaneTile(Vector3Int pos)
    {
        PlaneTile tile = _fabric.CreatePlaneTile();
        tile.Coords = pos;
        tile.transform.localPosition = CalcPosition(pos);
        tile.transform.localScale = Vector3.one * _cellSize;
    }

    private Vector3 CalcPosition(Vector3Int pos)
    {
        float x = pos.x * _cellSize - _planeXSize / 2;
        float y = pos.z * _cellSize;
        float z = pos.y * _cellSize - _planeYSize / 2;
        return new Vector3(x, y, z);
    }
}
