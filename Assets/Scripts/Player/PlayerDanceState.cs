using System.Collections;
using System.Collections.Generic;
using UnityEditor.Timeline.Actions;
using UnityEngine;

public class PlayerDanceState : PlayerState
{
    public PlayerDanceState(Player _player, PlayerStateMachine _stateMachine, string _animBoolState) : base(_player, _stateMachine, _animBoolState)
    {
    }

    public override void Enter()
    {
        base.Enter();
        souscePlay.src.clip = souscePlay.win;
        souscePlay.src.Play();
        player.StartCoroutine(DelayedWin());
        player.StartCoroutine(DelayedWins());
    }
    private IEnumerator DelayedWin()
    {
        yield return new WaitForSeconds(3);
        Win();
    }
    private IEnumerator DelayedWins()
    {
        yield return new WaitForSeconds(3);
        ExitWin();
    }
    public override void Exit()
    {
        base.Exit();
        
    }

    public override void Update()
    {
        base.Update();
        
    }
    public void ExitWin()
    {
        stateMachine.ChangeState(player.playerIdleState);
    }
    public void Win()
    {
        player.canvasDie.gameObject.SetActive(true);
    }
}
