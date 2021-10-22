using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
public class PlayerInputEventManager : MonoBehaviour {

    public event Action leftMouseButton;
    public event Action leftMouseButtonUp;
    public event Action<bool> leftMouseButtonHold;
    public event Action rightMouseButton;
    public event Action inventoryKey;
    public event Action scroll;
    public event Action debugKey;
    public event Action debugSpawnItemKey;
    public event Action jumpKey;
    public event Action openStructKey;
    public event Action dropKey;
    public event Action pickUpItemKey;
    public event Action<int> onHotBarDelta;

    private void Update() {

        leftMouseButtonHold?.Invoke(Input.GetKey(KeyCode.Mouse0));

        if (Input.GetKeyUp(KeyCode.Mouse0))
            {
                leftMouseButtonUp?.Invoke();
            }

        if (Input.anyKey)
        {
            if (Input.GetKeyDown(KeyCode.Mouse0))
            {
                leftMouseButton?.Invoke();
            }



            if (Input.GetKeyDown(KeyCode.Mouse1)){
                rightMouseButton?.Invoke();
            }

            if (Input.mouseScrollDelta != Vector2.zero)
            {
                scroll?.Invoke();
            }

            if (Input.GetKeyDown(KeyCode.O)) {
                debugKey?.Invoke();
            }

            if (Input.GetKeyDown(KeyCode.Tab))
            {
                inventoryKey?.Invoke();
            }

            if (Input.GetKeyDown(KeyCode.P))
            {
                debugSpawnItemKey?.Invoke();
            }

            if (Input.GetKeyDown(KeyCode.Space))
            {
                jumpKey?.Invoke();
            }

            if(Input.GetKeyDown(KeyCode.E))
            {
                openStructKey?.Invoke();
                pickUpItemKey?.Invoke();
            }

            if (Input.GetKeyDown(KeyCode.Q))
            {
                dropKey?.Invoke();
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
