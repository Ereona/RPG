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
}
