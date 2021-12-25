using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class BattleState
{
    private List<Unit> _allUnits;
    private Owner _turn { get; set; }
    private Unit _selectedUnit;

    public void Init(List<Unit> units)
    {
        _allUnits = units;
        SetTurn(Owner.Player);
    }

    public void ChangeTurn()
    {
        SetTurn(_turn == Owner.Player ? Owner.Enemy : Owner.Player);
    }

    private void SetTurn(Owner turn)
    {
        _turn = turn;
        if (_selectedUnit != null)
        {
            _selectedUnit.Deselect();
        }
        _selectedUnit = null;
    }

    public void DoMove(Vector2Int target)
    {
        Unit u = _selectedUnit;
        u.Move(target);
    }

    public void DoAttack(Unit target)
    {
        Unit u = _selectedUnit;
        u.Attack();
        target.Damage(u.Settings.DamageAttackPoints);
        if (!target.IsAlive())
        {
            _allUnits.Remove(target);
            target.Die();
        }
    }

    public void SelectUnit(Unit u)
    {
        if (_selectedUnit != null)
        {
            _selectedUnit.Deselect();
        }
        _selectedUnit = u;
        u.Select();
    }

    public bool CanBeSelected(Unit u)
    {
        return u.Owner == _turn;
    }

    public bool CanBeAttacked(Unit u)
    {
        return u.Owner != _turn;
    }

    public bool CanMoveTo(Vector2Int target)
    {
        return true;
    }
}
