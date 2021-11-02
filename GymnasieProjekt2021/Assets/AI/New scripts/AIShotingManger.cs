using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIShotingManger : MonoBehaviour
{
    //public
    public GameObject shot;


    //privet
    float shotTimer = 2f;
    AIManeger aIManeger;
    private void Start()
    {
        aIManeger = GetComponent<AIManeger>();
    }
    private void Update()
    {
        if(aIManeger.stage == AIManeger.AiStages.attacking)
        {
            shotTimer -= Time.deltaTime;
            if(shotTimer <= 0)
            {
                Instantiate(shot , transform.position + (transform.forward * 2), transform.rotation);
                shotTimer = 2f;
            }
        }
    }
}
