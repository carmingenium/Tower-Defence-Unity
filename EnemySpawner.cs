using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.IO;

public class EnemySpawner : MonoBehaviour
{

    public int r;
    public GameObject[] enemyPrefab;
    // Tier 1 Enemies: 0 - 4
    // Tier 2 Enemies: 5 - 8
    // Tier 3 Enemies: 9 - 11
    public List<GameObject> chosenEnemies;
    public int Wave;
    string wave;
    // ------------------------------------

    public GameObject loseScreen;
    

    void Start()
    {
        BeginSpawner();
        wave = File.ReadAllText(Application.streamingAssetsPath + "/Waves.txt");
    }
    private void Update()
    { 
        
    }
    public Vector2 CircleSpawnFunction()
    {
        int angle = UnityEngine.Random.Range(0, 360);

        double ylen = r * Math.Sin(angle);
        double xlen = r * Math.Cos(angle);

        Vector2 SpawnPoint = new Vector2((float)xlen, (float)ylen);
        return SpawnPoint;
    }
    public void BeginSpawner()
    {
        // find the right enemies from the wave value
        bool wave_continue= true;
        // from file, get values (which enemies to spawn, how many will be spawned)
        while(wave_continue){
            for (int i = 0; i < 25; i++) 
            // from the file, repeat this for loop for every enemy type that will be summoned, for the amount of enemy summon
            {
                Vector2 EnemySpawnPoint = CircleSpawnFunction();
                Vector3 convert = new Vector3(EnemySpawnPoint.x, EnemySpawnPoint.y, 0);
                Instantiate(chosenEnemies[0], convert, Quaternion.identity);
            }
            wave_continue = false;
        }
    }

    public void Spawner()
    {
        // Random int = 0-4 or 5-8 or 9-11

        // read wave.txt
        // if( first 3 are new)
        // add a random new enemy of that tier , add to chosenenemies
        // remove "new" part
        // foreach enemy in chosenenemies
        // if( "(") read through the (xTy):Z, summon Yth tier of xth enemy Z times
        // remove that part from the line
        // repeat loop
    }
}
