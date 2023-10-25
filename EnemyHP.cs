using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHP : MonoBehaviour
{
    public float maxHP;     
    public float hp;

    // economy
    public int Tier;

    // Start is called before the first frame update
    void Start()
    {
        maxHP = 100;    
        hp = maxHP;     
    }

    // Update is called once per frame
    void Update()
    {
        if (hp <= 0)
        {
            GameObject.Find("EnemySpawner").GetComponent<EnemySpawner>().totalEnemyAmount -= 1;
            // calculate given gold from tier
            int goldAmount = 0;
            switch (Tier)
            {
                case 1:
                    goldAmount = 2;
                    break;
                case 2:
                    goldAmount = 5;
                    break;
                case 3:
                    goldAmount = 10;
                    break;
            }
            // give gold depending on the tier of Enemy
            GameObject.Find("Gold").GetComponent<Economy>().gold += goldAmount;

            Destroy(this.gameObject);
        }
    }
    public void takeDamage(float dmg) // needs to be removed from EnemyAI and should have its own script
    {
        this.hp -= dmg;
    }
}
