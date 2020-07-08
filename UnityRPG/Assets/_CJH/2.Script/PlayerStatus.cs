using System.Collections;
using System.Collections.Generic;
using System.Reflection.Emit;
using UnityEngine;

public class PlayerStatus : MonoBehaviour
{
    public NewUI_SpellDatabase spellData;
    #region"스테이터스"

    private float maxHp = 1;      //최대 생명력
    public float MaxHP
    {
        get { return maxHp; }
    }

    private float hp = 1; //생명력
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
    private int strength = 1;   //힘
    public int Strength
    {
        get { return strength; }
    }
    private int intellect = 1;  //지능
    public int Intellect
    {
        get { return intellect; }
    }
    private int agility = 1;    //민첩
    public int Agility
    {
        get { return agility; }
    }    
    private int sprite = 1;     //정신
    public int Sprite
    {
        get { return sprite; }
    }
    private int stamina = 1;    //스테미나
    public int Stamina
    {
        get { return stamina; }
    }
    private float block = 1;    //방어확률
    public float Block
    {
        get { return block; }
    }
    private float regeneration = 1; //생명력 재생량
    public float Regeneration
    {
        get { return regeneration; }
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
    private int armor = 1;          //방어력
    public int Armor
    {
        get { return armor; }
    }
    private float critical = 1;     //치명타 확률
    public float Critical
    {
        get { return critical; }
    }
    private float penetration = 1;  //관통확률
    public float Penetration
    {
        get { return penetration; }
    }
    private float criDamage = 1;    //치명타 데미지 비율
    public float CriDamage
    {
        get { return criDamage; }
    }
    private float resilience = 1;    //회피확률
    public float Resilience
    {
        get { return resilience; }
    }
    #endregion

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
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
