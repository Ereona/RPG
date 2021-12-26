using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleFieldSize : MonoBehaviour
{
    [SerializeField]
    private float _maxXSize;
    [SerializeField]
    private float _maxYSize;

    private float _planeXSize;
    private float _planeYSize;

    public float CellSize { get; private set; }

    public void Init(BattleFieldSettings settings)
    {
        float cellXSize = _maxXSize / settings.M;
        float cellYSize = _maxYSize / settings.N;
        CellSize = Mathf.Min(cellXSize, cellYSize);
        _planeXSize = CellSize * settings.M;
        _planeYSize = CellSize * settings.N;
    }

    public Vector3 CalcPosition(Vector2Int pos)
    {
        float x = pos.x * CellSize - _planeXSize / 2;
        float z = pos.y * CellSize - _planeYSize / 2;
        return new Vector3(x, 0, z);
    }
}
