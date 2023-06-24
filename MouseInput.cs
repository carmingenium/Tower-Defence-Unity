using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class MouseInput : MonoBehaviour
{
    Tile thisTile;
    Sprite current;
    public Sprite target;
    private void Start()
    {
        thisTile = this.GetComponent<Tile>();
        current = this.GetComponent<SpriteRenderer>().sprite;
    }
    private void OnMouseDown()
    {
        thisTile.ChangeSprite();
    }
}
