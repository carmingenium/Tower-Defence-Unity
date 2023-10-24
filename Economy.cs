using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Economy : MonoBehaviour
{
    public int gold;
    public TextMeshProUGUI gold_text;


    public int goldMineAmount;
    public float timer;
    public void Update()
    {
        gold_text.text = "" + gold;

        if(timer >= 1)
        {
            gold += goldMineAmount;
            timer = 0;
        }
        timer += Time.deltaTime;
    }
    public void Start()
    {
        goldMineAmount = 0;
        gold = 100;
    }
}
