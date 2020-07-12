using System.Collections;
using UnityEngine;


public class Player : MonoBehaviour
{
    public enum PlayerState
    {
        Idle,
        Move,
        Run,
        Casting,
        CastEnd,
        Damaged,
        Die
    }
    private PlayerState state = PlayerState.Idle;
    public PlayerState pState
    {
        get { return state; }
        set { state = value; }
    }

    private bool isBattle;
    public bool IsBattle
    {
        get { return isBattle; }
    }

    Animator anim;
    CharacterController cc;

    private int skillID;
    private int castedSkill;
    private float castTime = 0;
    //0일 경우 : buff, 1일 경우 : Active, 2일 경우 : Passive
    [SerializeField] private int skillType;
    //마법 시전시간이 필요한가?
    [SerializeField] private bool isCasting;
    //어떤 종류의 캐스팅 애니메이션을 사용할건가?
    [SerializeField] private int numCasting;

    #region "Move일 때 사용할 변수"
    #endregion
    float moveX = 0.0f;
    float moveZ = 0.0f;

    static float moveSpeed = 5.0f;
    public float MoveSpeed
    {
        get { return moveSpeed; }
        set { moveSpeed = value; }
    }

    void Start()
    {
        isBattle = true;
        castedSkill = 0;
        cc = GetComponent<CharacterController>();
        anim = GetComponent<Animator>();
        state = PlayerState.Idle;
        if (isBattle) { anim.SetBool("isBattle", isBattle); }
        else { anim.SetBool("isBattle", isBattle); }
        anim.SetTrigger("Idle");
    }
    // Update is called once per frame
    void Update()
    {
        moveX = Input.GetAxis("Horizontal");
        moveZ = Input.GetAxis("Vertical");
        switch (state)
        {
            case PlayerState.Idle:
                Idle();
                break;
            case PlayerState.Move:
                Move();
                break;
            case PlayerState.Run:
                Run();
                break;
            case PlayerState.Casting:
                //StartCoroutine(Casting());
                break;
            case PlayerState.Damaged:
                //
                break;
            case PlayerState.Die:
                Die();
                break;
            default:
                break;
        }
    }

    private void Idle()
    {
        if (moveX != 0 || moveZ != 0)
        {
            anim.ResetTrigger("Idle");

            anim.SetFloat("moveX", moveX);
            anim.SetFloat("moveZ", moveZ);
            if (Input.GetKey(KeyCode.LeftShift))
            {
                state = PlayerState.Run;
            }
            anim.SetTrigger("Move");
            state = PlayerState.Move;
        }
    }

    private void Move()
    {
        anim.SetFloat("moveX", moveX);
        anim.SetFloat("moveZ", moveZ);
        if (moveX == 0.0f && moveZ == 0.0f)
        {
            anim.ResetTrigger("Move");
            anim.SetTrigger("Idle");
            state = PlayerState.Idle;
        }
        else
        {
            if (Input.GetKey(KeyCode.LeftShift))
            {
                state = PlayerState.Run;
                return;
            }

            float h = moveX * MoveSpeed * Time.deltaTime;
            float v = moveZ * MoveSpeed * Time.deltaTime;

            Vector3 dir = new Vector3(h, 0, v);
            dir = Camera.main.transform.TransformDirection(dir);
            dir.Normalize();
            cc.SimpleMove(dir);
        }
    }

    private void Run()
    {
        anim.SetFloat("moveX", moveX);
        anim.SetFloat("moveZ", moveZ);

        if (moveX == 0.0f && moveZ == 0.0f)
        {
            anim.SetTrigger("Idle");
            state = PlayerState.Idle;
            return;
        }
        if (!Input.GetKey(KeyCode.LeftShift))
        {
            state = PlayerState.Move;
            return;
        }

        float h = moveX * MoveSpeed * Time.deltaTime;
        float v = moveZ * MoveSpeed * Time.deltaTime;

        Vector3 dir = new Vector3(h, 0, v);
        dir = Camera.main.transform.TransformDirection(dir);
        dir.Normalize();
        cc.SimpleMove(dir * 2);
    }

    IEnumerator Casting()
    {
        Debug.Log(castTime);
        if(isCasting) { yield return new WaitForSeconds(castTime); }
        else { yield return new WaitForSeconds(0); }

        state = PlayerState.CastEnd;
        anim.SetTrigger("Attack");

        if (skillID % 1000 == 3) { castedSkill = 0; }
        StartCoroutine(CastEnd());
    }

    public IEnumerator CastEnd()
    {
        yield return new WaitForSeconds(anim.GetCurrentAnimatorClipInfo(0).Length);

        state = PlayerState.Idle;
        anim.SetTrigger("Idle");
    }

    public IEnumerator Attack(int i)
    {
        anim.SetTrigger("Attack");
        anim.SetInteger("numCasting", i+1);
        state = PlayerState.CastEnd;

        yield return new WaitForSeconds(anim.GetCurrentAnimatorStateInfo(0).length);
        state = PlayerState.Idle;
        anim.SetTrigger("Idle");
    }


    IEnumerator Damaged()
    {
        anim.SetTrigger("Attack");
        state = PlayerState.CastEnd;

        yield return new WaitForSeconds(anim.GetCurrentAnimatorClipInfo(0).Length);
        
        state = PlayerState.Idle;
        anim.SetTrigger("Idle");
    }

    private void Die()
    {

    }

    public void GetSkill(int id, float casttime)
    {
        skillID = id;
        Debug.Log(skillID);
        castTime = casttime;

        skillType = id / 1000;

        if ((id % 1000) / 100 == 0) { isCasting = false; }
        else { isCasting = true; }

        numCasting = (id % 100) / 10;

        Debug.Log(numCasting);
        if (skillType == 0)
        {
            anim.SetBool("isBuff", true);
            state = PlayerState.Casting;
        }
        else if (skillType == 1)
        {
            anim.SetInteger("numCasting", numCasting);
            anim.SetTrigger("Casting");
            state = PlayerState.Casting;
        }
        else
        {
            return;
        }
    }

    public void Attacked(float damage)
    {
        StopAllCoroutines();
        transform.GetComponent<PlayerStatus>().Damaged = damage;
        if (transform.GetComponent<PlayerStatus>().HP > 0)
        {
            anim.SetTrigger("Damaged");
            state = PlayerState.Damaged;
            StartCoroutine(Damaged());
        }
        else
        {
            anim.SetTrigger("Die");
            state = PlayerState.Die;
        }
    }

    public float ForDebug()
    {
        return moveSpeed;
    }
}
