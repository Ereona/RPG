using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleController : MonoBehaviour
{
    private BattleState _state;

    private void Start()
    {
        EventBus.MouseClick += EventBus_MouseClick;
        EventBus.EndTurn += EventBus_EndTurn;
    }

    public void SetBattle(BattleState state)
    {
        _state = state;
    }

    private void EventBus_MouseClick(Vector3 pos)
    {
        if (_state == null)
        {
            Debug.LogError("BattleController not inited");
            return;
        }
        if (_state.Turn != Owner.Player)
        {
            return;
        }
        RaycastHit hit;
        var ray = Camera.main.ScreenPointToRay(pos);
        if (Physics.Raycast(ray, out hit))
        {
            var transform = hit.collider.GetComponent<Transform>();
            if (transform != null)
            {
                Unit unit = transform.GetComponentInParent<Unit>();
                if (unit != null)
                {
                    if (_state.CanBeSelected(unit))
                    {
                        _state.SelectUnit(unit);
                    }
                    else if (_state.CanBeAttacked(unit))
                    {
                        _state.DoAttack(unit);
                    }
                }
                PlaneTile tile = transform.GetComponentInParent<PlaneTile>();
                if (tile != null)
                {
                    if (_state.CanMoveTo(tile))
                    {
                        _state.DoMove(tile);
                    }
                }
            }
        }
    }

    private void EventBus_EndTurn()
    {
        _state.ChangeTurn();
        EventBus.RaiseTurnChanged(_state.Turn);
    }

    private void OnDestroy()
    {
        EventBus.MouseClick -= EventBus_MouseClick;
        EventBus.EndTurn -= EventBus_EndTurn;
    }
}
