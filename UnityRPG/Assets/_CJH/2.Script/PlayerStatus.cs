using System.Collections;
using System.Collections.Generic;
using System.Reflection.Emit;
using UnityEngine;

public class PlayerStatus : MonoBehaviour
{
    public NewUI_SpellDatabase spellData;
    #region"스테이터스"

    private int maxHp = 1;      //최대 생명력
    public int MaxHP
    {
        get { return maxHp; }
    }

    private int hp = 1; //생명력
    public int HP
    {
        get { return hp; }
    }
    public int Damaged
    {
        set 
        {
            if(value > hp) { hp = 0; }
            else { hp -= value; }
        }
    }
    public int Heal
    {
        set
        {
            hp += value;
            if(hp > MaxHP) { hp = MaxHP; }
        }
    }

    private int maxMp = 1;      //최대 마력
    public int MaxMP
    {
        get { return maxMp; }
    }
    private int mp = 1;         //마력
    public int MP
    {
        get { return mp; }
    }
    private int exp = 1;        //경험치
    public int EXP
    {
        get { return exp; }
    }
    public int AddExp
    {
        set { exp += value; }
    }
    private int maxExp = 100;     //최대 경험치
    public int MaxEXP
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

    public void UseMP(int i)
    {
        int cost;
        if (spellData.GetByID(i) != null)
        {
            cost = (int)spellData.GetByID(i).PowerCost;

            if (cost <= MP)
            {
                this.mp -= cost;
                transform.GetComponent<Player>().skillID = i;
            }
        }
    }
}
