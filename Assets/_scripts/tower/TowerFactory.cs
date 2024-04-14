using UnityEngine;

public class TowerFactory : MonoBehaviour
{
    [SerializeField] private Transform _parentTransform;

    [SerializeField] private GameObject _testTowerPrefab;

    public static TowerFactory instance { get; private set; }

    private void Awake()
    {
        instance = this;
    }

    public Tower CreateTower(string towerType, Vector2 spawnPoint)
    {
        GameObject createdTower = null;

        switch (towerType)
        {
            case "test":
                createdTower = Instantiate(_testTowerPrefab, spawnPoint, Quaternion.identity);
                break;
        }

        Tower tower = createdTower.GetComponent<Tower>();

        tower.transform.parent = _parentTransform;

        tower.GetComponent<RectTransform>().anchoredPosition = spawnPoint;

        //createdTower.transform.localScale = Vector3.one;

        tower.Initialize();

        return tower;
    }
}
