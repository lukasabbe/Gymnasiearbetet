using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MineScript : MonoBehaviour
{
    public int timeToMineBlock;

    private bool mining;
    private float mineTime;
    private void Start()
    {
        PlayerInputEventManager input = FindObjectOfType<PlayerInputEventManager>();
        input.leftMouseButtonHold += delegate (bool t) { mining = t; };
        mineTime = timeToMineBlock;
    }
    private void Update()
    {
        if (mining)
        {
            mine();
        }
    }
    void mine()
    {
        if (Physics.Raycast(GameManager.playerCamera.transform.position, GameManager.playerCamera.transform.forward, out RaycastHit ray, 3, Layers.resource))
        {
            mineTime -= Time.deltaTime;
            if(mineTime <= 0)
            {
                ray.transform.GetComponent<MineresourceStats>().DestroyRecurece();
            }
        }
        else
        {
            mineTime = timeToMineBlock;
        }
    }
}
