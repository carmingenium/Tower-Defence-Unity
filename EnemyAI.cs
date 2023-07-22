using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAI : MonoBehaviour
{
    public float speed;
    Vector2 targetPos;
    Vector2 direction;
    public Rigidbody2D rb;
    void Start()
    {
        Tile target = GameObject.FindGameObjectWithTag("Tilemap").GetComponent<Pathfinding>().core;
        targetPos = target.transform.position;
    }

    void Update()
    {
        // get closer to target
         direction = new Vector2(targetPos.x - this.transform.position.x, targetPos.y-transform.position.y);


    }
    private void FixedUpdate()
    {
        rb.velocity = new Vector2(direction.normalized.x * speed, direction.normalized.y * speed);
    }
}
