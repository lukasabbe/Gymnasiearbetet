using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackStateBase : INPCState
{
    public virtual INPCState DoState(NPCBase npc){
        return npc.wanderState;
    }
}
