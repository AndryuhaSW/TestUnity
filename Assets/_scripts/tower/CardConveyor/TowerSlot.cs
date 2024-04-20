using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;

public class TowerSlot : MonoBehaviour, IDropHandler
{
    private TowerFactory towerFactory;
    private bool isEmployed = false;


    private void Start()
    {
        towerFactory = TowerFactory.instance;
    }

    public void OnDrop(PointerEventData eventData)
    {
        if (eventData.pointerDrag != null && !isEmployed)
        {
            isEmployed = true;
            TowerCard towerCard = eventData.pointerDrag.GetComponent<TowerCard>();
            towerCard.OnEndDrag(eventData);
            TowerType type = towerCard.GetTowerType();
            towerCard.gameObject.SetActive(false);
            
            Tower tower = towerFactory.CreateTower(type);
            tower.GetComponent<RectTransform>().anchoredPosition = GetComponent<RectTransform>().anchoredPosition;
            tower.Initialize();
        }
            
    }
}
