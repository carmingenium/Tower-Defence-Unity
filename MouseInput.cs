using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class MouseInput : MonoBehaviour
{
    Sprite current;
    public Sprite target;
    private void Start()
    {
        current = this.GetComponent<SpriteRenderer>().sprite;
    }
    private void OnMouseDown()
    {
        ChangeSprite();
    }
    void ChangeSprite()
    {
        if (current != target)
            this.GetComponent<SpriteRenderer>().sprite = target;
    }
}
