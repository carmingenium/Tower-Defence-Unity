using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class EnemyMovement : MonoBehaviour
{
    // Movement
    public Rigidbody2D rb;
    public float speed;
    public Vector2 direction;
    // States
    public string moveState;
    private void Start()
    {
        moveState = "normal";
    }
    void Update()
    {
        if(moveState == "normal")
        {
            direction = this.GetComponent<EnemyAI>().direction;
        }
        else if(moveState == "jumping")
        {
            direction = this.GetComponent<JumperAI>().direction;
        }
    }

    private void FixedUpdate()
    {
        // movement
        rb.velocity = new Vector2(direction.normalized.x * speed, direction.normalized.y * speed);
    }
}
