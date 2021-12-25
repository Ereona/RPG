using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Unit : MonoBehaviour
{
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
    public Owner Owner { get; private set; }
    public Vector2Int Pos { get; private set; }
    public BattlefieldController Controller { get; set; }

    public int Health { get; private set; }

    public void Init(Owner owner, Vector2Int pos)
    {
        Owner = owner;
        Pos = pos;
    }

    public void Move(Vector2Int target)
    {
        Pos = target;
        transform.localPosition = Controller.CalcPosition(Pos);
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
    }

    public void Deselect()
    {

    }
}
