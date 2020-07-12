using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class EnemyStatus : MonoBehaviour
{
    public float maxHP; 
    private float hp;
    public float moveSpeed;
    public float attackRate;
    public float damage;
    public int attackPattern;

    public float MaxHP
    {
        get { return maxHP; }
        set 
        {
            maxHP = value;
            SetHP = maxHP;
        }
    }
    public float SetHP
    {
        set { hp = maxHP; }
    }
    public float HP
    {
        get { return hp; }
        set { hp = value; }
    }

}
