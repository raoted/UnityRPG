﻿using System.Collections;
using UnityEngine;
using UnityEngine.AI;


public class FootManFSM : EnemyFSM
{
    EnemyStatus status;
    EnemyState state;

    public BoxCollider weaponCollider;

    Transform player;
    // Start is called before the first frame update
    void Start()
    {
        Timer = 0.0f;

        FindRange = 15.0f;
        SightAngle = 120.0f;
        ChaseRange = 30.0f;
        AttackRange = 2.0f;

        status = GetComponent<EnemyStatus>();
        
        status.MaxHP = 100;
        status.Damage = 3;
        status.MoveSpeed = 10.0f;
        status.AttackRate = 3.0f;
        
        player = GameObject.Find("Player").transform;

        Weapon = weaponCollider;
        Weapon.enabled = false;

        StartPoint = transform;
        
        Agent = GetComponent<NavMeshAgent>();
        Agent.speed = status.MoveSpeed;
        
        Animator = GetComponent<Animator>();
        Animator.SetTrigger("Idle");
        
        state = EnemyState.Idle;
    }

    // Update is called once per frame
    void Update()
    {
        switch(state)
        {
            case EnemyState.Idle:
                Idle();
                break;
            case EnemyState.Move:
                Move();
                break;
            case EnemyState.Attack:
                Attack();
                break;
            case EnemyState.Return:
                Return ();
                break;
            case EnemyState.Damaged:
                //Damaged();
                break;
            case EnemyState.Die:
                //Die();
                break;
            default:
                break;
        }
    }

    public override void Idle()
    {
        if(Sight())
        {
            state = EnemyState.Move;
            Animator.SetTrigger("Move");
        }
    }

    public override void Move()
    {
        if(Vector3.Distance(transform.position, StartPoint.position) <= ChaseRange)
        {
            if (Vector3.Distance(transform.position, player.position) < AttackRange)
            {
                Agent.isStopped = true;
                Agent.velocity = Vector3.zero;
                state = EnemyState.Attack;
                Animator.SetTrigger("Attack");
            }
            else
            {
                if (Agent.destination != player.position)
                {
                    Agent.SetDestination(player.position);
                }
            }
        }
        else
        {
            if(!Agent.CalculatePath(StartPoint.position, Agent.path))
            {
                StartPoint = transform;
            }
            Agent.SetDestination(StartPoint.position);

            state = EnemyState.Return;
            Animator.SetTrigger("Return");
        }
    }

    public override void Attack()
    {
        transform.LookAt(player);

        if(Vector3.Distance(transform.position, player.position) > AttackRange)
        {
            Weapon.enabled = false;
            Agent.isStopped = false;

            state = EnemyState.Move;
            Animator.SetTrigger("Move");
        }
        else
        {
            if(Timer >= status.AttackRate)
            {
                Weapon.enabled = true;

                Animator.SetTrigger("Attack");
                Animator.SetInteger("numAttack", UnityEngine.Random.Range(1, 4));
                
                Timer = 0.0f;
                
                StartCoroutine(AttackEnd());
            }
            else
            {
                Timer += Time.deltaTime;
            }
        }
    }

    public override void Return()
    {
        if(Vector3.Distance(transform.position, StartPoint.position) == 0.0f)
        {
            iTween.RotateTo(gameObject, StartPoint.eulerAngles, 1.0f);

            state = EnemyState.Idle;
            Animator.SetTrigger("Idle");
        }
        else if(Vector3.Distance(transform.position, StartPoint.position) <= ChaseRange)
        {
            if(Sight())
            {
                state = EnemyState.Move;
                Animator.SetTrigger("Move");
            }
        }
    }

    public override IEnumerator Damaged()
    {
        yield return new WaitForSeconds(Animator.GetCurrentAnimatorClipInfo(0).Length);

        state = EnemyState.Idle;
        Animator.SetTrigger("Idle");
    }

    public override bool Sight()
    {
        Vector3 targetDir = (player.position - transform.position).normalized;
        float dist = Vector3.Distance(player.position, transform.position);
        float dot = Vector3.Dot(transform.forward, targetDir);

        float theta = Mathf.Acos(dot) * Mathf.Rad2Deg;

        if (theta <= SightAngle && dist <= FindRange) { return true; }
        else { return false; }
    }

    public override void HitDamage(float damage)
    {
        if (state == EnemyState.Damaged || state == EnemyState.Die) { return; }

        status.HP -= damage;
        if (status.HP > damage)
        {
            state = EnemyState.Damaged;
            Animator.SetTrigger("Damaged");

            StartCoroutine(Damaged());
        }
        else
        {
            status.HP = 0;
            Agent.enabled = false;
            transform.GetComponent<CapsuleCollider>().enabled = false;
            state = EnemyState.Die;
            Animator.SetTrigger("Die");
        }
        Debug.Log(status.HP);
    }

}
