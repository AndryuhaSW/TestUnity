using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

public class FsmState_PrepareStage : MonoBehaviour, FsmState
{
    [SerializeField]
    private GameObject _menu;
    [SerializeField]
    private Text _timer;
    [SerializeField]
    private int _timeDuration;

    private WaveCounter waveCounter;
    private FsmManager fsmManager;
    private FsmState_AttackStage fsmState_AttackStage;

    [Inject]
    public void Inject(WaveCounter waveCounter,
        FsmManager fsmManager, FsmState_AttackStage fsmState_AttackStage)
    {
        this.waveCounter = waveCounter;
        this.fsmManager = fsmManager;
        this.fsmState_AttackStage = fsmState_AttackStage;
    }

    public void Enter()
    {
        _menu.SetActive(true);

        StartCoroutine(Timer());
    }

    public void Exit()
    {
        _menu.SetActive(false);

        StopAllCoroutines();
    }

    private IEnumerator Timer()
    {
        for (int i = _timeDuration; i >= 1; i--)
        {
            _timer.text = i.ToString();
            yield return new WaitForSeconds(1);
        }

        fsmState_AttackStage.SetState();
    }


    

    public void SetState()
    {
        fsmManager.fsm.SetState(GameState.PrepareStage);
    }


    private void OnEnable()
    {
        waveCounter.EndWave += SetState;
    }

    private void OnDisable()
    {
        waveCounter.EndWave -= SetState;
    }
}
