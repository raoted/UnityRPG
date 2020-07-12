using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EntireBossRoom : MonoBehaviour
{
    public GameObject DoorLeft;
    public GameObject DoorRight;
    private void OnTriggerExit(Collider other)
    {
        if(!GameManager.Instance.bossInfo && other.transform.tag == "Player")
        {
            iTween.RotateTo(DoorLeft, new Vector3(0, 180, 0), 2);
            iTween.RotateTo(DoorRight, new Vector3(0, 180, 0), 2);
            GameManager.Instance.BossSpawn();
        }
    }


}
