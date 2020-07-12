using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public static SoundManager instance;

    public float mValue;
    public float bValue;
    public float sValue;

    private void Awake()
    {
        DontDestroyOnLoad(gameObject);
        if(instance == null) { instance = this; }
    }
    // Start is called before the first frame update
    void Start()
    {
        if(PlayerPrefs.HasKey("MasterVolume")) { mValue = PlayerPrefs.GetFloat("MasterVolume"); }
        else
        {
            mValue = 1.0f;
            PlayerPrefs.SetFloat("MasterVolume", mValue);
        }

        if(PlayerPrefs.HasKey("BGMVolume")) { bValue = PlayerPrefs.GetFloat("BGMVolume"); }
        else
        {
            bValue = 1.0f;
            PlayerPrefs.SetFloat("BGMVolume", bValue);
        }

        if(PlayerPrefs.HasKey("SEVolume")) { sValue = PlayerPrefs.GetFloat("SEVolume"); }
        else
        {
            sValue = 1.0f;
            PlayerPrefs.SetFloat("SEVolume", sValue);
        }
    }

    public void SaveValue(float mValue, float bValue, float sValue)
    {
        PlayerPrefs.SetFloat("MasterVolume", mValue);
        PlayerPrefs.SetFloat("BGMVolume", bValue);
        PlayerPrefs.SetFloat("SEVolume", sValue);
    }
}
