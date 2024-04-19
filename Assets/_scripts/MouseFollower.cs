using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MouseFollower : MonoBehaviour
{
    [SerializeField] private Canvas _canvas;
    [SerializeField] private Image _image;

    public static MouseFollower instance;
    private void Awake()
    {
        instance = this;
    }
    public void SetData(Sprite sprite)
    {
        _image.sprite = sprite;
    }
    void Update()
    {
        Vector2 position;
        RectTransformUtility.ScreenPointToLocalPointInRectangle(
            (RectTransform)_canvas.transform,
            Input.mousePosition,
            _canvas.worldCamera,
            out position
                );
        transform.position = _canvas.transform.TransformPoint(position);
    }

    public void Toggle(bool val)
    {
        gameObject.SetActive(val);
    }
}