using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpenDoor : MonoBehaviour
{
    bool isOpen = false;
    
    private void OnTriggerStay(Collider other)
    {
        if (other.transform.tag == "Player")
        {
            if (Input.GetKeyDown(KeyCode.E))
            {
                iTween.MoveTo(gameObject, transform.position + new Vector3(0, 3, 0), 5);
            }
        }
    }
}
