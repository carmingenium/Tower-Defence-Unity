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
    public List<int> chosenEnemyIndex;
    public List<GameObject> chosenEnemies;
    public int WaveNumber;
    string wave;
    // ------------------------------------

    public GameObject loseScreen;
    

    void Start()
    {
        BeginSpawner();
        wave = File.ReadAllText(Application.streamingAssetsPath + "/Waves.txt");
        wave = wave.Replace("\r\n", "_");
        WaveReader();
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

    public void WaveReader()
    {
        // Random int = 0-4 or 5-8 or 9-11

        // read wave.txt
        char[] waveLine; // char array of the line

        int charAmount = 0; // to count amount of chars in one line
        char next = wave[charAmount]; // counting array by array
        

        while(next != '_') // while next character is not the end of line char
        {
            charAmount += 1; 
            next = wave[charAmount]; // go to the next char
        }
        charAmount += 1; // to include end of line character.

        waveLine = new char[charAmount]; // current line array

        for(int i = 0; i<charAmount; i++) // for every character, assign the right characters to the current line array from string
        {                                 // also remove this line completely from the string for the next wave to be more readable. 
            waveLine[i] = wave[0];
            wave = wave.Remove(0,1);
        }


        Spawner(waveLine);

        // if( first 3 are new)
        // add a random new enemy of that tier , add to chosenenemies
        // remove "new" part
        // foreach enemy in chosenenemies
        // if( "(") read through the (xTy):Z, summon Yth tier of xth enemy Z times
        // remove that part from the line
        // repeat loop
    }
    public void Spawner(char[] currentLine)
    {
        int numberOfEnemies = 1;
        foreach(char cr in currentLine)
        {
            if (cr.Equals(',')) numberOfEnemies += 1;
        }

        // if new enemy
        if(currentLine[0] == 'n')
        {
            if (currentLine[5] == '1')
            {
                int enemy = 0;
                do
                {
                    enemy = UnityEngine.Random.Range(1,5);
                } while (chosenEnemyIndex.Contains(enemy));
            }
            if (currentLine[5] == '2')
            {
                int enemy = 0;
                do
                {
                    enemy = UnityEngine.Random.Range(5, 9);
                } while (chosenEnemyIndex.Contains(enemy));
            }
            if (currentLine[5] == '3')
            {
                int enemy = 0;
                do
                {
                    enemy = UnityEngine.Random.Range(9, 12);
                } while (chosenEnemyIndex.Contains(enemy));
            }
        }
        else // only enemy summoning
        {
            // for the amount of enemy types
            for(int i = 0; numberOfEnemies>i; i++)
            {
                char point = ' ';
                int charIndex = 0;
                while(point != ':')
                {
                    charIndex += 1;
                    point = currentLine[charIndex]; 
                }
            }
        }
    }
}
