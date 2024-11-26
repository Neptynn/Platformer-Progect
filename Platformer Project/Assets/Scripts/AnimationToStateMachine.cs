using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationToStateMachine : MonoBehaviour
{
    public AttackState attackState;

    private void TriggerAttack()
    {
        attackState.TrigerAttack();
    }
    private void FinishedAttack()
    {
        attackState.FinishAttacking();
    }
}
