using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
public class PlayerInputEventManager : MonoBehaviour {

    public event Action leftMouseButton;
    public event Action rightMouseButton;
    public event Action inventoryKey;
    public event Action scroll;
    public event Action debugKey;
    public event Action debugSpawnItemKey;

    private void Update() {

        if (Input.GetKeyDown(KeyCode.Mouse0)) {
            leftMouseButton();
        }

        if (Input.GetKeyDown(KeyCode.Mouse1)){
            rightMouseButton();
        }

        if (Input.mouseScrollDelta != Vector2.zero)
        {
            scroll();
        }

        if (Input.GetKeyDown(KeyCode.O)) {
            debugKey();
        }

        if (Input.GetKeyDown(KeyCode.Tab))
        {
            inventoryKey();
        }

        if (Input.GetKeyDown(KeyCode.P))
        {
            debugSpawnItemKey();
        }
    }
}
