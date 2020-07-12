using System.Collections;
using UnityEngine;
using UnityEngine.AI;

public class FootManFSM : EnemyStatus
{
    enum EnemyState
    {
        Idle,
        Move,
        Attack,
        Return,
        Damaged,
        Die
    }
    EnemyState state;

    [SerializeField]
    private float findRange = 15.0f;
    private float sightAngle = 120.0f;
    private float moveRange = 30.0f;
    private float attackRange = 2.0f;
    
    private float timer = 0.0f;

    public BoxCollider weapon;
    NavMeshAgent agent;
    Animator anim;

    Transform startPoint;
    Transform player;
    // Start is called before the first frame update
    void Start()
    {
        weapon.enabled = false;
        startPoint = transform;
        agent = GetComponent<NavMeshAgent>();

        anim = GetComponent<Animator>();
        state = EnemyState.Idle;
        anim.SetTrigger("Idle");

        MaxHP = 100;
        damage = 3;
        moveSpeed = 10.0f;
        attackRate = 3.0f;

        agent.speed = moveSpeed;
        player = GameObject.Find("Player").transform;
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
        if(Vector3.Distance(transform.position, startPoint.position) <= moveRange)
        {
            if (Vector3.Distance(transform.position, player.position) < attackRange)
            {
                agent.isStopped = true;
                agent.velocity = Vector3.zero;
                state = EnemyState.Attack;
                anim.SetTrigger("Attack");
            }
            else
            {
                if (agent.CalculatePath(player.position, agent.path))
                {
                    agent.SetDestination(player.position);
                }
            }
        }
        else
        {
            if(!agent.CalculatePath(startPoint.position, agent.path))
            {
                startPoint.position = transform.position;
            }
            agent.SetDestination(startPoint.position);

            state = EnemyState.Return;
            anim.SetTrigger("Return");
        }
    }

    private void Attack()
    {
        transform.LookAt(player);
        if(Vector3.Distance(transform.position, player.position) > attackRange)
        {
            weapon.enabled = false;
            agent.isStopped = false;
            state = EnemyState.Move;
            anim.SetTrigger("Move");
        }
        else
        {
            timer += Time.deltaTime;
            if(timer >= attackRate)
            {
                weapon.enabled = true;
                anim.SetTrigger("Attack");
                anim.SetInteger("numAttack", UnityEngine.Random.Range(1, 4));
                timer = 0;
                //weapon.enabled = false;
                StartCoroutine(AttackEnd());
            }
        }
    }

    IEnumerator AttackEnd()
    {
        yield return new WaitForSeconds(anim.GetCurrentAnimatorStateInfo(0).length);
        weapon.enabled = false;
    }

    private void Return()
    {
        if(Vector3.Distance(transform.position, startPoint.position) == 0.0f)
        {
            iTween.RotateTo(gameObject, startPoint.eulerAngles, 1.0f);
            state = EnemyState.Idle;
            anim.SetTrigger("Idle");
        }
        else if(Vector3.Distance(transform.position, startPoint.position) <= moveRange)
        {
            if(Sight())
            {
                state = EnemyState.Move;
                anim.SetTrigger("Move");
            }
        }
    }

    IEnumerator Damage()
    {
        yield return new WaitForSeconds(anim.GetCurrentAnimatorClipInfo(0).Length);

        state = EnemyState.Idle;
        anim.SetTrigger("Idle");
    }

    IEnumerator Die()
    {
        yield return new WaitForSeconds(2.0f);
        gameObject.SetActive(false);
    }

    public bool Sight()
    {
        Vector3 targetDir = (player.position - transform.position).normalized;
        float dist = Vector3.Distance(player.position, transform.position);
        float dot = Vector3.Dot(transform.forward, targetDir);

        float theta = Mathf.Acos(dot) * Mathf.Rad2Deg;

        if (theta <= sightAngle && dist <= findRange)
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
        if (state == EnemyState.Damaged || state == EnemyState.Die) { return; }

        HP -= damage;
        if (HP > damage)
        {
            state = EnemyState.Damaged;
            anim.SetTrigger("Damaged");

            StartCoroutine(Damage());
        }
        else
        {
            HP = 0;
            agent.enabled = false;
            transform.GetComponent<CapsuleCollider>().enabled = false;
            StartCoroutine(Die());
            state = EnemyState.Die;
            anim.SetTrigger("Die");
        }
        Debug.Log(HP);
    }

}
