using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleHistory
{
    private Dictionary<Unit, List<BattleAction>> _actions = new Dictionary<Unit, List<BattleAction>>();

    public void AddRecord(Unit u, BattleAction a)
    {
        List<BattleAction> actionsList;
        if (!_actions.TryGetValue(u, out actionsList))
        {
            actionsList = new List<BattleAction>();
            _actions.Add(u, actionsList);
        }
        if (!actionsList.Contains(a))
        {
            actionsList.Add(a);
        }
    }

    public bool CanDoAction(Unit u, BattleAction a)
    {
        List<BattleAction> actionsList;
        if (_actions.TryGetValue(u, out actionsList))
        {
            return !actionsList.Contains(a);
        }
        else
        {
            return true;
        }
    }

    public bool CanDoSomeAction(Unit u)
    {
        return CanDoAction(u, BattleAction.Attack) || CanDoAction(u, BattleAction.Move);
    }

    public void Clear()
    {
        _actions.Clear();
    }
}
