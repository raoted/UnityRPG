using System;
using System.Reflection.Emit;
using UnityEngine;
using UnityEngine.UI;

public class Volume : MonoBehaviour
{
    public GameObject Master;
    public GameObject BGM;
    public GameObject SE;

    UISlider M_Slider;
    UISlider B_Slider;
    UISlider S_Slider;

    UILabel M_Label;
    UILabel B_Label;
    UILabel S_Label;

    // Update is called once per frame
    void Start()
    {
        M_Slider = Master.transform.GetChild(1).GetComponent<UISlider>();
        B_Slider = BGM.transform.GetChild(1).GetComponent<UISlider>();
        S_Slider = SE.transform.GetChild(1).GetComponent<UISlider>();

        M_Label = Master.transform.GetChild(0).GetComponent<UILabel>();
        B_Label = BGM.transform.GetChild(0).GetComponent<UILabel>();
        S_Label = SE.transform.GetChild(0).GetComponent<UILabel>();

        M_Slider.value = SoundManager.instance.mValue;
        B_Slider.value = SoundManager.instance.bValue;
        S_Slider.value = SoundManager.instance.sValue;
    }
    void Update()
    {
        M_Label.text = "Master : " + string.Format("{0:0}", M_Slider.value * 100) + "%";
        B_Label.text = "BGM : " + string.Format("{0:0}", B_Slider.value * 100) + "%";
        S_Label.text = "SE : " + string.Format("{0:0}", S_Slider.value * 100) + "%";
    }

    public void CloseOption()
    {
        gameObject.SetActive(false);
        SoundManager.instance.SaveValue(M_Slider.value, B_Slider.value, S_Slider.value);
        StartSceneButtonManager.instance.AllButtonControl(true);
    }
}
