using System;
using UnityEngine;

public class TowerFactory : MonoBehaviour
{
    [SerializeField] private RectTransform container;

    [SerializeField] private SinglePurposeTower singlePurposeTower;
    [SerializeField] private CircleRangeTower circleRangeTower;
    [SerializeField] private SplashTower splashTower;

    private PoolMono<SinglePurposeTower> singlePurposeTowerPool;
    private PoolMono<CircleRangeTower> circleRangeTowerPool;
    private PoolMono<SplashTower> splashTowerPool;

    public static TowerFactory instance { get; private set; }

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }

        singlePurposeTowerPool = new PoolMono<SinglePurposeTower>(singlePurposeTower, 10, container);
        circleRangeTowerPool = new PoolMono<CircleRangeTower>(circleRangeTower, 10, container);
        splashTowerPool = new PoolMono<SplashTower>(splashTower, 10, container);
    }

    public Tower CreateTower(TowerType towerType)
    {
        switch (towerType)
        {
            case TowerType.SinglePurpose:
                return singlePurposeTowerPool.GetFreeElement();
            case TowerType.CircleRange:
                return circleRangeTowerPool.GetFreeElement();
            case TowerType.Splash:
                return splashTowerPool.GetFreeElement();
        }

        throw new NullReferenceException("No such type of tower");
    }
}
