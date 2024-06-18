using UnityEngine;
using Zenject;

public class FsmManager : MonoBehaviour
{
    private FsmState_StartMenu state_startMenu;
    private FsmState_PrepareStage state_prepareStage;
    private FsmState_AttackStage state_attackStage;
    private FsmState_Win state_win;
    private FsmState_Loss state_loss;

    [Inject]
    public void Inject(
        FsmState_StartMenu state_startMenu,
        FsmState_PrepareStage state_prepareStage,
        FsmState_AttackStage state_attackStage,
        FsmState_Win state_win,
        FsmState_Loss state_loss)
    {
        this.state_startMenu = state_startMenu;
        this.state_prepareStage = state_prepareStage;
        this.state_attackStage = state_attackStage;
        this.state_win = state_win;
        this.state_loss = state_loss;
    }

    public Fsm fsm { get; private set; }

    private void Start()
    {
        fsm = new Fsm();

        fsm.AddState(GameState.StartMenu, state_startMenu);
        fsm.AddState(GameState.PrepareStage, state_prepareStage);
        fsm.AddState(GameState.AttakStage, state_attackStage);
        fsm.AddState(GameState.Win, state_win);
        fsm.AddState(GameState.Loss, state_loss);

        fsm.SetState(GameState.StartMenu);
    }
}
