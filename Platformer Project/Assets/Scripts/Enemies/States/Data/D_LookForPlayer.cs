using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "newLookForPlayerStateData", menuName = "Data/State Data/Look For Player State")]
public class D_LookForPlayer : ScriptableObject
{
    public int amountOfTurnes = 2;
    public float timeBeetwenTurns = 0.75f;
}
