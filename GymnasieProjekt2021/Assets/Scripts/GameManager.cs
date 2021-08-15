using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class GameManager{
    public static GameObject player = GameObject.FindGameObjectWithTag("Player");
    public static GameObject playerCamera = GameObject.FindGameObjectWithTag("MainCamera");
}
public static class Layers{
    // När du lägger till ett Layer så måste du skriva 1 << 'index'. Annars funkar det inte.
    public static int ground = 1 << 12;
    public static int structure = 1 << 16;
}
