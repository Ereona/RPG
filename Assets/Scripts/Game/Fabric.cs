using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fabric : MonoBehaviour
{
    [SerializeField]
    private Transform _itemsParent;
    [SerializeField]
    private PlaneTile _planeTilePrefab;

    public PlaneTile CreatePlaneTile()
    {
        PlaneTile planeTile = Instantiate(_planeTilePrefab, _itemsParent);
        return planeTile;
    }

    public Unit CreateUnit(Owner owner, BattleFieldUnitData data)
    {
        Unit unit = Instantiate(data.Unit.Prefab, _itemsParent);
        unit.Init(owner, new Vector2Int(data.MPos, data.NPos));
        unit.Settings = data.Unit;
        return unit;
    }
}
