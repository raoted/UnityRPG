using System;
using System.Collections;
using UnityEngine;
using UnityEngine.AI;

public class EnemyFSM : EnemyStatus
{
    public enum EnemyState
    {
        Idle,
        Move,
        Attack,
        Return,
        Damaged,
        Die
    }
    private EnemyState state;

    NavMeshAgent agent;
    Animator anim;
    public BoxCollider weapon;

    #region "Move와 Return에 사용할 변수"
    #endregion
    GameObject target;
    Vector3 startPoint;

    #region "Attack에 사용할 변수"
    #endregion
    private float attTime = 2.0f;
    private float timer = 0.0f;


    //시야각
    [SerializeField] float sightAngle = 0.0f;
    //시야 거리
    [SerializeField] float sightDist = 0.0f;
    //추적 거리
    [SerializeField] float chaseDist = 0.0f;
    LayerMask targetLayer = 0;
    // Start is called before the first frame update
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        anim = GetComponent<Animator>();
        agent.speed = moveSpeed;
        target = GameObject.Find("Player");
        startPoint = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        switch (state)
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
                Return();
                break;
            case EnemyState.Damaged:
                Damaged();
                break;
            case EnemyState.Die:
                Die();
                break;
            default:
                break;
        }
    }

    private void Idle()
    {
        if(Sight())
        {
            state = EnemyState.Move;
            anim.SetTrigger("Move");
        }
    }

    private void Move()
    {
        if(Vector3.Distance(transform.position, target.transform.position) <= agent.stoppingDistance)
        {
            agent.isStopped = true;
            agent.velocity = Vector3.zero;

            state = EnemyState.Attack;

            Debug.Log("Attack");
            anim.SetTrigger("Attack");
        }
        else if(Vector3.Distance(transform.position, startPoint) > chaseDist)
        {
            state = EnemyState.Return;
            anim.SetTrigger("Return");
            agent.SetDestination(startPoint);
        }
        else
        {
            agent.SetDestination(target.transform.position);
        }
    }

    private void Attack()
    {
        if (Vector3.Distance(target.transform.position, transform.position) <= agent.stoppingDistance)
        {
            if(anim.GetInteger("numAttack") == 0)
            {
                timer += Time.deltaTime;
                if (timer >= attackRate)
                {
                    timer = 0;
                    //공격 처리
                    anim.SetTrigger("Attack");
                    anim.SetInteger("numAttack", UnityEngine.Random.Range(1, attackPattern));
                    weapon.GetComponent<BoxCollider>().enabled = true;
                }
            }
            else
            {
                if(anim.GetCurrentAnimatorStateInfo(0).normalizedTime >= 1.0f)
                {
                    anim.SetInteger("numAttack", 0);
                    weapon.GetComponent<BoxCollider>().enabled = false;
                }
            }
        }
        else
        {
            agent.isStopped = false;
            timer = 0;
            state = EnemyState.Move;
            anim.SetTrigger("Move");
            Debug.Log("Move");
        }
    }

    private void Return()
    {
        if(Sight() && Vector3.Distance(transform.position, startPoint) <= chaseDist)
        {
            state = EnemyState.Move;
        }
        else if(Vector3.Distance(transform.position, startPoint) == 0.0f)
        {
            agent.velocity = Vector3.zero;
            state = EnemyState.Idle;
            anim.SetTrigger("Idle");
        }
    }

    private void Damaged()
    {
        if (anim.GetCurrentAnimatorStateInfo(0).normalizedTime >= 1.0f)
        {
            anim.SetTrigger("Idle");
        }
    }

    private void Die()
    {
        
    }

    public bool Sight()
    {
        Vector3 targetDir = (target.transform.position - transform.position).normalized;
        float dist = Vector3.Distance(target.transform.position, transform.position);
        float dot = Vector3.Dot(transform.forward, targetDir);

        float theta = Mathf.Acos(dot) * Mathf.Rad2Deg;

        if (theta <= sightAngle && dist <= sightDist)
        {
            Debug.Log("포착됨");
            return true;
        }
        else
        {
            return false;
        }
    }

    public void HitDamage(float damage)
    {
        if(damage >= 0)
        {
            state = EnemyState.Damaged;
            anim.SetTrigger("Damaged");
        }
        else 
        {
            state = EnemyState.Die;
            anim.SetTrigger("Die");
        }
    }
}
