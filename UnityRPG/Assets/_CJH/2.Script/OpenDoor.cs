using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpenDoor : MonoBehaviour
{
    public bool isSlide = true;
    public int angle = 0;
    private bool isAttach = false;

    private void Update()
    {
        if (isAttach && Input.GetKeyDown(KeyCode.E) && !GameManager.Instance.bossInfo)
        {
            if (isSlide) { iTween.MoveTo(gameObject, transform.position + new Vector3(0, 3, 0), 5); }
            else { iTween.RotateTo(gameObject, new Vector3(transform.rotation.x, angle, transform.rotation.z), 5); }
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.transform.tag == "Player")
        {
            isAttach = true;
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if(other.transform.tag == "Player")
        {
            isAttach = false;
        }
    }
}
