using UnityEngine;

public class LevelBackground : MonoBehaviour
{
    [SerializeField] private GameObject bg_level_1;
    [SerializeField] private GameObject bg_level_2;

    GameObject current_bg;

    public void Change(int level)
    {
        switch (level)
        {
            case 1:
                current_bg?.SetActive(false);
                bg_level_1.SetActive(true);
                current_bg = bg_level_1;
                break;
            case 2:
                current_bg?.SetActive(false);
                bg_level_2.SetActive(true);
                current_bg = bg_level_2;
                break;
        }
    }
}
