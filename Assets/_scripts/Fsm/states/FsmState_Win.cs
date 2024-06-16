using UnityEngine;

public class FsmState_Win : MonoBehaviour, FsmState
{
    [SerializeField] private GameObject _winMenu;


    public void Enter()
    {
        _winMenu.SetActive(true);
    }

    public void Exit()
    {
        _winMenu.SetActive(false);
    }

    public static void SetState()
    {
        FsmManager.Fsm.SetState(GameState.Win);
    }

    private void OnEnable()
    {
        LevelManager.instance.WinGame += SetState;
    }

    private void OnDisable()
    {
        LevelManager.instance.WinGame -= SetState;
    }
}
