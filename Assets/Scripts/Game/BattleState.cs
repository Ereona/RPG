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

    public List<Unit> AvailableForAttack { get; private set; }
    public List<PlaneTile> AvailableForMove { get; private set; }

    private BattleHistory _history = new BattleHistory();

    public void Init(List<Unit> units, List<PlaneTile> tiles)
    {
        _units = units;
        _tiles = tiles;
        AvailableForAttack = new List<Unit>();
        AvailableForMove = new List<PlaneTile>();
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
        _history.Clear();
    }

    public void DoMove(PlaneTile tile)
    {
        _history.AddRecord(_selectedUnit, BattleAction.Move);
        _selectedUnit.Move(tile.Coords);
        RecalcAvailability();
    }

    public void DoAttack(Unit target)
    {
        _history.AddRecord(_selectedUnit, BattleAction.Attack);
        _selectedUnit.Attack();
        target.Damage(_selectedUnit.Settings.DamageAttackPoints);
        if (!target.IsAlive())
        {
            _units.Remove(target);
            target.Die();
            CheckEndGame();
        }
        RecalcAvailability();
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
        return u.Owner == Turn && _history.CanDoSomeAction(u);
    }

    public bool CanBeAttacked(Unit u)
    {
        return AvailableForAttack.Contains(u);
    }

    public bool CanMoveTo(PlaneTile tile)
    {
        return AvailableForMove.Contains(tile);
    }

    private void RecalcAvailability()
    {
        ClearAvailability();
        if (_selectedUnit != null)
        {
            if (_history.CanDoAction(_selectedUnit, BattleAction.Attack))
            {
                foreach (Unit u in _units)
                {
                    if (u.Owner != _selectedUnit.Owner
                        && Distance(u, _selectedUnit) <= _selectedUnit.Settings.AttackRange)
                    {
                        u.SetIsTarget(true);
                        AvailableForAttack.Add(u);
                    }
                }
            }
            if (_history.CanDoAction(_selectedUnit, BattleAction.Move))
            {
                foreach (PlaneTile p in _tiles)
                {
                    if ((Distance(p, _selectedUnit) <= _selectedUnit.Settings.MoveRange)
                        && !Occupied(p))
                    {
                        p.SetIsTarget(true);
                        AvailableForMove.Add(p);
                    }
                }
            }
        }
    }

    private void ClearAvailability()
    {
        foreach (Unit u in AvailableForAttack)
        {
            u.SetIsTarget(false);
        }
        foreach (PlaneTile p in AvailableForMove)
        {
            p.SetIsTarget(false);
        }
        AvailableForAttack.Clear();
        AvailableForMove.Clear();
    }

    private float Distance(FieldEntity f1, FieldEntity f2)
    {
        return Mathf.Abs(f1.Coords.x - f2.Coords.x) + Mathf.Abs(f1.Coords.y - f2.Coords.y);
    }

    private bool Occupied(PlaneTile p)
    {
        return _units.Any(u => u.Coords == p.Coords);
    }

    public List<Unit> GetUnits(Owner owner)
    {
        return _units.Where(c => c.Owner == owner).ToList();
    }

    private void CheckEndGame()
    {
        if (GetUnits(Owner.Enemy).Count == 0)
        {
            EventBus.RaiseGameEnd(Owner.Player);
        }
        else if (GetUnits(Owner.Player).Count == 0)
        {
            EventBus.RaiseGameEnd(Owner.Enemy);
        }
    }
}
