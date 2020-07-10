using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Warp : MonoBehaviour
{
    BoxCollider box;
    public GameObject point;
    public GameObject enableMap;
    public GameObject disableMap;
    // Start is called before the first frame update
    void Start()
    {
        box = GetComponent<BoxCollider>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.transform.tag == "Player")
        {
            disableMap.SetActive(false);
            enableMap.SetActive(true);
            other.gameObject.transform.position = point.transform.position;
            other.gameObject.transform.rotation = point.transform.rotation;
            Camera.main.transform.rotation = point.transform.rotation;
        }
    }
}
