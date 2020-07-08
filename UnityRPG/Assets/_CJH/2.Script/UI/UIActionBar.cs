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
        if (Input.GetKeyDown(KeyCode.Alpha1) && spellSlot[0].GetSpellInfo() != null)
        {
            if (player.GetComponent<Player>().state == Player.PlayerState.Idle
                && player.GetComponent<PlayerStatus>().UseMP(spellSlot[0].GetSpellInfo().PowerCost))
            {
                player.GetComponent<Player>().GetSkill(spellSlot[0].GetSpellInfo().ID, spellSlot[0].GetSpellInfo().CastTime);
                spellSlot[0].OnClick();
            }
        }
        else if (Input.GetKeyDown(KeyCode.Alpha2) && spellSlot[1].GetSpellInfo() != null)
        {
            playerStatus.UseMP(spellSlot[1].GetSpellInfo().ID);
        }
        else if (Input.GetKeyDown(KeyCode.Alpha3) && spellSlot[2].GetSpellInfo() != null)
        {
            playerStatus.UseMP(spellSlot[2].GetSpellInfo().ID);
        }
        else if (Input.GetKeyDown(KeyCode.Alpha4) && spellSlot[3].GetSpellInfo() != null)
        {
            playerStatus.UseMP(spellSlot[3].GetSpellInfo().ID);
        }
        else if (Input.GetKeyDown(KeyCode.Alpha5) && spellSlot[4].GetSpellInfo() != null)
        {
            playerStatus.UseMP(spellSlot[4].GetSpellInfo().ID);
        }
        else if (Input.GetKeyDown(KeyCode.Alpha6) && spellSlot[5].GetSpellInfo() != null)
        {
            playerStatus.UseMP(spellSlot[5].GetSpellInfo().ID);
        }
        else if (Input.GetKeyDown(KeyCode.Alpha7) && spellSlot[6].GetSpellInfo() != null)
        {
            playerStatus.UseMP(spellSlot[6].GetSpellInfo().ID);
        }
        else if (Input.GetKeyDown(KeyCode.Alpha8) && spellSlot[7].GetSpellInfo() != null)
        {
            playerStatus.UseMP(spellSlot[7].GetSpellInfo().ID);
        }
        else if (Input.GetKeyDown(KeyCode.Alpha9) && spellSlot[8].GetSpellInfo() != null)
        {
            playerStatus.UseMP(spellSlot[8].GetSpellInfo().ID);
        }
        else if (Input.GetKeyDown(KeyCode.Alpha0) && spellSlot[9].GetSpellInfo() != null)
        {
            playerStatus.UseMP(spellSlot[9].GetSpellInfo().ID);
        }
        else if (Input.GetKeyDown(KeyCode.Minus) && spellSlot[10].GetSpellInfo() != null)
        {
            playerStatus.UseMP(spellSlot[10].GetSpellInfo().ID);
        }
        else if (Input.GetKeyDown(KeyCode.Equals) && spellSlot[11].GetSpellInfo() != null)
        {
            playerStatus.UseMP(spellSlot[11].GetSpellInfo().ID);
        }

        leftGloves.fillAmount = playerStatus.HP / playerStatus.MaxHP;
        rightGloves.fillAmount = (playerStatus.MP/playerStatus.MaxMP);
        expBar.fillAmount = playerStatus.EXP / playerStatus.MaxEXP;
    }
}
