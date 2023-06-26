using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TilemapController : MonoBehaviour
{
    public Tile activePanelTile;
    public bool activePanel;

    public void PanelDeactivate(GameObject panel)
    {
        panel.SetActive(false);
        activePanelTile = null;
        activePanel = false;
    }
    public void ChangeState(Sprite inputSprite)
    {
        //activePanelTile.tileState = ReceiveState();
        activePanelTile.transform.gameObject.GetComponent<SpriteRenderer>().sprite = inputSprite;
    }
    public void ChangeState(GameObject button)
    {
        activePanelTile.tileState = ReceiveState(button.name);
        activePanelTile.transform.gameObject.GetComponent<SpriteRenderer>().sprite = button.transform.GetComponent<Image>().sprite;
        PanelDeactivate(button.transform.parent.gameObject);
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

}
