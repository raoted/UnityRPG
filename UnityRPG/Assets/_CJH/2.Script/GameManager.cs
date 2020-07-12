using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    public GameObject SpawnPoint;
    public GameObject FootMan;
    public GameObject Boss;
    public UIManager UI;
    //BossInterface 보여주기
    public bool bossInfo = false;
    private bool windowOpen = false;
    public bool WindowOpen
    {
        get { return windowOpen; }
        set { windowOpen = value; }
    }
    private void Awake()
    {
        Instance = this;

        //if(PlayerPrefs.HasKey("MasterVolume")) { Master_Volume = PlayerPrefs.GetFloat("MasterVolume"); }
        //else { Master_Volume = 1.0f; }
        //if(PlayerPrefs.HasKey("BGMVolume")) { BGM_Volume = PlayerPrefs.GetFloat("BGMVolume"); }
        //else { BGM_Volume = 1.0f; }
        //if(PlayerPrefs.HasKey("SEVolume")) { SE_Volume = PlayerPrefs.GetFloat("SEVolume"); }
        //else { SE_Volume = 1.0f; }
    }
    private void Start()
    {
        //for(int i = 0; i < SpawnPoint.transform.childCount; i++)
        //{
        //   Instantiate(FootMan, SpawnPoint.transform.GetChild(i).transform);
        //}
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

    public void BossSpawn()
    {
        bossInfo = true;
        Boss.SetActive(true);
        UI.BossUIEnable(bossInfo);
    }
}
