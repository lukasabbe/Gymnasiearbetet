using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class NPCBase : MonoBehaviour
{
    private INPCState currentState;
    public WanderStateBase wanderState;
    public AttackState attackState = new AttackState();

    [HideInInspector] public NavMeshAgent navAgent;

    [HideInInspector] public Vector3 spawnPoint;
    
    private void OnEnable() {
        navAgent = GetComponent<NavMeshAgent>();

        spawnPoint = transform.position;

        currentState = wanderState;
    }
    private void Update() {
        currentState = currentState.DoState(this);
    }
}
