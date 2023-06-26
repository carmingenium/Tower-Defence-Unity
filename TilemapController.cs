using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class TilemapController : MonoBehaviour
{
    public Tile activePanelTile;
    public bool activePanel;
    public Tile[,] TileArray = new Tile[20,20];

    public Sprite[] allSprites = new Sprite[6];
    System.Random roll;
    public void Start()
    {
        int loopInt = 0;
        foreach (Sprite sr in Resources.LoadAll("UsedSprites", typeof(Sprite))){
            allSprites[loopInt++] = sr; // index was outside the bounds of array error.
        }
        // CREATING THE TILEARRAY //
        foreach (GameObject go in GameObject.FindGameObjectsWithTag("Tile"))
        {
            // go.parent.name = Y
            // go.position.x = X
            int yVal = int.Parse(go.transform.parent.name);
            TileArray[(int)go.transform.localPosition.x , yVal] = go.GetComponent<Tile>();
        }
        // SETTING UP START VALUES  //
        MapGen();
    }

    public void PanelDeactivate(GameObject panel)
    {
        panel.SetActive(false);
        activePanelTile = null;
        activePanel = false;
    }
    public void ChangeState(GameObject button)
    {
        activePanelTile.tileState = ReceiveState(button.name);
        activePanelTile.transform.gameObject.GetComponent<SpriteRenderer>().sprite = button.transform.GetComponent<Image>().sprite;
        PanelDeactivate(button.transform.parent.gameObject);
    }
    public void ChangeState(Tile tile,state Tilestate, Sprite StateSprite)
    {

    }
    public state ReceiveState(string stateString)
    {
        switch (stateString)
        {
            case "Empty":
                return state.Empty;
            case "Platform":
                return state.Platformed;
            case "Tower1":
                return state.tower1;
            case "Tower2":
                return state.tower2;
        }
        return state.Corrupted; // ERROR
    }
    
    public void MapGen()
    {
        // TARGET GEN //
        int TargetX = roll.Next(7, 13);
        int TargetY = roll.Next(7, 13);
        // find target sprite //
        Sprite targetSprite = null;
        foreach(Sprite sr in allSprites)
        {
            if (sr.name.Equals("TARGET_128"))
            {
                targetSprite = sr;
            }
        }
        ChangeState(TileArray[TargetX, TargetY], state.Target, targetSprite);

    }

}
