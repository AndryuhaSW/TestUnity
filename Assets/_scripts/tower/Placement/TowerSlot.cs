using UnityEngine;
using UnityEngine.EventSystems;

public class TowerSlot : MonoBehaviour, IDropHandler
{
    private TowerFactory towerFactory;
    private bool isEmployed = false;

    private void Awake()
    {
        towerFactory = TowerFactory.instance;
    }

    public void OnDrop(PointerEventData eventData)
    {
        if (eventData.pointerDrag != null && !isEmployed)
        {
            isEmployed = true;
            eventData.pointerDrag.SetActive(false);

            Tower tower = towerFactory.CreateTower(TowerType.SinglePurpose);
            tower.Initialize();
            tower.GetComponent<RectTransform>().anchoredPosition = GetComponent<RectTransform>().anchoredPosition;
        }
            
    }
}
