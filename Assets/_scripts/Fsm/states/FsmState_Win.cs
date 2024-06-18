using UnityEngine;
using Zenject;

public class FsmState_Win : MonoBehaviour, FsmState
{
    [SerializeField] private GameObject _winMenu;

    private WaveCounter waveCounter;
    private FsmManager fsmManager;

    [Inject]
    public void Inject(WaveCounter waveCounter, FsmManager fsmManager)
    {
        this.waveCounter = waveCounter;
        this.fsmManager = fsmManager;
    }


    public void Enter()
    {
        _winMenu.SetActive(true);
    }

    public void Exit()
    {
        _winMenu.SetActive(false);
    }

    public void SetState()
    {
        fsmManager.fsm.SetState(GameState.Win);
    }

    private void OnEnable()
    {
        waveCounter.AllWavesOver += SetState;
    }

    private void OnDisable()
    {
        waveCounter.AllWavesOver -= SetState;
    }
}
