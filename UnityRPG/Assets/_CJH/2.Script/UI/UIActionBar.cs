using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIActionBar : MonoBehaviour
{
    public GameObject player;
    private PlayerStatus playerStatus;
    public GameObject leftGloves;
    public GameObject rightGloves;
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
            if ((player.GetComponent<Player>().pState == Player.PlayerState.Idle
                || player.GetComponent<Player>().pState == Player.PlayerState.Move)
                && player.GetComponent<PlayerStatus>().UseMP(spellSlot[0].GetSpellInfo().PowerCost))
            {
                player.GetComponent<Player>().GetSkill(spellSlot[0].GetSpellInfo().ID, spellSlot[0].GetSpellInfo().CastTime);
                spellSlot[0].OnClick();
            }
        }
        else if (Input.GetKeyDown(KeyCode.Alpha2) && spellSlot[1].GetSpellInfo() != null)
        {
            if ((player.GetComponent<Player>().pState == Player.PlayerState.Idle
                            || player.GetComponent<Player>().pState == Player.PlayerState.Move)
                            && player.GetComponent<PlayerStatus>().UseMP(spellSlot[1].GetSpellInfo().PowerCost))
            {
                player.GetComponent<Player>().GetSkill(spellSlot[1].GetSpellInfo().ID, spellSlot[1].GetSpellInfo().CastTime);
                spellSlot[1].OnClick();
            }
        }
        else if (Input.GetKeyDown(KeyCode.Alpha3) && spellSlot[2].GetSpellInfo() != null)
        {
            if ((player.GetComponent<Player>().pState == Player.PlayerState.Idle
                || player.GetComponent<Player>().pState == Player.PlayerState.Move)
                && player.GetComponent<PlayerStatus>().UseMP(spellSlot[2].GetSpellInfo().PowerCost))
            {
                player.GetComponent<Player>().GetSkill(spellSlot[2].GetSpellInfo().ID, spellSlot[2].GetSpellInfo().CastTime);
                spellSlot[2].OnClick();
            }
        }
        else if (Input.GetKeyDown(KeyCode.Alpha4) && spellSlot[3].GetSpellInfo() != null)
        {
            if ((player.GetComponent<Player>().pState == Player.PlayerState.Idle
                || player.GetComponent<Player>().pState == Player.PlayerState.Move)
                && player.GetComponent<PlayerStatus>().UseMP(spellSlot[3].GetSpellInfo().PowerCost))
            {
                player.GetComponent<Player>().GetSkill(spellSlot[3].GetSpellInfo().ID, spellSlot[3].GetSpellInfo().CastTime);
                spellSlot[3].OnClick();
            }
        }
        else if (Input.GetKeyDown(KeyCode.Alpha5) && spellSlot[4].GetSpellInfo() != null)
        {
            if ((player.GetComponent<Player>().pState == Player.PlayerState.Idle
                || player.GetComponent<Player>().pState == Player.PlayerState.Move)
                && player.GetComponent<PlayerStatus>().UseMP(spellSlot[4].GetSpellInfo().PowerCost))
            {
                player.GetComponent<Player>().GetSkill(spellSlot[4].GetSpellInfo().ID, spellSlot[4].GetSpellInfo().CastTime);
                spellSlot[4].OnClick();
            }
        }
        else if (Input.GetKeyDown(KeyCode.Alpha6) && spellSlot[5].GetSpellInfo() != null)
        {
            if ((player.GetComponent<Player>().pState == Player.PlayerState.Idle
                || player.GetComponent<Player>().pState == Player.PlayerState.Move)
                && player.GetComponent<PlayerStatus>().UseMP(spellSlot[5].GetSpellInfo().PowerCost))
            {
                player.GetComponent<Player>().GetSkill(spellSlot[5].GetSpellInfo().ID, spellSlot[5].GetSpellInfo().CastTime);
                spellSlot[5].OnClick();
            }
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

        leftGloves.transform.GetChild(0).GetComponent<UISprite>().fillAmount = playerStatus.HP / playerStatus.MaxHP;
        leftGloves.transform.GetChild(3).GetComponent<UILabel>().text = playerStatus.HP + "/" + playerStatus.MaxHP;

        rightGloves.transform.GetChild(0).GetComponent<UISprite>().fillAmount = (playerStatus.MP/playerStatus.MaxMP);
        rightGloves.transform.GetChild(3).GetComponent<UILabel>().text = playerStatus.MP + "/" + playerStatus.MaxMP;
        
        expBar.fillAmount = playerStatus.EXP / playerStatus.MaxEXP;
    }
}
