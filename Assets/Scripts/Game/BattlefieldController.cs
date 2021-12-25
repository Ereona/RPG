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
    [SerializeField]
    private BattleController _battleController;

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

        List<PlaneTile> tiles = new List<PlaneTile>();
        for (int i = 0; i < _settings.M; i++)
        {
            for (int j = 0; j < _settings.N; j++)
            {
                tiles.Add(CreatePlaneTile(new Vector2Int(i, j)));
            }
        }

        List<Unit> units = new List<Unit>();
        foreach (BattleFieldUnitData data in _settings.PlayerUnits)
        {
            units.Add(CreateUnit(Owner.Player, data));
        }
        foreach (BattleFieldUnitData data in _settings.EnemyUnits)
        {
            units.Add(CreateUnit(Owner.Enemy, data));
        }

        BattleState state = new BattleState();
        state.Init(units, tiles);
        _battleController.SetState(state);
    }

    private PlaneTile CreatePlaneTile(Vector2Int pos)
    {
        PlaneTile tile = _fabric.CreatePlaneTile();
        tile.Coords = pos;
        tile.transform.localPosition = CalcPosition(pos);
        tile.transform.localScale = Vector3.one * _cellSize;
        return tile;
    }

    private Unit CreateUnit(Owner owner, BattleFieldUnitData data)
    {
        Unit unit = _fabric.CreateUnit(owner, data);
        unit.Controller = this;
        unit.transform.localPosition = CalcPosition(new Vector2Int(data.MPos, data.NPos));
        unit.transform.localScale = Vector3.one * _cellSize;
        return unit;
    }

    public Vector3 CalcPosition(Vector2Int pos)
    {
        float x = pos.x * _cellSize - _planeXSize / 2;
        float z = pos.y * _cellSize - _planeYSize / 2;
        return new Vector3(x, 0, z);
    }
}
