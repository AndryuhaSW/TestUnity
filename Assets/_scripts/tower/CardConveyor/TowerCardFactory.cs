using System;
using UnityEngine;

public class TowerCardFactory : MonoBehaviour
{
    [SerializeField] private RectTransform container;

    [SerializeField] private TowerCard SinglePurposeTowerPrefab;
    [SerializeField] private TowerCard CircleRangeTowerPrefab;
    [SerializeField] private TowerCard SplashTowerPrefab;

    private PoolMono<TowerCard> SinglePurposeTowerPool;
    private PoolMono<TowerCard> CircleRangeTowerPool;
    private PoolMono<TowerCard> SplashTowerPool;

    public static TowerCardFactory instance { get; private set; }

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }

        SinglePurposeTowerPool = new PoolMono<TowerCard>(SinglePurposeTowerPrefab, 10, container);
        CircleRangeTowerPool = new PoolMono<TowerCard>(CircleRangeTowerPrefab, 10, container);
        SplashTowerPool = new PoolMono<TowerCard>(SplashTowerPrefab, 10, container);
    }

    public TowerCard SpawnTowerCard(TowerType towerType)
    {
        switch (towerType)
        {
            case TowerType.SinglePurpose:
                return SinglePurposeTowerPool.GetFreeElement();
            case TowerType.CircleRange:
                return CircleRangeTowerPool.GetFreeElement();
            case TowerType.Splash:
                return SplashTowerPool.GetFreeElement();
        }

        throw new NullReferenceException("type of enemy is null");
    }
}
