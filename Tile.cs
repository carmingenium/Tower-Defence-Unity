using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tile : MonoBehaviour
{
    public state tileState;
    public int roadVal;
    public int posx;
    public int posy;

    public void PanelOpen()
    {
        switch (tileState)
        {
            case state.Target:
                break;
            case state.Corrupted:
                break;
            case state.Resource:
                GameObject panelGold = null;
                foreach (GameObject go in Resources.FindObjectsOfTypeAll(typeof(GameObject)) as GameObject[])
                {
                    if (go.name.Equals("Gold - Panel"))
                        panelGold = go;
                }
                if (!this.transform.root.GetComponent<TilemapController>().activePanel)
                {
                    transform.root.GetComponent<TilemapController>().activePanelTile = this;
                    panelGold.SetActive(true);
                    transform.root.GetComponent<TilemapController>().activePanel = true;
                }
                // add mine
                break;
            case state.Boulder:
                // maybe breaking for money?
                break;
            case state.Empty:
                GameObject panelEmpty = null;
                foreach (GameObject go in Resources.FindObjectsOfTypeAll(typeof(GameObject)) as GameObject[])
                {
                    if (go.name.Equals("Empty - Panel"))
                    {
                        panelEmpty = go;
                        break;
                    }
                }
                if (!this.transform.root.GetComponent<TilemapController>().activePanel)
                {
                    transform.root.GetComponent<TilemapController>().activePanelTile = this;
                    panelEmpty.SetActive(true);
                    transform.root.GetComponent<TilemapController>().activePanel = true;
                }
                // make the tile platform
                tileState = state.Platformed;
                // corner check
                if (!transform.root.GetComponent<Pathfinding>().cornerCheck())
                {
                    // if false, remove the platform and give error.
                    this.transform.GetComponent<Animator>().SetTrigger("error");
                    tileState = state.Empty;
                    transform.root.GetComponent<TilemapController>().PanelDeactivate(panelEmpty);
                    //this.GetComponent<SpriteRenderer>().sprite = null;
                }
                tileState = state.Empty;



                // Platform
                break;
            case state.Platformed:
                GameObject panelPlatform = null;
                foreach (GameObject go in Resources.FindObjectsOfTypeAll(typeof(GameObject)) as GameObject[])
                {
                    if (go.name.Equals("Platform - Panel"))
                        panelPlatform = go;
                }
                if (!this.transform.root.GetComponent<TilemapController>().activePanel)
                {
                    transform.root.GetComponent<TilemapController>().activePanelTile = this;
                    panelPlatform.SetActive(true);
                    transform.root.GetComponent<TilemapController>().activePanel = true;
                }
                // Units
                // Back to Empty (Sell)
                break;
            case state.tower1:
                GameObject panelTower1 = null;
                foreach (GameObject go in Resources.FindObjectsOfTypeAll(typeof(GameObject)) as GameObject[])
                {
                    if (go.name.Equals("Tower1 - Panel"))
                        panelTower1 = go;
                }
                if (!this.transform.root.GetComponent<TilemapController>().activePanel)
                {
                    transform.root.GetComponent<TilemapController>().activePanelTile = this;
                    panelTower1.SetActive(true);
                    transform.root.GetComponent<TilemapController>().activePanel = true;
                }
                // Sell
                // Upgrade
                break;
            case state.tower2:
                GameObject panelTower2 = null;
                foreach (GameObject go in Resources.FindObjectsOfTypeAll(typeof(GameObject)) as GameObject[])
                {
                    if (go.name.Equals("Tower2 - Panel"))
                        panelTower2 = go;
                }
                if (!this.transform.root.GetComponent<TilemapController>().activePanel)
                {
                    transform.root.GetComponent<TilemapController>().activePanelTile = this;
                    panelTower2.SetActive(true);
                    transform.root.GetComponent<TilemapController>().activePanel = true;
                }
                // Sell
                // Upgrade
                break;
            case state.Mine:
                GameObject panelGoldmine = null;
                foreach (GameObject go in Resources.FindObjectsOfTypeAll(typeof(GameObject)) as GameObject[])
                {
                    if (go.name.Equals("Goldmine - Panel"))
                        panelGoldmine = go;
                }
                if (!this.transform.root.GetComponent<TilemapController>().activePanel)
                {
                    transform.root.GetComponent<TilemapController>().activePanelTile = this;
                    panelGoldmine.SetActive(true);
                    transform.root.GetComponent<TilemapController>().activePanel = true;
                }
                // Sell
                // Upgrade
                break;
        }
    }

    public int[] getPos()
    {
        int[] pos = new int[2];
        pos[0] = posx;
        pos[1] = posy;
        return pos;
    }
    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.transform.CompareTag("Enemy"))
        {
            if(collision.GetComponent<EnemyAI>().behaviour != "tilepath")
            {
                collision.GetComponent<EnemyAI>().behaviour = "tilepath";
                collision.GetComponent<EnemyAI>().currentOnTile = this;
            }
        }
    }
    public void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.transform.CompareTag("Enemy"))
        {
            if(Vector3.Distance(this.transform.position,collision.transform.position) < 0.1f)
            collision.GetComponent<EnemyAI>().currentOnTile = this;
        }
        
    }
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