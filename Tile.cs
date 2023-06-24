using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tile : MonoBehaviour
{
    public state tileState;
    public int roadVal;

}
public enum state
{
    Target,             // 0
    Corrupted,          // 1
    Resource,           // 2
    Boulder,            // 3
    Empty,              // 4
    Platformed,         // 5
    tower1,             // 6
    tower2,             // 7
    Mine,               // 8    
}