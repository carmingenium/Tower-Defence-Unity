using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.IO;
using System.Linq;
using UnityEngine.UI;
using TMPro;

public class EnemySpawner : MonoBehaviour
{

    public int r;
    public GameObject[] enemyPrefab;
    // Tier 1 Enemies: 0 - 4
    // Tier 2 Enemies: 5 - 8
    // Tier 3 Enemies: 9 - 11
    public int[] chosenEnemyIndex;
    public List<GameObject> chosenEnemies;
    public int WaveNumber;
    public float totalEnemyAmount = 0;
    public float waveEnemyAmount;
    string wave;
    // ------------------------------------

    public GameObject loseScreen;
    public GameObject waveBar;
    public GameObject waveBarText;

    // ------------------------------------ 

    public TextMeshProUGUI newEnemy_text;
    public TextMeshProUGUI lastWave_text;


    void Start()
    {
        chosenEnemyIndex = new int[12];
        WaveNumber = 0;
        wave = File.ReadAllText(Application.streamingAssetsPath + "/Waves.txt");
        wave = wave.Replace("\r\n", "_");
    }
    private void Update()
    { 
        if(totalEnemyAmount == 0)
        {
            if(WaveNumber <= 80)
            {
                WaveReader();
                WaveNumber += 1;
            }
        }
        waveBarText.GetComponent<TextMeshProUGUI>().text = "Wave Number = " + WaveNumber;
        waveBar.GetComponent<Image>().fillAmount = totalEnemyAmount / waveEnemyAmount;
    }
    public Vector2 CircleSpawnFunction()
    {
        int angle = UnityEngine.Random.Range(0, 360);

        double ylen = r * Math.Sin(angle);
        double xlen = r * Math.Cos(angle);

        Vector2 SpawnPoint = new Vector2((float)xlen, (float)ylen);
        return SpawnPoint;
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

        if(wave[0] == 'l') // lastwave condition
        {
            lastWave_text.gameObject.SetActive(true);
            lastWave_text.CrossFadeAlpha(0, 3f, false);
            // give alert of last wave.
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
        int numberOfEnemies = 0;
        foreach(char cr in currentLine)
        {
            if (cr.Equals('.')) numberOfEnemies += 1;
        }

        // if new enemy
        if(currentLine[0] == 'n')
        {
            int enemy = 0;
            if (currentLine[5] == '1')  // Tier1
            {
                do
                {
                    enemy = UnityEngine.Random.Range(1,5);
                } while (chosenEnemyIndex.Contains(enemy));
            }
            else if (currentLine[5] == '2')  // Tier2
            {
                do
                {
                    enemy = UnityEngine.Random.Range(5, 9);
                } while (chosenEnemyIndex.Contains(enemy));
            }
            else if (currentLine[5] == '3')  // Tier3
            {
                do
                {
                    enemy = UnityEngine.Random.Range(9, 12);
                } while (chosenEnemyIndex.Contains(enemy));
            }
            // after new enemy has been selected
            chosenEnemyIndex[enemy] = enemy;         // add enemy to chosen enemies
            chosenEnemies.Add(enemyPrefab[enemy]);  // add enemy to chosen enemies
            // these enemies will not be spawned this turn and this is intentional.
            NewEnemy();
        }
        waveEnemyAmount = 0;
        totalEnemyAmount = 0;
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

            // Tier 1 Enemies: 0 - 4
            // Tier 2 Enemies: 5 - 8
            // Tier 3 Enemies: 9 - 11

            // at this point: charindex - 2 = tier level /// charindex -4 order of that tier
            // 
            int tierLevel = currentLine[charIndex - 2] - '0';
            int tierOrder = currentLine[charIndex - 4] - '0';
            GameObject enemyToSummon = null;

            // economy
            int tier = -1;

            switch (tierLevel) 
            {
                case 1: //tier1
                    tier = 1;
                    if (tierOrder == 1) // 0
                    {
                        enemyToSummon = chosenEnemies[0];
                    }
                    else if (tierOrder == 2) // 1
                    {
                        enemyToSummon = chosenEnemies[1];
                    }
                    else if (tierOrder == 3) // 2
                    { 
                        enemyToSummon = chosenEnemies[2];
                    }
                    else if (tierOrder == 4) // 3
                    {
                        enemyToSummon = chosenEnemies[3];
                    }
                    else if (tierOrder == 5) // 4
                    {
                        enemyToSummon = chosenEnemies[4];
                    }
                    break;
                case 2:
                    tier = 2;
                    if (tierOrder == 1) // 5
                    {
                        enemyToSummon = chosenEnemies[5];
                    }
                    else if (tierOrder == 2) // 6
                    {
                        enemyToSummon = chosenEnemies[6];
                    }
                    else if (tierOrder == 3) // 7
                    {
                        enemyToSummon = chosenEnemies[7];
                    }
                    else if (tierOrder == 4) // 8
                    {
                        enemyToSummon = chosenEnemies[8];
                    }
                    break;
                case 3:
                    tier = 3;
                    if (tierOrder == 1) // 9
                    {
                        enemyToSummon = chosenEnemies[9];
                    }
                    else if (tierOrder == 2) // 10
                    {
                        enemyToSummon = chosenEnemies[10];
                    }
                    else if (tierOrder == 3) // 11
                    {
                        enemyToSummon = chosenEnemies[11];
                    }
                    break;
            }


            string str = new string(currentLine);
            str = str.Remove(charIndex, 1);
            currentLine = str.ToCharArray();

            point = currentLine[charIndex];

            List<int> integerNumbers = new List<int>();
            while(!(point == '.' || point == '_'))
            {
                integerNumbers.Add(point - '0');

                charIndex += 1;
                point = currentLine[charIndex];
            }

            // summon amount calculation
            int summonAmount = 0;

            if (integerNumbers.Count < 2)
            {
                int number = integerNumbers[0];
                summonAmount += number;
            }
            else
            {
                int number = integerNumbers[0];
                summonAmount += number * 10;
                number = integerNumbers[1];
                summonAmount += number;
            }

            // summon
            SummonEnemies(summonAmount, enemyToSummon, tier); 
            totalEnemyAmount += summonAmount;
            waveEnemyAmount += summonAmount;
        }
        
    }
    public void SummonEnemies(int summonAmount, GameObject enemy, int tier){
        for(int i = 0; i<summonAmount; i++){
            Vector2 EnemySpawnPoint = CircleSpawnFunction();
            Vector3 convert = new Vector3(EnemySpawnPoint.x, EnemySpawnPoint.y, 0);
            GameObject newEnemy = Instantiate(enemy, convert, Quaternion.identity);
            newEnemy.GetComponent<EnemyAI>().Tier = tier;
        }
        
    }

    public void NewEnemy()
    {
        newEnemy_text.gameObject.SetActive(true);
        newEnemy_text.CrossFadeAlpha(0, 2f, false);
    }
}
