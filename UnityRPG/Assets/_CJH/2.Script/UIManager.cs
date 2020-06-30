using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    public GameObject Inventory;
    public GameObject SpellBook;

    void Update()
    {
        if(Input.GetKeyDown(KeyCode.I))
        {
            if(Inventory.activeSelf) { Inventory.SetActive(false); }
            else { Inventory.SetActive(true); }
        }
        if(Input.GetKeyDown(KeyCode.K))
        {
            if(SpellBook.activeSelf) { SpellBook.SetActive(false); }
            else { SpellBook.SetActive(true); }
        }
    }
}
