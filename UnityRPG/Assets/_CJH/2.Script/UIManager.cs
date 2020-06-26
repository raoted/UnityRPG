using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    public GameObject Menu;
    public GameObject VolumeSetting;

    public void OnMenuOpen()
    {
        Debug.Log("Pushed");
        if(!Menu.activeSelf && !VolumeSetting.activeSelf)
        {
            Menu.SetActive(true);
        }
    }

    public void OnMenuClose()
    {
        Menu.SetActive(false);
    }

    public void OnVolumeSettingOpen()
    {
        if(!Menu.activeSelf && !VolumeSetting.activeSelf)
        {
            VolumeSetting.SetActive(true);
        }
    }
    public void OnVolumeSubmit()
    {
        PlayerPrefs.SetFloat("MasterVolume", GameManager.Instance.MasterVolume);
        PlayerPrefs.SetFloat("BGMVolume", GameManager.Instance.BGMVolume);
        PlayerPrefs.SetFloat("SEVolume", GameManager.Instance.SEVolume);

        VolumeSetting.SetActive(false);
    }
    public void OnVolumeCancle()
    {
        VolumeSetting.SetActive(false);
    }
}
