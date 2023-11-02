using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JumperAI : MonoBehaviour
{
    // have a cooldown
    // if cooldown <= 0
    // check adjacent tiles
        // if there is a platform
            // check adjacent lowest RoadVal and tile behind the platform
                // if tile behind the platform is lower roadVal than current target
                    // set target to tile behind platform and jump to it
                    // set large cooldown
                // else
                    // set low cooldown and continue
    public float cooldown;   // have a cooldown
    public Tile[,] map;      // map to check adjacents

    public Vector2 direction;   // direction to move in

    public void Start(){
        map = GameObject.FindGameObjectWithTag("Tilemap").GetComponent<Pathfinding>().tileMap;
    }
    public void Update(){
        if(cooldown <= 0)     // if cooldown <= 0
        { 
            // check adjacent tiles
           bool[] possibleTiles = GameObject.FindGameObjectWithTag("Tilemap").GetComponent<Pathfinding>().possibleTileCheck(
            this.GetComponent<EnemyAI>().currentOnTile);
            // if there is a platform
            Tile[] platforms = new Tile[4];
            List<Tile> adjacentTiles = new List<Tile>();
            for(int possible = 0; possible<4;possible++){
                // 0 up, 1 down, 2 right, 3 left
                if(possibleTiles[possible]){
                    switch (possible)
                        case 0: //0 up
                            // get current tile
                            Tile current = map[this.GetComponent<EnemyAI>().currentOnTile.posx , this.GetComponent<EnemyAI>().currentOnTile.posy + 1];
                            // if current tile is a platform
                            if(current.tileState == state.Platformed)
                                platforms[0] = current; // then add it to platforms
                            adjacentTiles.Add(current); // add adjacent to adjacent tiles
                            break;
                        case 1: //1 down
                            Tile current = map[this.GetComponent<EnemyAI>().currentOnTile.posx , this.GetComponent<EnemyAI>().currentOnTile.posy - 1];
                            // if current tile is a platform
                            if(current.tileState == state.Platformed)
                                platforms[1] = current; // then add it to platforms
                            adjacentTiles.Add(current); // add adjacent to adjacent tiles
                            break;
                        case 2: // 2 right
                            Tile current = map[this.GetComponent<EnemyAI>().currentOnTile.posx+1 , this.GetComponent<EnemyAI>().currentOnTile.posy];
                            // if current tile is a platform
                            if(current.tileState == state.Platformed)
                                platforms[2] = current; // then add it to platforms
                            adjacentTiles.Add(current); // add adjacent to adjacent tiles
                            break;
                        case 3: // 3 left
                            Tile current = map[this.GetComponent<EnemyAI>().currentOnTile.posx - 1 , this.GetComponent<EnemyAI>().currentOnTile.posy];
                            // if current tile is a platform
                            if(current.tileState == state.Platformed)
                                platforms[3] = current; // then add it to platforms
                            adjacentTiles.Add(current); // add adjacent to adjacent tiles
                            break;
                }
            }
            if(platforms.Count == 0){ // if there are no platforms to jump over, set small cooldown and continue
                cooldown = 2;
            }
            else{
                // Find normal target that enemy is supposed to go to.
                int lowestAdjacentRoadVal = 998;
                foreach(Tile tile in adjacentTiles){
                    if(tile.roadVal < lowestAdjacentRoadVal)
                        lowestAdjacentRoadVal = tile.roadVal;
                }
                // then compare that target to the tiles behind the adjacent platforms.
                int lowestJump = 998;
                Tile newTarget = null;
                for(int direct = 0; direct < 4; direct++){
                    switch (direct)
                        case 0: // up
                            if(platforms[0] != null){
                                Tile tileBehind = map[platforms[0].posx, platforms[0].posy + 1];
                                if(tileBehind.roadVal < lowestJump){
                                    lowestJump = tileBehind.roadVal;
                                    newTarget = tileBehind;
                                }
                                    
                            }
                            break;
                        case 1: // down
                            if(platforms[1] != null){
                                Tile tileBehind = map[platforms[1].posx, platforms[1].posy - 1];
                                if(tileBehind.roadVal < lowestJump){
                                    lowestJump = tileBehind.roadVal;
                                    newTarget = tileBehind;
                                }
                            }
                            break;
                        case 2: // right
                            if(platforms[2] != null){
                                Tile tileBehind = map[platforms[2].posx + 1, platforms[2].posy];
                                if(tileBehind.roadVal < lowestJump){
                                    lowestJump = tileBehind.roadVal;
                                    newTarget = tileBehind;
                                }
                            }
                            break;
                        case 3: // left
                            if(platforms[3] != null){
                                Tile tileBehind = map[platforms[3].posx - 1, platforms[3].posy];
                                if(tileBehind.roadVal < lowestJump){
                                    lowestJump = tileBehind.roadVal;
                                    newTarget = tileBehind;
                                }
                            }
                            break;
                }
                if(lowestJump > lowestAdjacentRoadVal){
                    cooldown = 2;
                }
                else{
                    // set target direction to tile behind platform and jump to it
                    direction = new Vector2(newTarget.transform.position.x - this.transform.position.x, newTarget.transform.position.y - this.transform.position.y);
                    // set movestate to jumping
                    this.GetComponent<EnemyMovement>().moveState = "jumping";
                    // set enemy collider to trigger
                    this.GetComponent<BoxCollider2D>().isTrigger = true;
                    // NEED TO SET IT BACK TO NOT TRIGGER LATER ON (im not sure if it is even necessary)
                    // set large cooldown
                    cooldown = 10;
                }
            }
             
        }
        else{
            cooldown -= Time.deltaTime;
        }
    }
}
