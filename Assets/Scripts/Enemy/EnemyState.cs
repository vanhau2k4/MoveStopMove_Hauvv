using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyState 
{
    protected EnemyStateMachine stateMachine;
    protected Enemy enemy;
    public NavMeshAgent agent;
    private string animboolName;
    protected bool triggerCalled;
    public EnemyState(Enemy _enemy, EnemyStateMachine _stateMachine, string _animboolName)
    {
        this.enemy = _enemy;
        this.stateMachine = _stateMachine;
        this.animboolName = _animboolName;
    }

    public virtual void Enter()
    {
        enemy.anim.SetBool(animboolName, true);
    }
    public virtual void Update()
    {
        agent = enemy.agent;
    }
    public virtual void Exit() 
    {
        enemy.anim.SetBool(animboolName, false);
    }
    public virtual void AnimationFinishTrigger()
    {
        triggerCalled = true;
    }
}
