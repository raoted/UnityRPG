using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    //Sound Volume 관리
    private static float Master_Volume;
    public float MasterVolume
    {
        get { return Master_Volume; }
        set { Master_Volume = value; }
    }

    private static float BGM_Volume;
    public float BGMVolume
    {
        get { return BGM_Volume; }
        set { BGM_Volume = BGMVolume; }
    }
  
    private static float SE_Volume;
    public float SEVolume
    {
        get { return SE_Volume; }
        set { SE_Volume = value; }
    }

    //
    private bool mobileMode = false;
    public bool MobileMode
    {
        get { return mobileMode; }
        set { mobileMode = value; }
    }
    //
    private bool windowOpen = false;
    public bool WindowOpen
    {
        get { return windowOpen; }
        set { windowOpen = value; }
    }
    private void Awake()
    {
        Instance = this;

        if(PlayerPrefs.HasKey("MasterVolume")) { Master_Volume = PlayerPrefs.GetFloat("MasterVolume"); }
        else { Master_Volume = 1.0f; }
        if(PlayerPrefs.HasKey("BGMVolume")) { BGM_Volume = PlayerPrefs.GetFloat("BGMVolume"); }
        else { BGM_Volume = 1.0f; }
        if(PlayerPrefs.HasKey("SEVolume")) { SE_Volume = PlayerPrefs.GetFloat("SEVolume"); }
        else { SE_Volume = 1.0f; }
    }

    private void Update()
    {
        //캐릭터 정보 & 스테이터스
        if(Input.GetKeyDown(KeyCode.C))
        {

        }
        //장비템 정보
        if(Input.GetKeyDown(KeyCode.E))
        {

        }
        //인벤토리
        if (Input.GetKeyDown(KeyCode.I)) 
        {

        }
        //스킬창 처리
        if(Input.GetKeyDown(KeyCode.K))
        {

        }
        //퀘스트 창 처리
        if(Input.GetKeyDown(KeyCode.Q))
        {

        }
    }
    public void OnChangeDivice()
    {
        MobileMode = !MobileMode;
    }

    
}
