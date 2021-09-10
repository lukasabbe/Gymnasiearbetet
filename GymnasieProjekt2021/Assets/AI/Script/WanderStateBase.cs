using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class WanderStateBase : INPCState
{
    public virtual INPCState DoState(NPCBase npc){
        return npc.wanderState;
    }
}
