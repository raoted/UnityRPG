using System.Collections;
using System.Collections.Generic;
using System.Reflection.Emit;
using UnityEngine;

public class PlayerStatus : MonoBehaviour
{
    #region"스테이터스"

    private float maxHp = 1;      //최대 생명력
    public float MaxHP
    {
        get { return maxHp; }
        set
        {
            maxHp = value;
            hp = maxHp;
        }
    }

    private float hp; //생명력
    public float HP
    {
        get { return hp; }
    }
    public float Damaged
    {
        set 
        {
            if(value > hp) { hp = 0; }
            else { hp -= value; }
        }
    }
    public float Heal
    {
        set
        {
            hp += value;
            if(hp > MaxHP) { hp = MaxHP; }
        }
    }

    private float maxMp = 50;      //최대 마력
    public float MaxMP
    {
        get { return maxMp; }
        set
        {
            maxMp = value;
            mp = value;
        }
    }
    private float mp = 50;         //마력
    public float MP
    {
        get { return mp; }
    }
    private float exp = 1;        //경험치
    public float EXP
    {
        get { return exp; }
    }
    public int AddExp
    {
        set { exp += value; }
    }
    private float maxExp = 100;     //최대 경험치
    public float MaxEXP
    {
        get { return maxExp; }
    }
    private int strength = 10;   //힘
    public int Strength
    {
        get { return strength; }
    }
    private int intellect = 30;  //지능
    public int Intellect
    {
        get { return intellect; }
    }
    private int agility = 15;    //민첩
    public int Agility
    {
        get { return agility; }
    }    
    private int sprite = 25;     //정신
    public int Sprite
    {
        get { return sprite; }
    }
    private int stamina = 20;    //스테미나
    public int Stamina
    {
        get { return stamina; }
    }
    private float block = 3;    //방어확률
    public float Block
    {
        get { return block; }
    }
    private float regeneration = 1; //초당 생명력 재생량
    public float RegeneHP
    {
        get { return regeneration; }
    }
    public float magicAtk = 15;
    public float MagicAtk
    {
        get { return MagicAtk; }
    }
    private float attackSpeed = 1;  //공속
    public float AttackSpeed
    {
        get { return attackSpeed; }
    }
    private float castSpeed = 1;    //캐스팅 속도
    public float CastSpeed
    {
        get { return castSpeed; }
    }
    private int armor = 5;          //방어력
    public int Armor
    {
        get { return armor; }
    }
    private float critical = 0;     //치명타 확률
    public float Critical
    {
        get { return critical; }
    }
    private float penetration = 0;  //관통확률
    public float Penetration
    {
        get { return penetration; }
    }
    private float criDamage = 1.2f;    //치명타 데미지 비율
    public float CriDamage
    {
        get { return criDamage; }
    }
    private float resilience = 10;   //회피확률
    public float Resilience
    {
        get { return resilience; }
    }
    #endregion

    // Start is called before the first frame update
    void Start()
    {
        MaxHP = 700;
        MaxMP = 300;
        StartCoroutine(Regen());
    }

    IEnumerator Regen()
    {
        while(true)
        {
            yield return new WaitForSeconds(1.0f);
            Heal = RegeneHP;
            if (mp >= MaxMP) { mp = MaxMP; }
            else { mp += (Sprite / 5); }
        }
    }

    public void LevelUp()
    {

    }

    public bool UseMP(float cost)
    {
        if (cost <= MP)
        {
            mp -= cost;
            return true;
        }
        else
        {
            return false;
        } 
    }
}
