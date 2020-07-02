using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIActionBar : MonoBehaviour
{
    public GameObject player;
    private PlayerStatus playerStatus;
    public UISprite leftGloves;
    public UISprite rightGloves;
    public UISprite expBar;
    public List<RnMUI_SpellSlot> spellSlot;

    // Start is called before the first frame update
    void Start()
    {
        playerStatus = player.GetComponent<PlayerStatus>();

        for (int i = 0; i < 12; i++)
        {
            spellSlot.Add(transform.GetChild(0).GetChild(0).GetChild(i).GetComponent<RnMUI_SpellSlot>());
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1)) 
        {
            playerStatus.UseMP(spellSlot[0].GetComponent<RnMUI_SpellSlot>().GetSpellInfo().ID);
            spellSlot[0].GetComponent<RnMUI_SpellSlot>().OnClick();

        }
        else if (Input.GetKeyDown(KeyCode.Alpha2)) 
        {
            playerStatus.UseMP(spellSlot[1].GetComponent<RnMUI_SpellSlot>().GetSpellInfo().ID); 
        }
        else if (Input.GetKeyDown(KeyCode.Alpha3)) 
        {
            playerStatus.UseMP(spellSlot[2].GetComponent<RnMUI_SpellSlot>().GetSpellInfo().ID); 
        }
        else if (Input.GetKeyDown(KeyCode.Alpha4)) 
        {
            playerStatus.UseMP(spellSlot[3].GetComponent<RnMUI_SpellSlot>().GetSpellInfo().ID); 
        }
        else if (Input.GetKeyDown(KeyCode.Alpha5)) 
        {
            playerStatus.UseMP(spellSlot[4].GetComponent<RnMUI_SpellSlot>().GetSpellInfo().ID); 
        }
        else if (Input.GetKeyDown(KeyCode.Alpha6)) 
        {
            playerStatus.UseMP(spellSlot[5].GetComponent<RnMUI_SpellSlot>().GetSpellInfo().ID); 
        }
        else if (Input.GetKeyDown(KeyCode.Alpha7)) 
        {
            playerStatus.UseMP(spellSlot[6].GetComponent<RnMUI_SpellSlot>().GetSpellInfo().ID); 
        }
        else if (Input.GetKeyDown(KeyCode.Alpha8)) 
        {
            playerStatus.UseMP(spellSlot[7].GetComponent<RnMUI_SpellSlot>().GetSpellInfo().ID); 
        }
        else if (Input.GetKeyDown(KeyCode.Alpha9)) 
        {
            playerStatus.UseMP(spellSlot[8].GetComponent<RnMUI_SpellSlot>().GetSpellInfo().ID); 
        }
        else if (Input.GetKeyDown(KeyCode.Alpha0)) 
        {
            playerStatus.UseMP(spellSlot[9].GetComponent<RnMUI_SpellSlot>().GetSpellInfo().ID); 
        }
        else if (Input.GetKeyDown(KeyCode.Minus))
        {
            playerStatus.UseMP(spellSlot[10].GetComponent<RnMUI_SpellSlot>().GetSpellInfo().ID);
        }
        else if (Input.GetKeyDown(KeyCode.Equals)) 
        {
            spellSlot[11].GetComponent<RnMUI_SpellSlot>();
            playerStatus.UseMP(spellSlot[11].GetComponent<RnMUI_SpellSlot>().GetSpellInfo().ID); 
        }

        leftGloves.fillAmount = (float)playerStatus.HP / (float)playerStatus.MaxHP;
        rightGloves.fillAmount = (float)playerStatus.MP / (float)playerStatus.MaxMP;
        expBar.fillAmount = (float)playerStatus.EXP / (float) playerStatus.MaxEXP;
    }
}
