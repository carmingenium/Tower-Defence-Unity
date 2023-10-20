using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Economy : MonoBehaviour
{
    public int gold;

    public TextMeshProUGUI gold_text;
    public void Update()
    {
        gold_text.text = "" + gold;
    }
    public void Start()
    {
        gold = 100;
    }
}
