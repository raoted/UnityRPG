using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossInfo : MonoBehaviour
{
    UILabel bossName;
    UISprite bossHPBar;
    EnemyFSM bossFSM;

    // Start is called before the first frame update
    void Start()
    {
        bossFSM = GameObject.FindWithTag("Boss").GetComponent<EnemyFSM>();
        bossName.text = bossFSM.transform.gameObject.name;
        bossHPBar.fillAmount = (bossFSM.HP / bossFSM.maxHP);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
