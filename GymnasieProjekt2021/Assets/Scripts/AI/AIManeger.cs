using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class AIManeger : MonoBehaviour
{
    public enum AiStages
    {
        Roaming,
        following,
        attacking
    }

    //Publics variables
    public Transform Player;
    public int viewAngle;
    public AiStages stage;
    public NavMeshAgent agent;
    [HideInInspector]
    public int AIType = 0;

    //privete variables

    private Vector3 randomPos;
    private float AttackCounter = 4f;
    private bool seePlayer;
    private System.Random rnd = new System.Random();
    private bool startRoming = true;


    //distence
    private float distanceToAttackMode;

    //Melee attack
    [HideInInspector]
    public float Melee_attackDMG;
    [HideInInspector]
    public float Melee_distanceToAttackMode;

    private void Start()
    {
        if(AIType == 0)
        {

        }
        else if (AIType == 1)
        {
            distanceToAttackMode = Melee_distanceToAttackMode;
        }
    }

    private void Update()
    {
        switch (stage)
        {
            case AiStages.Roaming:
                searching();
                RoamAround();
                break;
            case AiStages.following:
                startRoming = true;
                following();
                break;
            case AiStages.attacking:
                startRoming = true;
                MeleeAttack();
                break;
        }
    }

    private void MeleeAttack()
    {
        if (AIseePlayer())
        {
            if(Vector3.Distance(Player.position, transform.position) <= 1.2f)
            {
                Debug.Log("yess");
                agent.SetDestination(transform.position);
                agent.transform.LookAt(Player);
            }
            else
            {
                agent.SetDestination(Player.position);
                agent.transform.LookAt(Player);
            }
        }
        else stage = AiStages.following;
    }

    private void RoamAround()
    {
        if(randomPos == transform.position || startRoming)
        {
            randomPos = new Vector3(transform.position.x + rnd.Next(-5, 6), transform.position.y, transform.position.z + rnd.Next(-5, 6));
            NavMeshHit hit;
            if (NavMesh.SamplePosition(randomPos, out hit, 1f , NavMesh.AllAreas))
            {
                randomPos = hit.position;
            }
            agent.SetDestination(randomPos);
            startRoming = false;
        }
    }
    private void following()
    {
        Debug.Log(seePlayer);
        AttackCounter -= Time.deltaTime;
        if(AttackCounter <= 0)
        {
            if(seePlayer == false)
            {
                agent.SetDestination(transform.position);
                stage = AiStages.Roaming;
            }
            AttackCounter = 4f;
        }
        else
        {
            agent.SetDestination(Player.position);
            agent.transform.LookAt(Player);
            if(Vector3.Distance(transform.position,Player.position) <= distanceToAttackMode )
            {
                agent.SetDestination(transform.position);
                stage = AiStages.attacking;
            }
        }
        seePlayer = AIseePlayer();
    }
    private void searching()
    {
        for (int i = 0; i < viewAngle; i++)
        {
            Ray ray = new Ray();
            ray.origin = gameObject.transform.position;
            ray.direction = Quaternion.AngleAxis((i - (viewAngle / 2)) * 10, Vector3.down) * transform.forward;
            RaycastHit hit = new RaycastHit();
            if (Physics.Raycast(ray, out hit, 50))
            {
                Debug.DrawRay(ray.origin, ray.direction, Color.red);
                if (hit.collider.gameObject.tag == "Player")
                {
                    stage = AiStages.following;
                }
            }
            else
            {
                Debug.DrawRay(ray.origin, ray.direction, Color.white);
            }
        }
    }
    private bool AIseePlayer()
    {
        for (int i = 0; i < viewAngle; i++)
        {
            Ray ray = new Ray();
            ray.origin = gameObject.transform.position;
            ray.direction = Quaternion.AngleAxis((i - (viewAngle / 2)) * 10, Vector3.down) * transform.forward;
            RaycastHit hit = new RaycastHit();
            if (Physics.Raycast(ray, out hit, 50))
            {
                if (hit.collider.gameObject.tag == "Player")
                {
                    return true;
                }
            }
            else
            {
            }
        }
        return false;
    }
} 