using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tool : MonoBehaviour
{
    PlayerInputEventManager input;
    private void Awake() {
        PlayerInputEventManager input = FindObjectOfType<PlayerInputEventManager>();
    }
    void OnEnable(){
        input.leftMouseButton += OnLeftClick;
    }
    private void OnDisable() {
        input.leftMouseButton -= OnLeftClick;
    }
    void OnLeftClick(){
        
    }
}
