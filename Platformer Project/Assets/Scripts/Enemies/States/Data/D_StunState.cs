using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "newStunState", menuName = "Data/State Data/Stun Data")]
public class D_StunState : ScriptableObject
{
    public float stunTime = 3f;

    public float stusKnockbackTime = 0.2f;
    public float stusKnockbackSpeed = 20f;
    public Vector2 stunKnockbackAngle; 


}
