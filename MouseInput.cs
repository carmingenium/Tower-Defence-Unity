using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;
using UnityEngine.UI;


public class MouseInput : MonoBehaviour
{
    public Transform pause;
    // NEW INPUT SYSTEM //
    private Selecting selectAction;

    public void Awake()
    {
        selectAction = new Selecting(); 
    }
    private void OnEnable()
    {
        selectAction.Camera.Select.canceled += Release;
        selectAction.Camera.Enable();
    }
    private void OnDisable()
    {
        selectAction.Camera.Select.canceled -= Release;
    }
    // NEW INPUT SYSTEM //

    private void Release(InputAction.CallbackContext obj) // on click release
    {
        // Debug.Log(Input.mousePosition);
        Vector3 MousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        MousePos = new Vector3(MousePos.x, MousePos.y, MousePos.z + 2);
        // Debug.Log(MousePos);


        TilemapController tileMap = GameObject.FindGameObjectWithTag("Tilemap").GetComponent<TilemapController>();
        // check each tile for distance to mouseposition to get clicked tile.
        foreach (Tile tile in tileMap.TileArray)
        {
            if (Vector3.Distance(MousePos, tile.transform.position) < 0.5f)
            {
                // if(Vector3.Distance(MousePos, pause.position) > 12f) // an attempt to fix pause menu opening a panel bug
                tile.PanelOpen(); 
            }
        }
    }



}
