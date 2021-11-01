using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerStatsMono : MonoBehaviour
{
    public float HP;
    public float FoodLevel;

    public static Text HealthText;
    
    private void Awake()
    {
        PlayerStats.HP = HP;
        PlayerStats.MaxHP = HP;
        PlayerStats.FoodLevel = FoodLevel;
        HealthText = GameObject.Find("HealtText").GetComponent<Text>();
        updateHealthText();
    }

    public static void updateHealthText()
    {
        HealthText.text = $"Healt : {PlayerStats.HP}";
    }

}
