using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MouseFollower : MonoBehaviour
{
    [SerializeField] private Image _image;

    public static MouseFollower instance;
    private void Awake()
    {
        instance = this;
        Toggle(false);
    }
    public void SetData(Sprite sprite)
    {
        _image.sprite = sprite;
    }

    public void Toggle(bool val)
    {
        gameObject.SetActive(val);
    }
}