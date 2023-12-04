using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JumperAI : MonoBehaviour
{
    // START condition: cooldown = 0; d
    // check if adjacent tiles exist d
    // for each adjacent tile, check if existing tiles have platforms d
    // if no platforms, set cooldown = 3; d
    // for each platform, check if tiles across platforms exist d
    // if no tiles, set cooldown = 3; d
    // for each existing tile, check if their value is lower than lowest adjacent value d
    // if none is lower, set cooldown = 3; d
    // if one is lower, set new directions to that tile UNTIL that tile is reached (could be done by distance). d

    float cooldown;
    public Vector2 direction;
    Tile[,] map;

    bool jumpState; // could be converted to string

    public void Start()
    {
        cooldown = 15;
        map = GameObject.FindGameObjectWithTag("Tilemap").GetComponent<Pathfinding>().tileMap;

    }

    public void Update()
    {
        if(cooldown <= 0) 
        {
            cooldown = Jump();
        }
        else
        {
            cooldown -= Time.deltaTime;
        }
        if(Vector2.Distance(this.transform.position, direction) < 0.1f) // IF TARGET TILE IS REACHED, STOP JUMPING;
        {
            this.GetComponent<EnemyMovement>().moveState = "normal";
            // set cooldown to 15
            cooldown = 15;
        }
    }
    
    public int Jump()
    {
        // first check adjacent tiles for if they exist
        bool[] possibleTiles = GameObject.FindGameObjectWithTag("Tilemap").GetComponent<Pathfinding>().possibleTileCheck(
            this.GetComponent<EnemyAI>().currentOnTile);

        // check existing tiles if they are platforms
        Tile[] platforms = new Tile[4];  // array to save platform tiles with their directions
        int notPlatformCount = 0;
        for(int i = 0; i < 4; i++)
        {
            // 0 up, 1 down, 2 right, 3 left
            if (possibleTiles[i])
            {
                int x = this.GetComponent<EnemyAI>().currentOnTile.posx;
                int y = this.GetComponent<EnemyAI>().currentOnTile.posy;
                switch (i)
                {
                    case 0:
                        if (map[x, y + 1].tileState == state.Platformed)
                            platforms[i] = map[x, y + 1];
                        break;
                    case 1:
                        if (map[x, y - 1].tileState == state.Platformed)
                            platforms[i] = map[x, y - 1];
                        break;
                    case 2:
                        if (map[x + 1, y].tileState == state.Platformed)
                            platforms[i] = map[x + 1, y];
                        break;
                    case 3:
                        if (map[x - 1, y].tileState == state.Platformed)
                            platforms[i] = map[x - 1, y];
                        break;
                }
            }
            else
                notPlatformCount++;
        }
        if(notPlatformCount == 4)
        {
            return 3; // set cooldown 3;
        }

        // for each platform check if tiles across the platforms exist
        Tile[] jumpables = new Tile[4];
        int notExistingTile = 0;
        for(int i = 0; i < 4; i++)
        {
            if (platforms[i] != null)
            {
                int x = this.GetComponent<EnemyAI>().currentOnTile.posx;
                int y = this.GetComponent<EnemyAI>().currentOnTile.posy;
                switch (i)
                {
                    case 0:
                        if (y + 2 < 51) // up
                        {
                            if (map[x, y + 2].tileState == state.Empty)   // also checking if existing tile is jumpable
                                jumpables[i] = platforms[i];
                        }
                        break;
                    case 1:
                        if (y - 2 > 0) // down
                        {
                            if (map[x, y - 2].tileState == state.Empty)   // also checking if existing tile is jumpable
                            jumpables[i] = platforms[i];
                        }
                        break;
                    case 2:
                        if (x + 2 < 51) // right
                        {
                            if (map[x + 2, y].tileState == state.Empty)   // also checking if existing tile is jumpable
                            jumpables[i] = platforms[i];
                        }
                        break;
                    case 3:
                        if (x - 2 > 0) // left
                        {
                            if (map[x - 2, y].tileState == state.Empty)   // also checking if existing tile is jumpable
                            jumpables[i] = platforms[i];
                        }
                        break;
                }
            }
            else
                notExistingTile++;
        }
        if (notExistingTile == 4)
            return 3; // set cooldown
        // for each existing tile, check their roadvalue to adjacent lowest roadval
        int lowestRoadVal = adjacentLowestRoadVal(possibleTiles);
        Tile jumpTo = null;

        for(int i = 0; i < 4; i++)
        {
            if (jumpables[i] != null)
            {
                if (jumpables[i].roadVal < lowestRoadVal)
                    jumpTo = jumpables[i];
            }
        }
        if(jumpTo == null)
            return 3;// set cooldown
        else
        {
            // set direction to jumpTo
            direction = new Vector2(jumpTo.posx,jumpTo.posy);
            // set jumpState to true
            this.GetComponent<EnemyMovement>().moveState = "jumping";
            return 15;// set cooldown
        }
    }
    public int adjacentLowestRoadVal(bool[] possibleTiles)
    {
        int x = this.GetComponent<EnemyAI>().currentOnTile.posx;
        int y = this.GetComponent<EnemyAI>().currentOnTile.posy;

        int count = 0;
        int LowestRoadVal = 1000;
        foreach(bool tile in possibleTiles)
        {
            if (tile)
            {
                switch (count)
                {
                    case 0:
                        if (map[x, y + 1].roadVal < map[x, y].roadVal)
                            LowestRoadVal = map[x, y + 1].roadVal;
                        break;
                    case 1:
                        if (map[x, y - 1].roadVal < map[x, y].roadVal)
                            LowestRoadVal = map[x, y - 1].roadVal;
                        break;
                    case 2:
                        if (map[x + 1, y].roadVal < map[x, y].roadVal)
                            LowestRoadVal = map[x + 1, y].roadVal;
                        break;
                    case 3:
                        if (map[x - 1, y].roadVal < map[x, y].roadVal)
                            LowestRoadVal = map[x - 1, y].roadVal;
                        break;
                }
            }
        }
        return LowestRoadVal;
    }
}
