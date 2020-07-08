using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    public GameObject inventory;
    public GameObject spellBook;
    public GameObject spellFactory;
    public NewUI_SpellDatabase spellDatabase;
    public GameObject table;

    void Start()
    {
        if (table.transform.childCount < spellDatabase.spells.Length-1)
        {
            for (int i = 1; i < spellDatabase.spells.Length; i++)
            {
                GameObject spellRow = NGUITools.AddChild(table, spellFactory);
                spellRow.transform.GetChild(4).GetComponent<RnMUI_Assign_SpellSlot>().assignSpell = spellDatabase.Get(i).ID;
                spellRow.GetComponent<UIWidget>().depth = table.GetComponent<UIWidget>().depth + 1;
            }
        }
    }
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.I))
        {
            if(inventory.activeSelf) 
            {
                inventory.SetActive(false);
                if(AllWindowFalse()) { TimeManager.instance.timeScale = 1; }
            }
            else 
            {
                inventory.SetActive(true);
                TimeManager.instance.timeScale = 0;
            }
        }
        if(Input.GetKeyDown(KeyCode.K))
        {
            if(spellBook.activeSelf) 
            {
                spellBook.SetActive(false);
                if (AllWindowFalse()) { TimeManager.instance.timeScale = 1; }
            }
            else 
            {
                spellBook.SetActive(true);
                TimeManager.instance.timeScale = 0;
            }
        }
    }
    
    bool AllWindowFalse()
    {
        if(!inventory.activeSelf && !spellBook.activeSelf)
        {
            return true;
        }
        else
        {
            return false;
        }
    }
}
