using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class PlayerAnimation : MonoBehaviour
{
    Animation animation;
    Player player;
    // Start is called before the first frame update
    void Start()
    {
        animation = GetComponent<Animation>();
        player = GetComponent<Player>();
    }

    public void OnChangeAnimation(Player.PlayerState state)
    {
        switch (state)
        {
            case Player.PlayerState.Idle:
                IdleAnimation();
                break;
            case Player.PlayerState.Move:
                MoveAnimation();
                break;
            case Player.PlayerState.Run:
                RunAnimation();
                break;
            case Player.PlayerState.Casting:
                AttackAnimation();
                break;
            case Player.PlayerState.Damaged:
                DamagedAnimation();
                break;
            case Player.PlayerState.Die:
                DieAnimation();
                break;
            default:
                break;
        }
    }

    private void IdleAnimation()
    {
        if(animation.clip.name != "A_Idle_1")
        {
            animation.Stop();
            animation.clip = animation.GetClip("A_Idle_1");
        }
        animation.Play();
    }

    private void MoveAnimation()
    {
        if(animation.clip.name != "A_Run_1")
        {
            animation.Stop();
            animation.clip = animation.GetClip("A_Run_1");
        }
        animation.Play();
    }

    private void RunAnimation()
    {
        if (animation.clip.name != "A_Run_1")
        {
            animation.Stop();
            animation.clip = animation.GetClip("A_Run_1");
        }
        animation.Play();
    }

    private void AttackAnimation()
    {
        if(animation.clip.name != "A_Attack_1")
        {
            animation.clip = animation.GetClip("A_Attack_1");
            animation.Play();
        }
    }

    private void DamagedAnimation()
    {
        throw new NotImplementedException();
    }

    private void DieAnimation()
    {
        
    }
}
