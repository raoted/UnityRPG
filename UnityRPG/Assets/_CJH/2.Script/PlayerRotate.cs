using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerRotate : MonoBehaviour
{
    public UIJoystick joystick;
    float angleX;
    // Update is called once per frame
    void Update()
    {
        if (GameManager.Instance.MobileMode)
        {
            angleX += joystick.joyStickPosX * 220.0f * Time.deltaTime;
        }
        else
        {
            angleX += Input.GetAxis("Mouse X") * 220.0f * Time.deltaTime;
        }
        transform.eulerAngles = new Vector3(0, angleX, 0);
    }
}
