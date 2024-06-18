using UnityEngine;

public class LevelBackground : MonoBehaviour
{
    GameObject current_bg;

    public void Change(GameObject bg)
    {
        current_bg?.SetActive(false);
        bg.SetActive(true);
        current_bg = bg;
    }
}
