using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class BattleState
{
    public Owner Turn { get; private set; }

    private List<Unit> _units;
    private List<PlaneTile> _tiles;
    private Unit _selectedUnit;

    private List<Unit> _availableForAttack = new List<Unit>();
    private List<PlaneTile> _availableForMove = new List<PlaneTile>();

    public void Init(List<Unit> units, List<PlaneTile> tiles)
    {
        _units = units;
        _tiles = tiles;
        SetTurn(Owner.Player);
    }

    public void ChangeTurn()
    {
        SetTurn(Turn == Owner.Player ? Owner.Enemy : Owner.Player);
    }

    private void SetTurn(Owner turn)
    {
        Turn = turn;
        if (_selectedUnit != null)
        {
            _selectedUnit.Deselect();
        }
        _selectedUnit = null;
        ClearAvailability();
    }

    public void DoMove(PlaneTile tile)
    {
        Unit u = _selectedUnit;
        u.Move(tile.Coords);
        RecalcAvailability();
    }

    public void DoAttack(Unit target)
    {
        Unit u = _selectedUnit;
        u.Attack();
        target.Damage(u.Settings.DamageAttackPoints);
        if (!target.IsAlive())
        {
            _units.Remove(target);
            target.Die();
            RecalcAvailability();
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
        RecalcAvailability();
    }

    public bool CanBeSelected(Unit u)
    {
        return u.Owner == Turn;
    }

    public bool CanBeAttacked(Unit u)
    {
        return _availableForAttack.Contains(u);
    }

    public bool CanMoveTo(PlaneTile tile)
    {
        return _availableForMove.Contains(tile);
    }

    private void RecalcAvailability()
    {
        ClearAvailability();
        if (_selectedUnit != null)
        {
            foreach (Unit u in _units)
            {
                if (u.Owner != _selectedUnit.Owner
                    && Distance(u, _selectedUnit) <= _selectedUnit.Settings.AttackRange)
                {
                    u.SetIsTarget(true);
                    _availableForAttack.Add(u);
                }
            }
            foreach (PlaneTile p in _tiles)
            {
                if ((Distance(p, _selectedUnit) <= _selectedUnit.Settings.MoveRange)
                    && !Occupied(p))
                {
                    p.SetIsTarget(true);
                    _availableForMove.Add(p);
                }
            }
        }
    }

    private void ClearAvailability()
    {
        foreach (Unit u in _availableForAttack)
        {
            u.SetIsTarget(false);
        }
        foreach (PlaneTile p in _availableForMove)
        {
            p.SetIsTarget(false);
        }
        _availableForAttack.Clear();
        _availableForMove.Clear();
    }

    private float Distance(FieldEntity f1, FieldEntity f2)
    {
        return Mathf.Abs(f1.Coords.x - f2.Coords.x) + Mathf.Abs(f1.Coords.y - f2.Coords.y);
    }

    private bool Occupied(PlaneTile p)
    {
        return _units.Any(u => u.Coords == p.Coords);
    }
}
