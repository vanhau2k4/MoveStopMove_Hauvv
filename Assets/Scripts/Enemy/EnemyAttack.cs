using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAttack : EnemyState
{
    public EnemyAttack(Enemy _enemy, EnemyStateMachine _stateMachine, string _animboolName) : base(_enemy, _stateMachine, _animboolName)
    {
    }

    public override void Enter()
    {
        base.Enter();
    }

    public override void Exit()
    {
        base.Exit();
    }

    public override void Update()
    {
        base.Update();
        if (agent.velocity.magnitude > 0) stateMachine.ChangeState(enemy.enemyMove);
        if (agent.velocity.magnitude == 0 && enemy.target == null) stateMachine.ChangeState(enemy.enemyIdle);
    }
}
