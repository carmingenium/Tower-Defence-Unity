using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetTile : MonoBehaviour
{
    public float maxHP = 10;
    public float hp;
    GameObject loseScreen;
    public void Start()
    {
        hp = maxHP;
        loseScreen = GameObject.Find("EnemySpawner").GetComponent<EnemySpawner>().loseScreen;
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            hp -= 1;
            GameObject.Find("EnemySpawner").GetComponent<EnemySpawner>().totalEnemyAmount -= 1;
            Destroy(collision.gameObject);
        }
    }
    public void Update()
    {
        if(hp <= 0)
        {
            Time.timeScale = 0;
            loseScreen.SetActive(true);
        }
    }
}
