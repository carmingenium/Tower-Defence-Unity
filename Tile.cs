using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tile : MonoBehaviour
{
    public state tileState;
    public int roadVal;

    public Sprite Platform;

    public void PanelOpen()
    {
        switch (tileState)
        {
            case state.Target:
                break;
            case state.Corrupted:
                break;
            case state.Resource:
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
                        panelEmpty = go;
                }
                if (!this.transform.root.GetComponent<TilemapController>().activePanel)
                {
                    transform.root.GetComponent<TilemapController>().activePanelTile = this;
                    panelEmpty.SetActive(true);
                    transform.root.GetComponent<TilemapController>().activePanel = true;
                }
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
                // Sell
                // Upgrade
                break;
            case state.tower2:
                // Sell
                // Upgrade
                break;
            case state.Mine:
                // Sell
                // Upgrade
                break;
        }
    }
    //public void ChangeSprite(state input)
    //{
    //    if(tileState == state.Empty)
    //    {
    //        tileState += 1;
    //        this.GetComponent<SpriteRenderer>().sprite = Platform;
    //    }
    //}
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