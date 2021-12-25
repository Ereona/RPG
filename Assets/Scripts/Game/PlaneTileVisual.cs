using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlaneTileVisual : MonoBehaviour
{
    [SerializeField]
    private MeshRenderer _mesh;
    [SerializeField]
    private Color _normalColor;
    [SerializeField]
    private Color _targetColor;

    public void SetIsTarget(bool value)
    {
        _mesh.material.SetColor("_BaseColor", value ? _targetColor : _normalColor);
    }
}
