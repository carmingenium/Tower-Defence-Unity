using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitAction : MonoBehaviour
{
    public GameObject currentTarget;
    public float timer;
    public bool attacked;
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
        Destroy(currentTarget.gameObject);
        currentTarget = null; // safety measurements
        timer = 3;
    }
}
