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
    //
    Tile currentOnTile;
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
            tilePathfinding();
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
        for(int i = 0; i<4; i++)
        {
            if(possibleTiles[i] == true)
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
        }
        // find lowest value tile
        float lowest = 998;
        int lowestPos = -1;
        int lowIndex = -1;
        foreach (float road in roadVals)
        {
            lowIndex += 1;
            if(road < lowest)
            {
                lowest = road;
                lowestPos = lowIndex;
            }
        }
        // THE BUG IS THAT YOU CANNOT CHECK NONEXISTENT TILES FOR IF THEY ARE REAL

        if (lowestPos == 0)
        {
            // go
            Vector3 newTargetTilePos = GameObject.FindGameObjectWithTag("Tilemap").GetComponent<TilemapController>().TileArray[currentOnTile.posx, currentOnTile.posy + 1].transform.position;
            direction = new Vector2(newTargetTilePos.x - this.transform.position.x, newTargetTilePos.y - this.transform.position.y);
        }
        if (lowestPos == 1)
        {
            // go
            Vector3 newTargetTilePos = GameObject.FindGameObjectWithTag("Tilemap").GetComponent<TilemapController>().TileArray[currentOnTile.posx, currentOnTile.posy - 1].transform.position;
            direction = new Vector2(newTargetTilePos.x - this.transform.position.x, newTargetTilePos.y - this.transform.position.y);
        }
        if (lowestPos == 2)
        {
            // go
            Vector3 newTargetTilePos = GameObject.FindGameObjectWithTag("Tilemap").GetComponent<TilemapController>().TileArray[currentOnTile.posx + 1, currentOnTile.posy].transform.position;
            direction = new Vector2(newTargetTilePos.x - this.transform.position.x, newTargetTilePos.y - this.transform.position.y);
        }
        if (lowestPos == 3)
        {
            // go
            Vector3 newTargetTilePos = GameObject.FindGameObjectWithTag("Tilemap").GetComponent<TilemapController>().TileArray[currentOnTile.posx - 1, currentOnTile.posy].transform.position;
            direction = new Vector2(newTargetTilePos.x - this.transform.position.x, newTargetTilePos.y - this.transform.position.y);
        }
    }
    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Tile"))
        {
            currentOnTile = collision.gameObject.GetComponent<Tile>();
        }
    }
}
