using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class AIManeger : MonoBehaviour
{
    public enum AiStages
    {
        Roaming,
        attacking
    }
    public Transform Player;
    public int viewAngle;
    public AiStages stage;
    public NavMeshAgent agent;
    private void Update()
    {
        switch (stage)
        {
            case AiStages.Roaming:
                searching();
                break;
            case AiStages.attacking:
                break;
        }
    }

    private void searching()
    {
        for (int i = 0; i < viewAngle; i++)
        {
            Ray ray = new Ray();
            ray.origin = gameObject.transform.position;
            ray.direction = Quaternion.AngleAxis((i - (viewAngle / 2)) * 10, Vector3.down) * Vector3.right + transform.forward;
            RaycastHit hit = new RaycastHit();
            if (Physics.Raycast(ray, out hit, 50))
            {
                Debug.DrawRay(ray.origin, ray.direction, Color.red);
                if (hit.collider.gameObject.tag == "Player")
                {
                    //stage = AiStages.attacking;
                    agent.SetDestination(hit.point);
                    agent.transform.rotation = Quaternion.LookRotation(-hit.point, Vector3.up);
                }
            }
            else
            {
                Debug.DrawRay(ray.origin, ray.direction, Color.white);
            }
        }
    }
} 


