using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//using UnityEngine.Scripting.APIUpdating;


public class Player : MonoBehaviour
{
    public enum PlayerState
    {
        Idle,
        Move,
        Run,
        Spell,
        Damaged,
        Die
    }

    public UIJoystick joystick;
    Animator anim;
    CharacterController cc;
    public int skillID = 0;
    public PlayerState state = PlayerState.Idle;

    

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
        cc = GetComponent<CharacterController>();
        anim = GetComponent<Animator>();
        state = PlayerState.Idle;
        anim.SetTrigger("Idle");
    }
    // Update is called once per frame
    void Update()
    {
        if(GameManager.Instance.MobileMode)
        {
            moveX = joystick.joyStickPosX;
            moveZ = joystick.joyStickPosY;
        }
        else
        {
            moveX = Input.GetAxis("Horizontal");
            moveZ = Input.GetAxis("Vertical");
        }

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
            case PlayerState.Spell:
                Attack();
                break;
            case PlayerState.Damaged:
                Damaged();
                break;
            case PlayerState.Die:
                Die();
                break;
            default:
                break;
        }
    }

    private void CheckMoveX()
    {
        if (moveX > 0.1f)
        {
            anim.SetBool("Left", false);
            anim.SetBool("Right", true);
        }
        else if (moveX < -0.1f)
        {
            anim.SetBool("Left", true);
            anim.SetBool("Right", false);
        }
        else
        {
            anim.SetBool("Left", false);
            anim.SetBool("Right", false);
        }
    }
    private void CheckMoveZ()
    {
        if (moveZ > 0.1f)
        {
            anim.SetBool("Front", true);
            anim.SetBool("Back", false);
        }
        else if(moveZ < -0.1f)
        {
            anim.SetBool("Front", false);
            anim.SetBool("Back", true);
        }
        else
        {
            anim.SetBool("Front", false);
            anim.SetBool("Back", false);
        }
    }
    private void Idle()
    {
        if (moveX != 0 || moveZ != 0) 
        {
            if(Input.GetKey(KeyCode.LeftShift))
            {
                state = PlayerState.Run;
                anim.SetBool("Run", true);
            }
            state = PlayerState.Move;
            anim.SetTrigger("Walk");
        }
    }

    private void Move()
    {
        if(Input.GetKey(KeyCode.LeftShift))
        {
            state = PlayerState.Run;
            anim.SetBool("Run", true);
        }
        CheckMoveX();
        CheckMoveZ();
        float h = moveX * MoveSpeed * Time.deltaTime;
        float v = moveZ * MoveSpeed * Time.deltaTime;


        Vector3 dir = new Vector3(h, 0, v);
        //dir = Camera.main.transform.TransformDirection(dir);
        dir.Normalize();
        cc.SimpleMove(dir);

        if (moveX == 0f && moveZ == 0f) 
        {
            state = PlayerState.Idle;
            anim.SetTrigger("Idle");
        }
    }

    private void Run()
    {
        if (!Input.GetKey(KeyCode.LeftShift))
        {
            state = PlayerState.Move;
            anim.SetTrigger("Walk");
        }
        else if(moveX == 0f && moveZ == 0f)
        {
            state = PlayerState.Idle;
            anim.SetTrigger("Idle");
        }
        CheckMoveX();
        CheckMoveZ();
    }

    private void Attack()
    {

    }

    private void Damaged()
    {

    }

    private void Die()
    {

    }
}
