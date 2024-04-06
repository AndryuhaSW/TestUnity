
using UnityEngine;

public class FsmState_StartMenu : MonoBehaviour, FsmState
{
    [SerializeField]
    private GameObject menu;

    public void Enter()
    {
        menu.SetActive(true);
    }

    public void Exit()
    {
        menu.SetActive(false);
    }

    public static void SetState()
    {
        FsmManager.Fsm.SetState("StartMenu");
    }
}
