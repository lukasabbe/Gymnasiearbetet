using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovmentStates
{
    public static MovementState States = MovementState.walking;
}
public enum MovementState {
    off,
    walking
}