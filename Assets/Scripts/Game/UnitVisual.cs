using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitVisual : MonoBehaviour
{
    [SerializeField]
    private MeshRenderer _head;
    [SerializeField]
    private Color _playerColor;
    [SerializeField]
    private Color _enemyColor;
    [SerializeField]
    private GameObject _selection;
    [SerializeField]
    private GameObject _targetMark;

    public void SetOwner(Owner owner)
    {
        switch (owner)
        {
            case Owner.Player:
                _head.material.SetColor("_BaseColor", _playerColor);
                break;
            case Owner.Enemy:
                _head.material.SetColor("_BaseColor", _enemyColor);
                break;
        }
    }

    public void Select()
    {
        _selection.SetActive(true);
    }

    public void Deselect()
    {
        _selection.SetActive(false);
    }

    public void SetIsTarget(bool value)
    {
        _targetMark.SetActive(value);
    }
}
