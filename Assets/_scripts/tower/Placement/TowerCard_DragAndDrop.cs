using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class TowerCard_DragAndDrop : MonoBehaviour, IBeginDragHandler, IEndDragHandler, IDragHandler
{
    [SerializeField] private Canvas canvas;
    [SerializeField] private GameObject draggingIconPrefab;
    [SerializeField] private TowerType _type;
    private GameObject draggingIcon; // Полупрозрачная копия
    private RectTransform rectTransform;
    private CanvasGroup canvasGroup;
    private Vector2 initialPosition;
    private Vector2 _destroyPosition;
    public void Initialize(Canvas canvas, Vector2 destroyPosition)
    {
        this.canvas = canvas;
        _destroyPosition = destroyPosition;
    }

    private void Awake()
    {
        rectTransform = GetComponent<RectTransform>();
        canvasGroup = GetComponent<CanvasGroup>();
        initialPosition = rectTransform.anchoredPosition;
    }

    private void Update()
    {
        initialPosition = rectTransform.anchoredPosition;

        if (rectTransform.anchoredPosition.x >= _destroyPosition.x)
        {
            //MouseFollower.instance.Toggle(false);
            
            if (draggingIcon != null)
            {
                Destroy(draggingIcon);
            }
            Destroy(this.gameObject);
        }
    }

    public void OnBeginDrag(PointerEventData eventData)
    {

        // Добавляем компонент Image к новому объекту
        
        draggingIcon = Instantiate(draggingIconPrefab, transform.position, Quaternion.identity, canvas.transform);



        draggingIcon.GetComponent<Image>().sprite = this.gameObject.GetComponent<Image>().sprite;
        draggingIcon.transform.SetAsLastSibling();
        //MouseFollower.instance.SetData(this.gameObject.GetComponent<Image>().sprite);
        //MouseFollower.instance.gameObject.transform.position = this.gameObject.transform.position;
        //MouseFollower.instance.Toggle(true);

        canvasGroup.alpha = .6f;
        canvasGroup.blocksRaycasts = false;
    }

    public void OnDrag(PointerEventData eventData)
    {
        draggingIcon.GetComponent<RectTransform>().anchoredPosition += eventData.delta / canvas.scaleFactor;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        Destroy(draggingIcon);
        //MouseFollower.instance.Toggle(false);

        canvasGroup.alpha = 1f;
        canvasGroup.blocksRaycasts = true;
        rectTransform.anchoredPosition = initialPosition;
    }

    public TowerType GetTowerType()
    {
        return _type;
    }
}
