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
    public event Action jumpKey;
    public event Action<int> onHotBarDelta;

    private void Update() {


        if (Input.anyKey)
        {
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

            if (Input.GetKeyDown(KeyCode.Space))
            {
                jumpKey();
            }
            for(int i = 0; i < 10; i++)
            {
                if (Input.GetKeyDown(i.ToString())){
                    onHotBarDelta.Invoke(i);
                }
            }
        }
    }
}
