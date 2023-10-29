using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JumperAI : MonoBehaviour
{
    // have a cooldown
    // when its not on cooldown and when there is a platform adjacent
        //check if jumping over adjacent gets enemy closer
            //if yes jump over and add cooldown
            //if no dont jump, add smaller cooldown
    public float cooldown;
    public Tile currentOnTile;
    public Vector2 direction;
    public Tile target;
    public void Start()
    {
        cooldown = 10;
    }
    public void Update() {
        currentOnTile = this.GetComponent<EnemyAI>().currentOnTile;
        if(cooldown <= 0)
        {
            SkillPath();
        }
        if(cooldown > 0) cooldown -= Time.deltaTime;
         
    }
    public void SkillPath()
    {
        // possible tile check
        bool[] possibleTiles = GameObject.FindGameObjectWithTag("Tilemap").GetComponent<Pathfinding>().possibleTileCheck(currentOnTile);
        // from possible tiles, platform check.
        float[] roadVals = new float[4];
        for (int i = 0; i < 4; i++)
        {
            if (possibleTiles[i] == true)
            {
                if (i == 0) // up   
                {
                    roadVals[i] = GameObject.FindGameObjectWithTag("Tilemap").GetComponent<TilemapController>().TileArray[currentOnTile.posx, currentOnTile.posy + 1].roadVal;
                }
                else if (i == 1) // down
                {
                    roadVals[i] = GameObject.FindGameObjectWithTag("Tilemap").GetComponent<TilemapController>().TileArray[currentOnTile.posx, currentOnTile.posy - 1].roadVal;
                }
                else if (i == 2) // right
                {
                    roadVals[i] = GameObject.FindGameObjectWithTag("Tilemap").GetComponent<TilemapController>().TileArray[currentOnTile.posx + 1, currentOnTile.posy].roadVal;
                }
                else if (i == 3) // left
                {
                    roadVals[i] = GameObject.FindGameObjectWithTag("Tilemap").GetComponent<TilemapController>().TileArray[currentOnTile.posx - 1, currentOnTile.posy].roadVal;
                }
            }
            else
            {
                roadVals[i] = 1000;
            }
        }

        float minJump = 999;
        float[] temp = new float[8];
        for (float tile = 0; tile < 4; tile++)
        {
            switch (tile)
            {
                case 0: // up
                    if (roadVals[0] == 999)
                        temp[0] = GameObject.FindGameObjectWithTag("Tilemap").GetComponent<TilemapController>().TileArray[currentOnTile.posx, currentOnTile.posy + 2].roadVal;
                    else
                        temp[4] = GameObject.FindGameObjectWithTag("Tilemap").GetComponent<TilemapController>().TileArray[currentOnTile.posx, currentOnTile.posy + 1].roadVal;
                    break;
                case 1: // down
                    if (roadVals[1] == 999)
                        temp[1] = GameObject.FindGameObjectWithTag("Tilemap").GetComponent<TilemapController>().TileArray[currentOnTile.posx, currentOnTile.posy - 2].roadVal;
                    else
                        temp[5] = GameObject.FindGameObjectWithTag("Tilemap").GetComponent<TilemapController>().TileArray[currentOnTile.posx, currentOnTile.posy - 1].roadVal;
                    break;
                case 2: // right
                    if (roadVals[2] == 999)
                        temp[2] = GameObject.FindGameObjectWithTag("Tilemap").GetComponent<TilemapController>().TileArray[currentOnTile.posx + 2, currentOnTile.posy].roadVal;
                    else
                        temp[6] = GameObject.FindGameObjectWithTag("Tilemap").GetComponent<TilemapController>().TileArray[currentOnTile.posx + 1, currentOnTile.posy].roadVal;
                    break;
                case 3: // left
                    if (roadVals[3] == 999)
                        temp[3] = GameObject.FindGameObjectWithTag("Tilemap").GetComponent<TilemapController>().TileArray[currentOnTile.posx - 2, currentOnTile.posy].roadVal;
                    else
                        temp[7] = GameObject.FindGameObjectWithTag("Tilemap").GetComponent<TilemapController>().TileArray[currentOnTile.posx - 1, currentOnTile.posy].roadVal;
                    break;
            }
        }
        // after all the values are found, find shortest path and follow it. 
        bool Jumping = false;
        for(int lowest = 0; lowest < 8; lowest++)
        {
            if (temp[lowest] < minJump)
            {   
                minJump = temp[lowest];
                if(lowest < 4) // if lowest value is a jump, jumping true
                {
                    target = roadValToTile(lowest);
                    Jumping = true;
                }
                else
                {
                    Jumping = false;
                }
            }
        }
        if (Jumping)    // if shortest path does include jumping over a platform, jump over and give long cooldown.
        {
            // Set moveState to jumping
            this.GetComponent<EnemyMovement>().moveState = "jumping";
            this.GetComponent<Animator>().SetTrigger("Jump");
            cooldown = 12;
        }
        else            // If shortest path DOES NOT include jumping over a platform, give short cooldown.
        {
            cooldown = 2;
        }
    }
    public Tile roadValToTile(int val)
    {
        Tile target = null;
        if(val == 0)
        {
            // up
            target = GameObject.FindGameObjectWithTag("Tilemap").GetComponent<TilemapController>().TileArray[currentOnTile.posx, currentOnTile.posy + 2];
        }
        else if(val == 1)
        {
            // down
            target = GameObject.FindGameObjectWithTag("Tilemap").GetComponent<TilemapController>().TileArray[currentOnTile.posx, currentOnTile.posy - 2];
        }
        else if(val == 2)
        {
            // right
            target = GameObject.FindGameObjectWithTag("Tilemap").GetComponent<TilemapController>().TileArray[currentOnTile.posx + 2, currentOnTile.posy];
        }
        else if(val == 3)
        {
            // left
            target = GameObject.FindGameObjectWithTag("Tilemap").GetComponent<TilemapController>().TileArray[currentOnTile.posx - 2, currentOnTile.posy];
        }
        return target;
    }
}
