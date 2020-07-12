using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossInfo : MonoBehaviour
{
    UILabel bossName;
    UISprite bossHPBar;
    public EnemyFSM bossFSM;

    // Start is called before the first frame update
    void Start()
    {
        bossName = transform.GetChild(0).GetComponent<UILabel>();
        bossHPBar = transform.GetChild(1).GetComponent<UISprite>();
         
        bossName.text = bossFSM.transform.gameObject.name;
        bossHPBar.fillAmount = (bossFSM.HP / bossFSM.maxHP);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
