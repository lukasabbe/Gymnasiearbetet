using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStatsMono : MonoBehaviour
{
    public float HP;
    public float FoodLevel;
    private void Awake()
    {
        PlayerStats.HP = HP;
        PlayerStats.FoodLevel = FoodLevel;
        Destroy(this);
    }
}
