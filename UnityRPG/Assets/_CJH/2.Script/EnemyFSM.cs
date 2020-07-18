using System.Collections;
using System.Security.Policy;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UIElements;

public abstract class EnemyFSM : MonoBehaviour
{
    //에너미 상태 enum 선언
    public enum EnemyState
    {
        Idle,
        Move,
        Attack,
        Return,
        Damaged,
        Die
    }

    //NavMeshAgent 변수 선언
    private NavMeshAgent agent;
    //NavMeshAgent getter, setter 선언
    public NavMeshAgent Agent
    {
        get { return agent; }
        set { agent = value; }
    }
    //Animator 변수 선언
    private Animator anim;
    //Animator getter, setter 선언
    public Animator Animator
    {
        get { return anim;}
        set { anim = value; }
    }
    //BoxCollider 변수 선언
    private BoxCollider weapon;
    //BoxCollider getter, setter 선언
    public BoxCollider Weapon
    {
        get { return weapon; }
        set { weapon = value; }
    }

    #region "Move와 Return에 사용할 변수"
    #endregion
    //NavMeshAgent의 목표를 저장할 GameObject 선언
    private GameObject target;
    //target의 getter, setter 선언
    public GameObject Target
    {
        get { return target; }
        set { target = value; }
    }
    //NavMeshAgent의 시작 위치를 저장
    private Transform startPoint;
    //startPoint의 getter, setter 선언
    public Transform StartPoint
    {
        get { return startPoint; }
        set { startPoint = value; }
    }

    #region "Attack에 사용할 변수"
    #endregion
    private float timer = 0.0f;
    public float Timer
    {
        get { return timer; }
        set { timer += value; }
    }


    //시야각
    private float sightAngle = 0.0f;
    public float SightAngle
    {
        get { return sightAngle; }
        set { sightAngle = value; }
    }
    //시야 거리
    private float findRange = 0.0f;
    public float FindRange
    {
        get { return findRange; }
        set { findRange = value; }
    }
    //추적 거리
    float chaseRange = 0.0f;
    public float ChaseRange
    {
        get { return chaseRange; }
        set { chaseRange = value; }
    }
    //공격 범위
    float attackRange = 0.0f;
    public float AttackRange
    {
        get { return attackRange; }
        set { attackRange = value; }
    }
    LayerMask targetLayer = 0;

    // Update is called once per frame


    public abstract void Idle();
    public abstract void Move();
    public abstract void Attack();
    public IEnumerator AttackEnd()
    {
        yield return new WaitForSeconds(anim.GetCurrentAnimatorStateInfo(0).length);
        weapon.enabled = false;
    }
    public abstract void Return();
    public abstract IEnumerator Damaged();
    public void Die()
    {
        StopAllCoroutines();
    }
    public abstract bool Sight();
    public abstract void HitDamage(float damage);
}
