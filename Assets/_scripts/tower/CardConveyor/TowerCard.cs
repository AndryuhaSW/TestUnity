using System.Threading;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using UnityTask = System.Threading.Tasks.Task;

public class TowerCard : MonoBehaviour, IBeginDragHandler, IEndDragHandler, IDragHandler
{
    [SerializeField] private TowerType towerType;
    private Canvas canvas;
    private RectTransform rectTransform;
    private CanvasGroup canvasGroup;
    private Vector2 startPosition;
    private Vector2 endPosition;
    private float speed;
    private Vector2 initialPosition;

    //Идея хорошая, но вроде можно улучшить. Пока пусть будет так
    [SerializeField] private GameObject draggingIconPrefab;
    private GameObject draggingIcon; // Полупрозрачная копия

    protected CancellationTokenSource token;

    private void Awake()
    {
        rectTransform = GetComponent<RectTransform>();
        canvasGroup = GetComponent<CanvasGroup>();
    }

    public async void Initialize(Canvas canvas, Vector2 startPosition, Vector2 endPosition, float speed)
    {
        token = new CancellationTokenSource();
        this.canvas = canvas;
        this.startPosition = startPosition;
        this.endPosition = endPosition;
        this.speed = speed;

        Move();
    }

    private async UnityTask Move()
    {
        rectTransform.anchoredPosition = startPosition;
        while (endPosition.x - rectTransform.anchoredPosition.x > 0.1f)
        {
            rectTransform.position = Vector2.MoveTowards(rectTransform.position, endPosition, speed * Time.fixedDeltaTime);
            initialPosition = rectTransform.anchoredPosition;
            await UnityTask.Delay((int)(Time.fixedDeltaTime * 1000), token.Token);
        }
        gameObject.SetActive(false);
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        draggingIcon = Instantiate(draggingIconPrefab, transform.position, Quaternion.identity, canvas.transform);

        draggingIcon.GetComponent<Image>().sprite = this.gameObject.GetComponent<Image>().sprite;
        draggingIcon.transform.SetAsLastSibling();

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

        canvasGroup.alpha = 1f;
        canvasGroup.blocksRaycasts = true;
        rectTransform.anchoredPosition = initialPosition;
    }

    //Потом привязать к какому-то событию(прекращение всех волн)
    public void StopWorking()
    {
        token.Cancel();
        gameObject.SetActive(false);
    }

    public TowerType GetTowerType()
    {
        return towerType;
    }
}
