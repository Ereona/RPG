using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Unit : FieldEntity
{
    [SerializeField]
    private UnitVisual _visual;
    [SerializeField]
    private HealthBar _healthBar;

    private UnitSettings _settings;
    public UnitSettings Settings 
    {
        get { return _settings; }
        set 
        { 
            _settings = value;
            Health = _settings.HealthPoints;
            _healthBar.SetStartHealth(_settings.HealthPoints);
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
            _healthBar.SetOwner(value);
        }
    }

    public BattleFieldSize Sizes { get; set; }

    private int _health;
    public int Health 
    {
        get 
        { 
            return _health; 
        }
        private set 
        { 
            _health = value;
            _healthBar.SetHealth(value);
        }
    }

    public void Init(Owner owner, Vector2Int pos)
    {
        Owner = owner;
        Coords = pos;
    }

    public void Move(Vector2Int target)
    {
        Coords = target;
        transform.localPosition = Sizes.CalcPosition(Coords);
    }

    public void Attack()
    {

    }

    public void Damage(int value)
    {
        Health -= value;
        _visual.Damage();
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
