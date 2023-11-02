using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JumperAI : MonoBehaviour
{
    // have a cooldown
    // if cooldown <= 0
    // check adjacent tiles
        // if there is a platform
            // check current target and tile behind the platform
                // if tile behind the platform is lower roadVal than current target
                    // set target to tile behind platform and jump to it
                    // set large cooldown
                // else
                    // set low cooldown and continue
    public float cooldown;   // have a cooldown

    public void Update(){
        if(cooldown <= 0)     // if cooldown <= 0
        { 
           bool[] possibleTiles = GameObject.FindGameObjectWithTag("Tilemap").GetComponent<Pathfinding>().possibleTileCheck(
            this.gameObject.GetComponent<EnemyAI>().currentOnTile);
             
        }
        else{
            cooldown -= Time.deltaTime;
        }
    }
}
