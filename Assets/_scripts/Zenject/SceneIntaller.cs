using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using UnityEngine;
using Zenject;

public class SceneIntaller : MonoInstaller
{
    [SerializeField] private List<Sheep> sheeps;

    public List<LevelSettings> levelsSettings;
    public SheepManager sheepManager;
    public WaveCounter waveCounter;
    public EnemyCounter enemyCounter;
    public LevelBackground levelBackground;
    public LevelManager levelManager;

    public EnemyFactory enemyFactory;

    public Wallet wallet;

    public FsmState_StartMenu state_startMenu;
    public FsmState_PrepareStage state_prepareStage;
    public FsmState_AttackStage state_attackStage;
    public FsmState_Win state_win;
    public FsmState_Loss state_loss;
    public FsmManager fsmManager;

    public override void InstallBindings()
    {
        Container.BindInstance(levelsSettings);
        Container.BindInstance(sheeps);

        Container.BindInstance(sheepManager);
        Container.BindInstance(waveCounter);
        Container.BindInstance(enemyCounter);
        Container.BindInstance(levelBackground);
        Container.BindInstance(levelManager);
        Container.BindInstance(wallet);

        Container.BindInstance(enemyFactory);

        Container.BindInstance(state_startMenu);
        Container.BindInstance(state_prepareStage);
        Container.BindInstance(state_attackStage);
        Container.BindInstance(state_win);
        Container.BindInstance(state_loss);
        Container.BindInstance(fsmManager);


    }
}
