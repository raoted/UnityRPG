using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerRotate : MonoBehaviour
{
    float angleX;
    // Update is called once per frame
    void Update()
    {
        transform.eulerAngles = new Vector3(0, Camera.main.transform.eulerAngles.y, 0);
    }
}
