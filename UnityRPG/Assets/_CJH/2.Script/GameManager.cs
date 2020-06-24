using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    private void Awake() => Instance = this;

    private bool mobileMode = false;
    public bool MobileMode
    {
        get { return mobileMode; }
        set { mobileMode = value; }
    }

    // Start is called before the first frame update
    
    public void OnChangeDivice()
    {
        MobileMode = !MobileMode;
    }
}
