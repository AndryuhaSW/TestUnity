using UnityEngine;
using UnityEngine.UI;

public class FsmState_AttackStage : MonoBehaviour, FsmState
{
    [SerializeField]
    private Text waveNumber_text;

    [SerializeField] private LevelManager _levelManager;
    [SerializeField] private CardConveyor cardConveyor;
    


    public void Enter()
    {
        _levelManager.LoadLevel(_levelManager.CurrentLevel);
        cardConveyor.Initialize();
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
