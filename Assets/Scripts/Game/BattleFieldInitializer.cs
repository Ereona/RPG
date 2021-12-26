using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleFieldInitializer : MonoBehaviour
{
    [SerializeField]
    private Fabric _fabric;

    public BattleState CreateBattle(BattleFieldSettings settings, BattleFieldSize sizes)
    {
        List<PlaneTile> tiles = new List<PlaneTile>();
        for (int i = 0; i < settings.M; i++)
        {
            for (int j = 0; j < settings.N; j++)
            {
                tiles.Add(CreatePlaneTile(new Vector2Int(i, j), sizes));
            }
        }

        List<Unit> units = new List<Unit>();
        foreach (BattleFieldUnitData data in settings.PlayerUnits)
        {
            units.Add(CreateUnit(Owner.Player, data, sizes));
        }
        foreach (BattleFieldUnitData data in settings.EnemyUnits)
        {
            units.Add(CreateUnit(Owner.Enemy, data, sizes));
        }

        BattleState state = new BattleState();
        state.Init(units, tiles);
        return state;
    }

    private PlaneTile CreatePlaneTile(Vector2Int pos, BattleFieldSize sizes)
    {
        PlaneTile tile = _fabric.CreatePlaneTile();
        tile.Coords = pos;
        tile.transform.localPosition = sizes.CalcPosition(pos);
        tile.transform.localScale = Vector3.one * sizes.CellSize;
        return tile;
    }

    private Unit CreateUnit(Owner owner, BattleFieldUnitData data, BattleFieldSize sizes)
    {
        Unit unit = _fabric.CreateUnit(owner, data);
        unit.Sizes = sizes;
        unit.transform.localPosition = sizes.CalcPosition(new Vector2Int(data.MPos, data.NPos));
        unit.transform.localScale = Vector3.one * sizes.CellSize;
        return unit;
    }
}
