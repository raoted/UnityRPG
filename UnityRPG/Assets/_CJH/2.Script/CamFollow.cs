using System;
using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using TMPro;
using UnityEngine;
using UnityEngine.Audio;

public class CamFollow : MonoBehaviour
{
    //카메라의 Transform
    private Transform cam;
    //카메라가 따라갈 타겟
    public Transform target;
    //카메라가 타겟을 따라가는 속도
    public float followSpeed = 0.5f;
    //카메라와 타겟의 거리
    public float dist = 2.5f;

    //모바일 환경일 경우 사용할 조이스틱
    public UIJoystick joystick;

    //카메라 초기 위치
    private float x = 0.0f;
    private float y = 0.0f;

    //조이스틱의 값
    private float joyX = 0.0f;
    private float joyY = 0.0f;

    //카메라 회전 속도
    private float xSpeed = 220.0f;
    private float ySpeed = 100.0f;

    //y축 제한
    private float yMinLimit = -10.0f;
    private float yMaxLimit = 40.0f;

    private float prevDist = 0.0f;
    private float currDist = 0.0f;

    float ClampAngle(float angle, float min, float max)
    {
        if(angle < -360) { angle += 360.0f; }
        if(angle > 360) { angle -= 360.0f; }

        return Mathf.Clamp(angle, min, max);
    }
    private void Start()
    {
        //변화 컴포넌트 가져오기
        cam = GetComponent<Transform>();
        //조이스틱 컴포넌트 가져오기
        //joystick = GetComponent<UIJoystick>();

        Vector3 angles = transform.eulerAngles;
        x = angles.y;
        y = angles.x;
    }


    private void LateUpdate()
    {
        if (target)
        {
            if(GameManager.Instance.MobileMode) { MobileCam(); }
            else { PcCam(); }

            
            if (dist < 0.5f) { dist = 0.5f; }
            if (dist > 10.0f) { dist = 10.0f; }

            y = ClampAngle(y, yMinLimit, yMaxLimit);

            Quaternion rotation = Quaternion.Euler(y, x, 0);
            Vector3 position = rotation * new Vector3(0, 2, -dist) + target.position;

            transform.rotation = rotation;
            transform.position = position;
        }
    }

    private void MobileCam()
    {
        //조이스틱 값 획득
        joyX = joystick.joyStickPosX;
        joyY = joystick.joyStickPosY;

        if (joyX >= 0.5f || joyX <= -0.5f) { x += joyX * xSpeed * Time.deltaTime; }
        if (joyY >= 0.5f || joyY <= -0.5f) { y -= joyY * ySpeed * Time.deltaTime; }

        //CheckTouch();
    }

    private void CheckTouch()
    {
        int touchCount = Input.touchCount;

        if (touchCount == 2 && 
            (Input.touches[0].phase == TouchPhase.Moved || 
            Input.touches[1].phase == TouchPhase.Moved))
        {
            dist -= 0.5f * Vector2.Distance(Input.touches[0].position, Input.touches[1].position);
        }
    }

    private void PcCam()
    {
        //조이스틱 값 획득
        joyX = Input.GetAxis("Mouse X");
        joyY = Input.GetAxis("Mouse Y");

        if (joyX >= 0.5f || joyX <= -0.5f) { x += joyX * xSpeed * Time.deltaTime; }
        if (joyY >= 0.5f || joyY <= -0.5f) { y -= joyY * ySpeed * Time.deltaTime; }

    }
}
