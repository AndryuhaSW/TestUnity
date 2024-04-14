
using UnityEngine;
using UnityEngine.UI;

public class FsmState_AttackStage : MonoBehaviour, FsmState
{
    [SerializeField]
    private Text waveNumber_text;

    private LevelManager _levelManager;

    

    private void Awake()
    {
        _levelManager = GetComponent<LevelManager>();
    }

    public void Enter()
    {
        _levelManager.LoadLevel(_levelManager.CurrentLevel);

        waveNumber_text.gameObject.SetActive(true);
        waveNumber_text.text = $"{_levelManager.GetCurrentWaveNumber()+1}/{_levelManager.GetMaxWaveNumber()+1}";
    }

    public void Exit()
    {
        waveNumber_text.gameObject.SetActive(false);
    }

    public static void SetState()
    {
        FsmManager.Fsm.SetState(GameState.AttakStage);
    }

}
