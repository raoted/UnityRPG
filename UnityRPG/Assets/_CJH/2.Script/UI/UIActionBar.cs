using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIActionBar : MonoBehaviour
{
    public PlayerStatus player;
    public UISprite leftGloves;
    public UISprite rightGloves;
    public UISprite expBar;
    // Start is called before the first frame update

    // Update is called once per frame
    void Update()
    {
        leftGloves.fillAmount = (float)player.HP / (float)player.MaxHP;
        rightGloves.fillAmount = (float)player.MP / (float)player.MaxMP;
        expBar.fillAmount = (float)player.EXP / (float) player.MaxEXP;
    }
}
