using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;


public class MouseInput : MonoBehaviour
{
    Tile thisTile;
    Sprite current;
    private void Start()
    {
        thisTile = this.GetComponent<Tile>();
        current = this.GetComponent<SpriteRenderer>().sprite;
    }
    private void OnMouseUp()
    {
        // there is a bug that sometimes this doesnt trigger correctly
        // I think I wont be able to fix this as it seems like a problem about unity's input system and I dont want to do extra work for a prototype
        thisTile.PanelOpen();
    }

}
