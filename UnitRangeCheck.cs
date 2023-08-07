using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitRangeCheck : MonoBehaviour
{
    // later in development
    public string attackTargetState;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        // if something enters the trigger, recognize it
        if (collision.CompareTag("Enemy"))
        {
            Destroy(collision.gameObject);
        }
    }
}
