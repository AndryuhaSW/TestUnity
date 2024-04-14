using UnityEngine;
using UnityEngine.EventSystems;

public class TowerSlot : MonoBehaviour, IDropHandler
{
    private TowerFactory towerFactory;

    private void Awake()
    {
        towerFactory = TowerFactory.instance;
    }

    public void OnDrop(PointerEventData eventData)
    {
        if (eventData.pointerDrag != null)
        {
            eventData.pointerDrag.GetComponent<RectTransform>().anchoredPosition = GetComponent<RectTransform>().anchoredPosition;
            Destroy(eventData.pointerDrag);
            towerFactory.CreateTower("test", GetComponent<RectTransform>().anchoredPosition);
        }
            
    }
}
