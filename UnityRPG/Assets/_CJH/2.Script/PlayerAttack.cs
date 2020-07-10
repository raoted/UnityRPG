using System.Collections;
using System.Collections.Generic;
using System.Net;
using UnityEngine;
using UnityScript.Scripting.Pipeline;

public class PlayerAttack : MonoBehaviour
{
    Player player;
    public GameObject baseFactory;
    [SerializeField]Queue<GameObject> baseAttack = new Queue<GameObject>();
    int baseCount = 10;
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
            baseAttack.Enqueue(bullet);
        }
    }

    private void Update()
    {
        if(player.pState == Player.PlayerState.Idle || player.pState == Player.PlayerState.Move)
        {
            if (Input.GetMouseButtonDown(0))
            {
                player.pState = Player.PlayerState.CastEnd;
                player.StartCoroutine(player.Attack(Random.Range(1, 6)));
                BaseAttack();
            }
        }
    }
    // Update is called once per frame
    public void BaseAttack()
    {
        if(baseAttack.Count == 0)
        {
            baseAttack.Enqueue(baseFactory);
            baseCount++;
        }
        GameObject fire = Instantiate(baseFactory);
        attackCount = Random.Range(0, attackPoint.Length - 1);
        
        fire.transform.position = attackPoint[attackCount].transform.position;
        fire.transform.rotation = attackPoint[attackCount].transform.rotation;
        fire.SetActive(true);
    }
}
