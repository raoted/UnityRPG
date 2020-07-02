using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartSceneManager : MonoBehaviour
{
    public GameObject loginWindow;      //로그인 창
    public GameObject selectCharacter;  //캐릭터 선택창
    public GameObject createCharacter;  //새 캐릭터 생성창

    public UILabel inputId;
    public UILabel inputPw;

    bool isLoginSuccess = false;
    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnLoginRequest()
    {
        Debug.Log("ID : " + inputId.text);
        Debug.Log("PW : " + inputPw.text);
        //이거 서버 구현되면 서버로 전송해야됨
        isLoginSuccess = true;
        loginWindow.SetActive(false);
        selectCharacter.SetActive(true);
    }
}
