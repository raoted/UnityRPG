using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class StartSceneButtonManager : MonoBehaviour
{
    GameObject OptionWindow;
    [SerializeField] GameObject StartButton;
    [SerializeField] GameObject OptionButton;
    [SerializeField] GameObject ExitButton;

    public static StartSceneButtonManager instance;
    private void Awake() => instance = this;

    void Start()
    {
        OptionWindow = transform.GetChild(1).gameObject;
        
        StartButton = transform.GetChild(0).GetChild(2).gameObject;
        OptionButton = transform.GetChild(0).GetChild(3).gameObject;
        ExitButton = transform.GetChild(0).GetChild(4).gameObject;
    }

    public void OnPressStartButton()
    {
        SceneManager.LoadScene(1);
    }
    public void OnPressOptionButton()
    {
        OptionWindow.SetActive(true);
        AllButtonControl(false);
    }

    public void AllButtonControl(bool b)
    {
        StartButton.GetComponent<UIButton>().enabled = b;
        OptionButton.GetComponent<UIButton>().enabled = b;
        ExitButton.GetComponent<UIButton>().enabled = b;
    }
    public void OnPressExitButton()
    {
        //UnityEditor.EditorApplication.isPlaying = false;
        //빌드시에는 아래 문장 활성화 후 빌드
        Application.Quit();
    }
}
