using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAI : MonoBehaviour
{
    public string behaviour;
    public float speed;
    Vector2 targetPos;
    Vector2 direction;
    public Rigidbody2D rb;
    void Start()
    {
        behaviour = "getclose";
        Tile target = GameObject.FindGameObjectWithTag("Tilemap").GetComponent<Pathfinding>().core;
        targetPos = target.transform.position;
    }

    void Update()
    {
        // get closer to target
        if (behaviour.Equals("getclose"))
        {
            GetClose();
        }
        else if (behaviour.Equals("firsttile"))
        {
            FirstTile();
        }
        else if (behaviour.Equals("tilepath"))
        {
            // use tile values to find way.
            direction = Vector2.zero;
        }
    }
    private void FixedUpdate()
    {
        rb.velocity = new Vector2(direction.normalized.x * speed, direction.normalized.y * speed);
    }
    public void GetClose()
    {
        direction = new Vector2(targetPos.x - this.transform.position.x, targetPos.y - transform.position.y);
    }
    public void FirstTile()
    {
        direction = new Vector2(0, 0);
        Tile[,] tilemap = GameObject.FindGameObjectWithTag("Tilemap").GetComponent<TilemapController>().TileArray;
        Tile closestTile = GameObject.FindGameObjectWithTag("Tilemap").GetComponent<Pathfinding>().core;
        float closestDist = 999;
        foreach(Tile tile in tilemap)
        {
            if(Vector3.Distance(closestTile.transform.position,this.transform.position) < closestDist)
            {
                closestTile = tile;
                closestDist = Vector3.Distance(closestTile.transform.position, this.transform.position);
            }
        }
        direction = new Vector2(closestTile.transform.position.x - this.transform.position.x, closestTile.transform.position.y - this.transform.position.y);
    }
}
