using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitAction : MonoBehaviour
{
    public GameObject currentTarget; // current enemy target

    public float timerMax; // attack speed
    public float timer;    // attack cooldown
    public bool attacked;  // attack enabler

    public float attackDMG;// damage to enemy
    public void Start()
    {
        if (this.gameObject.GetComponent<Tile>().tileState.Equals(state.tower1))
        {
            attackDMG = 50;
            timerMax = 2;
        }
        else if (this.gameObject.GetComponent<Tile>().tileState.Equals(state.tower2))
        {
            attackDMG = 30;
            timerMax = 1.2f;
        }
    }
    private void Update()
    {
        if(currentTarget != null && !attacked)
        {
            Attack();
        }
        if(timer > 0)
        {
            timer -= Time.deltaTime;
        }
        if(timer <= 0)
        {
            timer = 0;
            attacked = false;
        }
    }
    public void Attack()
    {
        attacked = true;


        //Destroy(currentTarget.gameObject);
        currentTarget.GetComponent<EnemyAI>().takeDamage(attackDMG); 


        currentTarget = null; // safety measurements
        timer = timerMax;
    }
}
