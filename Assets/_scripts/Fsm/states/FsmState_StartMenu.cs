
using UnityEngine;
using Zenject;

public class FsmState_StartMenu : MonoBehaviour, FsmState
{
    [SerializeField] private GameObject menu;

    private FsmManager fsmManager;

    [Inject]
    public void Inject(FsmManager fsmManager)
    {
        this.fsmManager = fsmManager;
    }

    public void Enter()
    {
        menu.SetActive(true);
    }

    public void Exit()
    {
        menu.SetActive(false);
    }

    public void SetState()
    {
        fsmManager.fsm.SetState(GameState.StartMenu);
    }
}
