using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class FsmState_PrepareStage : MonoBehaviour, FsmState
{
    [SerializeField]
    private GameObject _menu;
    [SerializeField]
    private Text _timer;
    [SerializeField]
    private int _timeDuration;

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

        FsmState_AttackStage.SetState();
    }


    private void OnEnable()
    {
        LevelManager.AllEnemiesDead += SetState;
    }

    private void OnDisable()
    {
        LevelManager.AllEnemiesDead -= SetState;
    }

    public static void SetState()
    {   
        FsmManager.Fsm.SetState(GameState.PrepareStage);
    }
}
