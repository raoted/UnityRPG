using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    Player player;
    public GameObject baseFactory;
    [SerializeField]List<GameObject> baseAttack = new List<GameObject>();
    int baseCount = 20;
    [SerializeField]GameObject[] attackPoint;
    int attackCount;

    // Start is called before the first frame update
    void Start()
    {
        player = transform.GetComponent<Player>();
        attackPoint = new GameObject[transform.GetChild(5).childCount];
        
        for(int i = 0; i < attackPoint.Length; i++)
        {
            attackPoint[i] = transform.GetChild(5).GetChild(i).gameObject;
        }
        for(int i = 0; i < baseCount; i++)
        {
            GameObject bullet = Instantiate(baseFactory);
            baseAttack.Add(bullet);
        }
    }

    private void Update()
    {
        if(player.pState == Player.PlayerState.Idle)
        {
            if(Input.GetMouseButtonDown(0))
            {
                foreach(GameObject bullet in baseAttack)
                {
                    if(!bullet.activeSelf)
                    {
                        StartCoroutine(player.Attack(Random.Range(0, 2)));

                        int idx = Random.Range(0, attackPoint.Length - 1);

                        bullet.transform.position = attackPoint[idx].transform.position;
                        bullet.transform.rotation = attackPoint[idx].transform.rotation;
                        bullet.SetActive(true);
                        break;
                    }
                }
            }
        }
    }
}
