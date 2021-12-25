using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Unit : FieldEntity
{
    [SerializeField]
    private UnitVisual _visual;

    private UnitSettings _settings;
    public UnitSettings Settings 
    {
        get { return _settings; }
        set 
        { 
            _settings = value;
            Health = _settings.HealthPoints;
        }
    }
    private Owner _owner;
    public Owner Owner 
    {
        get { return _owner; } 
        private set
        {
            _owner = value;
            _visual.SetOwner(value);
        }
    }

    public BattlefieldController Controller { get; set; }

    public int Health { get; private set; }

    public void Init(Owner owner, Vector2Int pos)
    {
        Owner = owner;
        Coords = pos;
    }

    public void Move(Vector2Int target)
    {
        Coords = target;
        transform.localPosition = Controller.CalcPosition(Coords);
    }

    public void Attack()
    {

    }

    public void Damage(int value)
    {
        Health -= value;
    }

    public bool IsAlive()
    {
        return Health > 0;
    }

    public void Die()
    {
        Destroy(this.gameObject);
    }

    public void Select()
    {
        _visual.Select();
    }

    public void Deselect()
    {
        _visual.Deselect();
    }

    public override void SetIsTarget(bool value)
    {
        _visual.SetIsTarget(value);
    }
}
