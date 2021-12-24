using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Unit : MonoBehaviour
{
    public UnitSettings Settings { get; set; }
    public Owner Owner { get; private set; }
    public Vector2Int Pos { get; private set; }

    public void Init(Owner owner, Vector2Int pos)
    {
        Owner = owner;
        Pos = pos;
    }
}
