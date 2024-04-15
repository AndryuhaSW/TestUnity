using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FsmState_Loss : MonoBehaviour, FsmState
{
    public void Enter()
    {
        
    }

    public void Exit()
    {
        
    }

    public static void SetState()
    {
        FsmManager.Fsm.SetState(GameState.Loss);
    }

    private void OnEnable()
    {
        LevelManager.AllSheepsStealStolen += SetState;
    }

    private void OnDisable()
    {
        LevelManager.AllSheepsStealStolen -= SetState;
    }
}
