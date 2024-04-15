using UnityEngine;

public class FsmManager : MonoBehaviour
{
    [SerializeField]
    private FsmState_StartMenu state_startMenu;
    [SerializeField]
    private FsmState_PrepareStage state_prepareStage;
    [SerializeField]
    private FsmState_AttackStage state_attackStage;
    [SerializeField]
    private FsmState_Win state_win;
    [SerializeField]
    private FsmState_Loss state_loss;

    public static Fsm Fsm { get; private set; }

    private void Start()
    {
        Fsm = new Fsm();

        Fsm.AddState(GameState.StartMenu, state_startMenu);
        Fsm.AddState(GameState.PrepareStage, state_prepareStage);
        Fsm.AddState(GameState.AttakStage, state_attackStage);
        Fsm.AddState(GameState.Win, state_win);
        Fsm.AddState(GameState.Loss, state_loss);

        Fsm.SetState(GameState.StartMenu);
    }
}
