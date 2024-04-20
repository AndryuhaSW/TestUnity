using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class FsmState_Loss : MonoBehaviour, FsmState
{
    [SerializeField] private GameObject manu;

    public void Enter()
    {
        manu.SetActive(true);
    }

    public void Exit()
    {
        manu.SetActive(false);
    }

    public static void SetState()
    {
        FsmManager.Fsm.SetState(GameState.Loss);
    }

    private void OnEnable()
    {
        LevelManager.instance.AllSheepsStealStolen += SetState;
    }

    private void OnDisable()
    {
        LevelManager.instance.AllSheepsStealStolen -= SetState;
    }
}
