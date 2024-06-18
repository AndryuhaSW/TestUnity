using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;

public class TowerSlot : MonoBehaviour, IDropHandler, IPointerClickHandler
{
    private TowerFactory towerFactory;
    private bool isEmployed = false;
    private Tower towerInSlot;

    private void Start()
    {
        towerFactory = TowerFactory.instance;
    }

    public void OnDrop(PointerEventData eventData)
    {
        if (eventData.pointerDrag != null && !isEmployed)
        {
            TowerCard towerCard = eventData.pointerDrag.GetComponent<TowerCard>();
            
            /*if (Wallet.Instance.GetMoney() >= towerCard.GetPrice() && towerCard.GetIsMouseFollowerActive())
            {
                isEmployed = true;
                Wallet.Instance.ChangeMoney(-towerCard.GetPrice());
                TowerType type = towerCard.GetTowerType();
                towerCard.gameObject.SetActive(false);

                Tower tower = towerFactory.CreateTower(type);
                towerInSlot = tower;
                tower.GetComponent<RectTransform>().anchoredPosition = GetComponent<RectTransform>().anchoredPosition;
                tower.Initialize();
            }*/
            towerCard.OnEndDrag(eventData);
        }
            
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        
        if (eventData.button == PointerEventData.InputButton.Right && towerInSlot != null && isEmployed == true)
        {
            towerInSlot.SaleTower();
            isEmployed = false;
            towerInSlot = null;
        }
    }
}
