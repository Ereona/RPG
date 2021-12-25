using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class AIBrain : MonoBehaviour
{
    private BattleState _state;
    private float _delay = 1f;

    private Coroutine _doActionsCoroutine;

    private void Start()
    {
        EventBus.TurnChanged += EventBus_TurnChanged;
        EventBus.GameEnd += EventBus_GameEnd;
    }

    private void EventBus_TurnChanged(Owner obj)
    {
        if (obj == Owner.Enemy)
        {
            _doActionsCoroutine = StartCoroutine(DoActions());
        }
    }

    private void EventBus_GameEnd(Owner obj)
    {
        if (_doActionsCoroutine != null)
        {
            StopCoroutine(_doActionsCoroutine);
            _doActionsCoroutine = null;
        }
    }

    public void Init(BattleState state)
    {
        _state = state;
    }

    private IEnumerator DoActions()
    {
        List<Unit> aiUnits = _state.GetUnits(Owner.Enemy);
        foreach (Unit selected in aiUnits)
        {
            bool hasAttacked = false;
            _state.SelectUnit(selected);
            Unit attacked = FindUnitForAttack();
            if (attacked != null)
            {
                _state.DoAttack(attacked);
                hasAttacked = true;
                yield return new WaitForSeconds(_delay);        
            }
            PlaneTile toMove = FindTileForMove();
            if (toMove != null)
            {
                _state.DoMove(toMove);
                yield return new WaitForSeconds(_delay);
            }
            if (!hasAttacked)
            {
                attacked = FindUnitForAttack();
                if (attacked != null)
                {
                    _state.DoAttack(attacked);
                    yield return new WaitForSeconds(_delay);
                }
            }
        }
        yield return null;
        _doActionsCoroutine = null;
        EventBus.RaiseEndTurn();
    }

    private Unit FindUnitForAttack()
    {
        return _state.AvailableForAttack.FirstOrDefault();
    }

    private PlaneTile FindTileForMove()
    {
        List<PlaneTile> available = _state.AvailableForMove;
        if (available.Count == 0)
        {
            return null;
        }
        int index = Random.Range(0, available.Count);
        return available[index];
    }

    private void OnDestroy()
    {
        EventBus.TurnChanged -= EventBus_TurnChanged;
        EventBus.GameEnd -= EventBus_GameEnd;
    }
}
