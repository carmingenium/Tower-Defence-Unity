using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAI : MonoBehaviour
{
    // Fundamentals
    public float maxHP;
    public float hp;
    public Rigidbody2D rb;
    public float speed;
    // pathfinding
    public string behaviour;
    Vector2 targetPos;
    Vector2 direction;
    public Tile currentOnTile;
    void Start()
    {
        maxHP = 100;    // sbr
        hp = maxHP;     // sbr
        behaviour = "getclose";
        Tile target = GameObject.FindGameObjectWithTag("Tilemap").GetComponent<Pathfinding>().core;
        targetPos = target.transform.position;
    }

    void Update()
    {
        if(hp <= 0)     // sbr
        {
            GameObject.Find("EnemySpawner").GetComponent<EnemySpawner>().totalEnemyAmount -= 1;
            // get tier of enemy
            // give gold depending on the tier of Enemy
            Destroy(this.gameObject);
        }
        // get closer to target
        if (behaviour.Equals("getclose"))
        {
            GetClose();
            behaviour = "standby";
        }
        else if (behaviour.Equals("firsttile"))
        {
            FirstTile();
            behaviour = "standby";
        }
        else if (behaviour.Equals("tilepath")) // && this.transform.position == currentOnTile.transform.position
        {
            // use tile values to find way.
            tilePathfinding();
        }
    }
    private void FixedUpdate()
    {
        // movement     // sbr
        rb.velocity = new Vector2(direction.normalized.x * speed, direction.normalized.y * speed);  // sbr
    }
    public void GetClose()
    {
        // set direction to the target
        direction = new Vector2(targetPos.x - this.transform.position.x, targetPos.y - transform.position.y);
    }
    public void FirstTile()
    {
        // find the closest tile, set direction to that tile.
        direction = new Vector2(0, 0);
        Tile[,] tilemap = GameObject.FindGameObjectWithTag("Tilemap").GetComponent<TilemapController>().TileArray;
        Tile closestTile = GameObject.FindGameObjectWithTag("Tilemap").GetComponent<Pathfinding>().core;
        float closestDist = 999;
        foreach (Tile tile in tilemap)
        {
            float currentdist = Vector3.Distance(tile.transform.position, this.transform.position);
            if (currentdist < closestDist)
            {
                closestTile = tile;
                closestDist = Vector3.Distance(tile.transform.position, this.transform.position);
            }
        }
        direction = new Vector2(closestTile.transform.position.x - this.transform.position.x, closestTile.transform.position.y - this.transform.position.y);
    }
    public void tilePathfinding()
    {
        // get value of current tile
        int currentRoadVal = currentOnTile.roadVal;
        // check adjacent tiles
        bool[] possibleTiles = GameObject.FindGameObjectWithTag("Tilemap").GetComponent<Pathfinding>().possibleTileCheck(currentOnTile);
        float[] roadVals = new float[4];
        for (int i = 0; i < 4; i++)
        {
            if (possibleTiles[i] == true)
            {
                if (i == 0)
                {
                    roadVals[i] = GameObject.FindGameObjectWithTag("Tilemap").GetComponent<TilemapController>().TileArray[currentOnTile.posx, currentOnTile.posy + 1].roadVal;
                }
                if (i == 1)
                {
                    roadVals[i] = GameObject.FindGameObjectWithTag("Tilemap").GetComponent<TilemapController>().TileArray[currentOnTile.posx, currentOnTile.posy - 1].roadVal;
                }
                if (i == 2)
                {
                    roadVals[i] = GameObject.FindGameObjectWithTag("Tilemap").GetComponent<TilemapController>().TileArray[currentOnTile.posx + 1, currentOnTile.posy].roadVal;
                }
                if (i == 3)
                {
                    roadVals[i] = GameObject.FindGameObjectWithTag("Tilemap").GetComponent<TilemapController>().TileArray[currentOnTile.posx - 1, currentOnTile.posy].roadVal;
                }
            }
            else
            {
                roadVals[i] = 999;
            }
        }
        // find lowest value tile
        float lowest = 998;
        int lowestPos = -1;
        int lowIndex = -1;
        foreach (float road in roadVals)
        {
            lowIndex += 1;
            if (road < lowest)
            {
                lowest = road;
                lowestPos = lowIndex;
            }
        }

        if (lowestPos == 0)
        {
            // go up
            Vector3 newTargetTilePos = GameObject.FindGameObjectWithTag("Tilemap").GetComponent<TilemapController>().TileArray[currentOnTile.posx, currentOnTile.posy + 1].transform.position;
            direction = new Vector2(newTargetTilePos.x - this.transform.position.x, newTargetTilePos.y - this.transform.position.y);
            //direction = Vector2.up;
        }
        if (lowestPos == 1)
        {
            // go down
            Vector3 newTargetTilePos = GameObject.FindGameObjectWithTag("Tilemap").GetComponent<TilemapController>().TileArray[currentOnTile.posx, currentOnTile.posy - 1].transform.position;
            direction = new Vector2(newTargetTilePos.x - this.transform.position.x, newTargetTilePos.y - this.transform.position.y);
            //direction = Vector2.down;
        }
        if (lowestPos == 2)
        {
            // go right
            Vector3 newTargetTilePos = GameObject.FindGameObjectWithTag("Tilemap").GetComponent<TilemapController>().TileArray[currentOnTile.posx + 1, currentOnTile.posy].transform.position;
            direction = new Vector2(newTargetTilePos.x - this.transform.position.x, newTargetTilePos.y - this.transform.position.y);
            //direction = Vector2.right;
        }
        if (lowestPos == 3)
        {
            // go left
            Vector3 newTargetTilePos = GameObject.FindGameObjectWithTag("Tilemap").GetComponent<TilemapController>().TileArray[currentOnTile.posx - 1, currentOnTile.posy].transform.position;
            direction = new Vector2(newTargetTilePos.x - this.transform.position.x, newTargetTilePos.y - this.transform.position.y);
            //direction = Vector2.left;
        }
    }

    public void takeDamage(float dmg) // needs to be removed from EnemyAI and should have its own script
    {
        this.hp -= dmg;
    }
}
