using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class FsmState_Loss : MonoBehaviour, FsmState
{
    [SerializeField] private GameObject manu;

    public void Enter()
    {
        Debug.Log(6);
        manu.SetActive(true);
    }

    public void Exit()
    {
        manu.SetActive(false);
    }

    public static void SetState()
    {
        Debug.Log(5);
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
