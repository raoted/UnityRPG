using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerRotate : MonoBehaviour
{
    float angleX;
    // Update is called once per frame
    void Update()
    {

        angleX += Input.GetAxis("Mouse X") * 220.0f * Time.deltaTime;

        transform.eulerAngles = new Vector3(0, angleX, 0);
    }
}
