using System;
using System.Reflection.Emit;
using UnityEngine;
using UnityEngine.UI;

public class Volume : MonoBehaviour
{
    public GameObject master;
    public GameObject bgm;
    public GameObject se;

    UISlider masterSlider;
    UISlider bgmSlider;
    UISlider seSlider;

    UILabel masterLabel;
    UILabel bgmLabel;
    UILabel seLabel;

    // Update is called once per frame
    void Start()
    {
        masterSlider = master.transform.GetChild(1).GetComponent<UISlider>();
        bgmSlider = bgm.transform.GetChild(1).GetComponent<UISlider>();
        seSlider = se.transform.GetChild(1).GetComponent<UISlider>();

        masterLabel = master.transform.GetChild(0).GetComponent<UILabel>();
        bgmLabel = bgm.transform.GetChild(0).GetComponent<UILabel>();
        seLabel = se.transform.GetChild(0).GetComponent<UILabel>();

        masterSlider.value = GameManager.Instance.MasterVolume;
        bgmSlider.value = GameManager.Instance.BGMVolume;
        seSlider.value = GameManager.Instance.SEVolume;

        masterLabel.text = "Master : " + Mathf.Floor(masterSlider.value * 100) + "%";
        bgmLabel.text = "Master : " + Mathf.Floor(bgmSlider.value * 100) + "%";
        seLabel.text = "Master : " + Mathf.Floor(seSlider.value * 100) + "%";
    }
    void Update()
    {
        if (masterSlider.value != GameManager.Instance.MasterVolume)
        {
            masterLabel.text = "Master : " + Mathf.Floor(masterSlider.value * 100) + "%";
            GameManager.Instance.MasterVolume = masterSlider.value;
        }

        if (bgmSlider.value != GameManager.Instance.BGMVolume)
        {
            bgmLabel.text = "BGM : " + Mathf.Floor(bgmSlider.value * 100) + "%";
            GameManager.Instance.BGMVolume = bgmSlider.value;
        }

        if (seSlider.value != GameManager.Instance.SEVolume)
        {
            seLabel.text = "SE : " + Mathf.Floor(seSlider.value * 100) + "%";
            GameManager.Instance.SEVolume = seSlider.value;
        }
    }
}
