using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlaneTile : FieldEntity
{
    [SerializeField]
    private PlaneTileVisual _visual;

    public override void SetIsTarget(bool value)
    {
        _visual.SetIsTarget(value);
    }
}
