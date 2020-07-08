using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAttack : MonoBehaviour
{
    public EnemyStatus user;

    private void OnTriggerEnter(Collider trigger)
    {
        if(trigger.transform.tag == "Player")
        {
            trigger.gameObject.GetComponent<Player>().Attacked(2);
        }
    }
}
