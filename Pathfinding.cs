using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pathfinding : MonoBehaviour
{
    // attach to Map


    Tile[,] tileMap;
    public Tile core;
    void Start()
    {
        tileMap = this.gameObject.GetComponent<TilemapController>().TileArray;

        // Find Core Tile
        foreach(Tile tile in tileMap)
        {
            if (tile.tileState.Equals(state.Target))
            {
                core = tile;
            }
        }

        // start the first pathfinding loop from Core tile
        pathfindValueSet();
    }

    public void pathfindValueSet()
    {
        bool[,] checkArray = new bool[20, 20];
        int[] corePos = core.getPos();
        bool done = false;

        // lastwave
        // last wave means last checked and value setted wave and its tiles.
        List<Tile> lastWave = new List<Tile>();
        // nextwave
        List<Tile> nextWave = new List<Tile>();

        // setting core (wave 0)
        int pathfindingVal = 0;
        checkArray[corePos[0], corePos[1]] = true;
        core.roadVal = pathfindingVal;
        pathfindingVal += 1;
        // put core into last wave list
        lastWave.Add(core);


        // start wave loop from here
        while (!done)
        {
            // find available surroundings of last wave
            foreach(Tile tile in lastWave)
            {
                // find available surroundings of last wave
                bool[] direction = possibleTileCheck(tile);
                for(int i = 0; i < 4; i++)
                {
                    
                    if (direction[i]) // 0 up, 1 down, 2 right, 3 left
                                      // put surroundings in a list
                    {
                        if (i == 0)
                        {
                            if(!checkArray[tile.posx, tile.posy + 1] && (tileMap[tile.posx, tile.posy + 1].tileState.Equals(state.Empty) | tileMap[tile.posx, tile.posy + 1].tileState.Equals(state.Corrupted)))
                            {
                                nextWave.Add(tileMap[tile.posx, tile.posy + 1]);
                                checkArray[tile.posx, tile.posy + 1] = true;
                            }
                            if (!(tileMap[tile.posx, tile.posy + 1].tileState.Equals(state.Empty) | tileMap[tile.posx, tile.posy + 1].tileState.Equals(state.Corrupted) | tileMap[tile.posx, tile.posy + 1].tileState.Equals(state.Target)))
                            {
                                tileMap[tile.posx, tile.posy + 1].roadVal = 999;
                            }
                        }
                        else if (i == 1)
                        {
                            if (!checkArray[tile.posx, tile.posy - 1] && (tileMap[tile.posx, tile.posy - 1].tileState.Equals(state.Empty) | tileMap[tile.posx, tile.posy - 1].tileState.Equals(state.Corrupted)))
                            {
                                nextWave.Add(tileMap[tile.posx, tile.posy - 1]);
                                checkArray[tile.posx, tile.posy - 1] = true;
                            }
                            if (!(tileMap[tile.posx, tile.posy - 1].tileState.Equals(state.Empty) | tileMap[tile.posx, tile.posy - 1].tileState.Equals(state.Corrupted) | tileMap[tile.posx, tile.posy - 1].tileState.Equals(state.Target)))
                            {
                                tileMap[tile.posx, tile.posy - 1].roadVal = 999;
                            }
                        }
                        else if (i == 2)
                        {
                            if (!checkArray[tile.posx + 1, tile.posy] && (tileMap[tile.posx + 1, tile.posy].tileState.Equals(state.Empty) | tileMap[tile.posx + 1, tile.posy].tileState.Equals(state.Corrupted)))
                            {
                                nextWave.Add(tileMap[tile.posx + 1, tile.posy]);
                                checkArray[tile.posx + 1, tile.posy] = true;
                            }
                            if (!(tileMap[tile.posx + 1, tile.posy].tileState.Equals(state.Empty) | tileMap[tile.posx + 1, tile.posy].tileState.Equals(state.Corrupted) | tileMap[tile.posx + 1, tile.posy].tileState.Equals(state.Target)))
                            {
                                tileMap[tile.posx + 1, tile.posy].roadVal = 999;
                            }
                        }
                        else if (i == 3)
                        {
                            if (!checkArray[tile.posx - 1, tile.posy] && (tileMap[tile.posx - 1, tile.posy].tileState.Equals(state.Empty) | tileMap[tile.posx - 1, tile.posy].tileState.Equals(state.Corrupted)))
                            {
                                nextWave.Add(tileMap[tile.posx - 1, tile.posy]);
                                checkArray[tile.posx - 1, tile.posy] = true;
                            }
                            if (!(tileMap[tile.posx - 1, tile.posy].tileState.Equals(state.Empty) | tileMap[tile.posx - 1, tile.posy].tileState.Equals(state.Corrupted) | tileMap[tile.posx - 1, tile.posy].tileState.Equals(state.Target)))
                            {
                                tileMap[tile.posx - 1, tile.posy].roadVal = 999;
                            }
                        }
                    }
                }
            }
            if (nextWave.Count.Equals(0))
            {
                done = true;
            }
            // set value to nextwave items
            foreach (Tile tile in nextWave)
            {
                if (tile.tileState.Equals(state.Empty) | tile.tileState.Equals(state.Corrupted))
                {
                    tile.roadVal = pathfindingVal;
                }
            }
            // reset lastwave
            lastWave.Clear();
            
            // set nextwave to lastwave
            foreach(Tile tile in nextWave)
            {
                lastWave.Add(tile);
            }
            // reset nextwave
            nextWave.Clear();



            // increase pathfindingval;
            pathfindingVal += 1;
        }
        // write an algorithm here to set right values.

    }
    public bool[] possibleTileCheck(Tile checktile)
    {
        int[] pos = checktile.getPos();
        bool up = false, down = false, right = false, left = false;
        // up check
        if (pos[1]+1 < 20 && pos[1] + 1 >= 0)
        {
            up = true;
        }        
        // down check
        if (pos[1] - 1 < 20 && pos[1] - 1 >= 0)
        {
            down = true;
        }        
        // right check
        if (pos[0] + 1 < 20 && pos[0] + 1 >= 0)
        {
            right = true;
        }        
        // left check
        if (pos[0] - 1 < 20 && pos[0] - 1 >= 0)
        {
            left = true;
        }
        // 0 = up, 1 = down, 2 = right, 3 = left
        bool[] directions = new bool[4];
        directions[0] = up;
        directions[1] = down;
        directions[2] = right;
        directions[3] = left;
        return directions;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.transform.CompareTag("Enemy"))
        {
            collision.gameObject.GetComponent<EnemyAI>().behaviour = "firsttile";
        }
    }
}
