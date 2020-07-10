using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EntireBossRoom : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if(other.transform.tag == "Player")
        {
            GameManager.Instance.bossInfo = true;
        }
    }


}
