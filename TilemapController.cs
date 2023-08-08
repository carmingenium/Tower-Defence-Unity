using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class TilemapController : MonoBehaviour
{
    public Tile activePanelTile;
    public bool activePanel;
    public Tile[,] TileArray = new Tile[20,20];
    public Sprite[] allSprites;

    public GameObject UnitRange;
    public void Start()
    {
        allSprites = new Sprite[9];
        int loopInt = 0;
        foreach (Sprite sr in Resources.LoadAll("UsedSprites", typeof(Sprite))){
            allSprites[loopInt++] = sr;
        }
        // CREATING THE TILEARRAY //
        foreach (GameObject go in GameObject.FindGameObjectsWithTag("Tile"))
        {
            // go.parent.name = Y
            // go.position.x = X
            int yVal = int.Parse(go.transform.parent.name);
            TileArray[(int)go.transform.localPosition.x , yVal] = go.GetComponent<Tile>();
            go.GetComponent<Tile>().posx = (int)go.transform.localPosition.x;
            go.GetComponent<Tile>().posy = yVal;
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
    public void ChangeState(GameObject button) // in game tile change
    {
        // set tilestate
        activePanelTile.tileState = ReceiveState(button.name);
        activePanelTile.transform.gameObject.GetComponent<SpriteRenderer>().sprite = button.transform.GetComponent<Image>().sprite;

        // Unit creation
        if(activePanelTile.tileState.Equals(state.tower1) || activePanelTile.tileState.Equals(state.tower2))
        {
            CreateUnit(activePanelTile);
        }

        // panel close
        PanelDeactivate(button.transform.parent.gameObject);
        this.gameObject.GetComponent<Pathfinding>().pathfindValueSet();
    }
    public void ChangeState(Tile tile,state Tilestate, Sprite StateSprite) // Start tile setting
    {
        tile.tileState = Tilestate;
        tile.gameObject.GetComponent<SpriteRenderer>().sprite = StateSprite;
        if (Tilestate.Equals(state.Target))
        {
            tile.gameObject.AddComponent<TargetTile>();
        }
    }
    public state ReceiveState(string stateString)
    {
        switch (stateString)
        {
            case "Empty":
                return state.Empty;
            case "Platform":
                activePanelTile.GetComponent<Collider2D>().isTrigger = false;
                return state.Platformed;
            case "Goldmine":
                return state.Mine;
            case "Gold":
                return state.Resource;
            case "Tower1":
                activePanelTile.GetComponent<Collider2D>().isTrigger = false;
                return state.tower1;
            case "Tower2":
                activePanelTile.GetComponent<Collider2D>().isTrigger = false;
                return state.tower2;
        }
        return state.Corrupted; // ERROR
    }
    
    public void MapGen()
    {
        // TARGET GEN //
        int TargetX = Random.Range(7, 13);
        int TargetY = Random.Range(7, 13);
        // find target sprite //
        Sprite targetSprite = allSprites[7]; // targetsprite location = 4
        ChangeState(TileArray[TargetX, TargetY], state.Target, targetSprite);
        // changing targets collider, so that it can take damage
        TileArray[TargetX, TargetY].gameObject.GetComponent<Collider2D>().isTrigger = false;
        // adding target to the genList, so that it wont be used again in gen.


        // Corruption Gen
        ChangeState(TileArray[TargetX + 1, TargetY], state.Corrupted, allSprites[8]);
        ChangeState(TileArray[TargetX - 1, TargetY], state.Corrupted, allSprites[8]);
        ChangeState(TileArray[TargetX + 1, TargetY + 1], state.Corrupted, allSprites[8]);
        ChangeState(TileArray[TargetX - 1, TargetY + 1], state.Corrupted, allSprites[8]);
        ChangeState(TileArray[TargetX + 1, TargetY - 1], state.Corrupted, allSprites[8]);
        ChangeState(TileArray[TargetX - 1, TargetY - 1], state.Corrupted, allSprites[8]);
        ChangeState(TileArray[TargetX, TargetY - 1], state.Corrupted, allSprites[8]);
        ChangeState(TileArray[TargetX, TargetY + 1], state.Corrupted, allSprites[8]);
        // Gold Gen (2 for now)
        for(int i = 0; i < 2; i++)
        {
            int GoldX = -1;
            int GoldY = -1;
            do
            {
                GoldX = Random.Range(2, 18);
                GoldY = Random.Range(2, 18);
            } while (TileArray[GoldX, GoldY].tileState != state.Empty);
            ChangeState(TileArray[GoldX, GoldY], state.Resource, allSprites[5]);
        }

        // Boulder Gen (2 for now)
        for (int i = 0; i < 2; i++)
        {
            int BldX = -1;
            int BldY = -1;
            do
            {
                BldX = Random.Range(2, 18);
                BldY = Random.Range(2, 18);
            } while (TileArray[BldX, BldY].tileState != state.Empty);
            ChangeState(TileArray[BldX, BldY], state.Boulder, allSprites[4]);
        }
    }

    public void CreateUnit(Tile unitTile)
    {
        unitTile.gameObject.AddComponent<UnitAction>();
        Instantiate(UnitRange, unitTile.gameObject.transform);
    }
}
