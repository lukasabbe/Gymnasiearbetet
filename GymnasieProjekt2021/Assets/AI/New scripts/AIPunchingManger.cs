using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIPunchingManger : MonoBehaviour
{
    private float attackTimmer = 1.5f;
    private AIManeger aIManeger;
    //private GameObject punchBlock;
    private Vector3 startPos;
    private void Start()
    {
        aIManeger = GetComponent<AIManeger>();
    }
    private void Update()
    {
        if (aIManeger.stage == AIManeger.AiStages.attacking)
        {
            attackTimmer -= Time.deltaTime;
            if(attackTimmer<= 0)
            {
                punchAttack();
                attackTimmer = 1.5f;
            }
        }
    }
    void punchAttack()
    {
        PlayerStats.HP -= 1;
        PlayerStatsMono.updateHealthText();
    }
}
