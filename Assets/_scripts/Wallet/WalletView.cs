using TMPro;
using UnityEngine;

public class WalletView : MonoBehaviour
{
    [SerializeField] private Wallet _wallet;
    [SerializeField] private TextMeshProUGUI _text;

    private void OnEnable()
    {
        _wallet.MoneyChanged += DisplayMoney;
        DisplayMoney();
    }

    private void OnDisable()
    {
        _wallet.MoneyChanged -= DisplayMoney;
    }

    private void DisplayMoney()
    {
        _text.text = _wallet.GetMoney().ToString();
    }
}
