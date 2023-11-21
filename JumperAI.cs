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
    // for each existing tile, check if their value is lower than lowest adjacent value
    // if none is lower, set cooldown = 3;
    // if one is lower, set new directions to that tile UNTIL that tile is reached (could be done by distance).

    int cooldown;
    Vector2 direction;
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
            cooldown -= Time.deltaTime
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
                        if (map[x, y + 1].tileState == Platformed)
                            platforms[i] = map[x, y + 1];
                        break;
                    case 1:
                        if (map[x, y - 1].tileState == Platformed)
                            platforms[i] = map[x, y - 1];
                        break;
                    case 2:
                        if (map[x + 1, y].tileState == Platformed)
                            platforms[i] = map[x + 1, y];
                        break;
                    case 3:
                        if (map[x - 1, y].tileState == Platformed)
                            platforms[i] = map[x - 1, y];
                        break;
                }
            }
            else
                notPlatformCount++;
        }
        if(notPlatformCount = 4)
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
                            jumpables[i] = platforms[i];
                        }
                        break;
                    case 1:
                        if (y - 2 > 0) // down
                        {
                            jumpables[i] = platforms[i];
                        }
                        break;
                    case 2:
                        if (x + 2 < 51) // right
                        {
                            jumpables[i] = platforms[i];
                        }
                        break;
                    case 3:
                        if (x - 2 > 0) // left
                        {
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


    }
    public int adjacentLowestRoadVal()
    {

    }
}
