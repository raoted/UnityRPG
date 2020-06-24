using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeSight : MonoBehaviour
{
    public Transform 전투시야;
    public Transform 마을시야;

    // Start is called before the first frame update
    void Start()
    {
        transform.position = 전투시야.transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Alpha1)) { transform.position = 전투시야.position; }
        if(Input.GetKeyDown(KeyCode.Alpha2)) 
        {
            transform.position = new Vector3(마을시야.position.x, 마을시야.position.y+2, 마을시야.position.z - 10); 
        }
    }
}
