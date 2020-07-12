using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIStatus : MonoBehaviour
{
    public List<UILabel> label;
    private PlayerStatus player;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.Find("Player").GetComponent<PlayerStatus>();
    }

    // Update is called once per frame
    void Update()
    {
        label[0].text = player.Strength.ToString();
        label[1].text = player.Intellect.ToString();
        label[2].text = player.Agility.ToString();
        label[3].text = player.Stamina.ToString();
        label[4].text = player.Block.ToString();
        label[5].text = player.RegeneHP.ToString();
        label[6].text = player.AttackSpeed.ToString();
        label[7].text = player.CastSpeed.ToString();
        label[8].text = player.Armor.ToString();
        label[9].text = player.Critical.ToString();
        label[10].text = player.Penetration.ToString();
        label[11].text = player.CriDamage.ToString();
    }
}
