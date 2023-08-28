using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    Tile core;
    TextMeshProUGUI hpText;
    Image hpFill;
    
    // Start is called before the first frame update
    void Start()
    {
        hpText = this.GetComponentInChildren<TextMeshProUGUI>();
        hpFill = this.GetComponentInChildren<Image>();
    }
    public void Awake()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if(core == null)
        {
            core = GameObject.FindGameObjectWithTag("Tilemap").GetComponent<Pathfinding>().core;
        }
        else
        {
            hpText.text = core.GetComponent<TargetTile>().hp + "/" + core.GetComponent<TargetTile>().maxHP;
            hpFill.fillAmount = core.GetComponent<TargetTile>().hp / core.GetComponent<TargetTile>().maxHP;
        }
    }
}
