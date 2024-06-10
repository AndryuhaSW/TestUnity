using UnityEngine;
using UnityEngine.UI;

public class FsmState_AttackStage : MonoBehaviour, FsmState
{
    [SerializeField] private Text waveNumber_text;

    [SerializeField] private LevelManager _levelManager;
    


    public void Enter()
    {
        LevelManager.instance.StartLevel();
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
