using UnityEngine;
using Zenject;

public class FsmState_Loss : MonoBehaviour, FsmState
{
    [SerializeField] private GameObject manu;

    private SheepManager SheepManager;
    private FsmManager fsmManager;

    [Inject]
    public void Inject(SheepManager SheepManager, FsmManager fsmManager)
    {
        this.SheepManager = SheepManager;
        this.fsmManager = fsmManager;
    }

    public void Enter()
    {
        manu.SetActive(true);
    }

    public void Exit()
    {
        manu.SetActive(false);
    }

    public void SetState()
    {
        fsmManager.fsm.SetState(GameState.Loss);
    }

    private void OnEnable()
    {
        SheepManager.AllSheepStolen += SetState;
    }

    private void OnDisable()
    {
        SheepManager.AllSheepStolen -= SetState;
    }
}
