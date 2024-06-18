using UnityEngine;
using UnityEngine.UI;
using Zenject;

public class WalletView : MonoBehaviour
{
    [SerializeField] private Text _text;

    private Wallet wallet;


    [Inject]
    public void Inject(Wallet wallet)
    {
        this.wallet = wallet;
    }

    private void OnEnable()
    {
        wallet.MoneyChanged += DisplayMoney;
        DisplayMoney();
    }

    private void OnDisable()
    {
        wallet.MoneyChanged -= DisplayMoney;
    }

    private void DisplayMoney()
    {
        _text.text = wallet.GetMoney().ToString();
    }
}
