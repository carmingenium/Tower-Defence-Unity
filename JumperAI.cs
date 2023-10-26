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

    public void Update(){
        if(cooldown <= 0){
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

        float minJump = -1;
        float[] temp = new float[8];
        for(float tile=0; tile<4; tile++)
        {
            switch(tile) 
            {
                case 0: // up
                    if(roadVals[0] == 999)
                        float temp[0] = GameObject.FindGameObjectWithTag("Tilemap").GetComponent<TilemapController>().TileArray[currentOnTile.posx, currentOnTile.posy + 2].roadVal;
                    else
                        float temp[4] = GameObject.FindGameObjectWithTag("Tilemap").GetComponent<TilemapController>().TileArray[currentOnTile.posx, currentOnTile.posy + 1].roadVal;
                    break;
                case 1: // down
                    if(roadVals[1] == 999)
                        float temp[1] = GameObject.FindGameObjectWithTag("Tilemap").GetComponent<TilemapController>().TileArray[currentOnTile.posx, currentOnTile.posy - 2].roadVal;
                    else
                        float temp[5] = GameObject.FindGameObjectWithTag("Tilemap").GetComponent<TilemapController>().TileArray[currentOnTile.posx, currentOnTile.posy - 1].roadVal;
                    break;
                case 2: // right
                    if(roadVals[2] == 999)
                        float temp[2] = GameObject.FindGameObjectWithTag("Tilemap").GetComponent<TilemapController>().TileArray[currentOnTile.posx + 2, currentOnTile.posy].roadVal;
                    else
                        float temp[6] = GameObject.FindGameObjectWithTag("Tilemap").GetComponent<TilemapController>().TileArray[currentOnTile.posx + 1, currentOnTile.posy].roadVal;
                    break;
                case 3: // left
                    if(roadVals[3] == 999)
                        float temp[3] = GameObject.FindGameObjectWithTag("Tilemap").GetComponent<TilemapController>().TileArray[currentOnTile.posx - 2, currentOnTile.posy].roadVal;
                    else
                        float temp[7] = GameObject.FindGameObjectWithTag("Tilemap").GetComponent<TilemapController>().TileArray[currentOnTile.posx - 1, currentOnTile.posy].roadVal;
                    break;
            }
        }
        // after all the values, find shortest path and follow it. 
        // If shortest path DOES NOT include jumping over a platform, give short cooldown.
        // else if shortest path does include jumping over a platform, jump over and give long cooldown.
    }
}
