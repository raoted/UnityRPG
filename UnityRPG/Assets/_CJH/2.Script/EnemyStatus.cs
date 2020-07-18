using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyStatus : MonoBehaviour
{
    private float maxHP;
    //maxHP getter, setter 선언
    public float MaxHP
    {
        get { return maxHP; }
        set
        {
            maxHP = value;
            
            if(hp > maxHP) { hp = maxHP; }
        }
    }

    private float hp;
    //hp getter, setter 선언
    public float HP
    {
        get { return hp; }
        set { hp = value; }
    }

    private float moveSpeed;
    //moveSpeed getter, setter 선언
    public float MoveSpeed
    {
        get { return moveSpeed; }
        set { moveSpeed = value; }
    }

    private float attackRate;
    //attackRate getter, setter 선언
    public float AttackRate
    {
        get { return attackRate; }
        set { attackRate = value; }
    }

    private float damage;
    //Damage getter, setter 선언
    public float Damage
    {
        get { return damage; }
        set { damage = value; }
    }

    private int attackPattern;
    //AttackPattern getter, setter 선언
    public int AttackPattern
    {
        get { return attackPattern; }
        set { attackPattern = value; }
    }
}
