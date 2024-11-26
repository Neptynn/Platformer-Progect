using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerGraundedStates : PlayerState
{
    protected int xInput;
    private bool JumpInput;
    public PlayerGraundedStates(Player player, PlayerStateMachine stateMachine, PlayerData playerData, string animBoolName) : base(player, stateMachine, playerData, animBoolName)
    {
    }

    public override void DoChecks()
    {
        base.DoChecks();
    }

    public override void Enter()
    {
        base.Enter();
    }

    public override void Exit()
    {
        base.Exit();
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();

        xInput = player.inputHandler.NormInputX;
        JumpInput = player.inputHandler.JumpInput;

        if(JumpInput)
        {
            player.inputHandler.UseJumpInput();
            stateMachine.ChangeState(player.JumpState);
        }
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
    }
}
