using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class EnemySpawner : MonoBehaviour
{

    public int r;
    public GameObject enemyPrefab;
    public GameObject loseScreen;
    void Start()
    {
        for (int i = 0; i < 25; i++)
        {
            Vector2 EnemySpawnPoint = CircleSpawnFunction();
            Vector3 convert = new Vector3(EnemySpawnPoint.x, EnemySpawnPoint.y, 0);
            Instantiate(enemyPrefab, convert, Quaternion.identity);
        }
    }
    public Vector2 CircleSpawnFunction()
    {
        int angle = UnityEngine.Random.Range(0, 360);

        double ylen = r * Math.Sin(angle);
        double xlen = r * Math.Cos(angle);

        Vector2 SpawnPoint = new Vector2((float)xlen, (float)ylen);
        return SpawnPoint;
    }
}
