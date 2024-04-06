using System.Collections;
using System.Collections.Generic;
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
        TowerFactory towerFactory = TowerFactory.instance;

        //tmp variant. delete next time
        towerFactory.CreateTower("test", new Vector2(5, -2.5f));
        towerFactory.CreateTower("test", new Vector2(-15, -2.5f));

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
        FsmManager.Fsm.SetState("AttackStage");
    }

}
