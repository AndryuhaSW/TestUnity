using UnityEngine;
using UnityEngine.UI;
using Zenject;

public class FsmState_AttackStage : MonoBehaviour, FsmState
{
    [SerializeField] private Text waveNumber_text;


    private LevelManager levelManager;
    private WaveCounter waveCounter;
    private FsmManager fsmManager;

    [Inject]
    public void Inject(LevelManager levelManager, 
        WaveCounter waveCounter, FsmManager fsmManager)
    {
        this.levelManager = levelManager;
        this.waveCounter = waveCounter;
        this.fsmManager = fsmManager;
    }

    public void Enter()
    {
        levelManager.StartLevel();
        waveNumber_text.gameObject.SetActive(true);
        waveNumber_text.text = $"{waveCounter.GetCurrentWave()+1}/{waveCounter.GetMaxWaveNumber()+1}";
    }

    public void Exit()
    {
        waveNumber_text.gameObject.SetActive(false);
    }

    public void SetState()
    {
        fsmManager.fsm.SetState(GameState.AttakStage);
    }
}
