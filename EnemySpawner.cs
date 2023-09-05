using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class EnemySpawner : MonoBehaviour
{

    public int r;
    public GameObject[] enemyPrefab;
    // Tier 1 Enemies: 0 - 4
    // Tier 2 Enemies: 5 - 8
    // Tier 3 Enemies: 9 - 11
    public List<GameObject> chosenEnemies;
    public int Wave;
    // ------------------------------------

    public GameObject loseScreen;
    

    void Start()
    {
        WaveSpawner();
    }
    private void Update()
    {
        // check file, if there is a change, register it
        // if(change){
        // Tier 1-2-3 which
        // Random int = 0-4 or 5-8 or 9-11
        // chosenEnemies.Add(enemyprefab[int])
        // }
    }
    public Vector2 CircleSpawnFunction()
    {
        int angle = UnityEngine.Random.Range(0, 360);

        double ylen = r * Math.Sin(angle);
        double xlen = r * Math.Cos(angle);

        Vector2 SpawnPoint = new Vector2((float)xlen, (float)ylen);
        return SpawnPoint;
    }
    public void WaveSpawner()
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
}
