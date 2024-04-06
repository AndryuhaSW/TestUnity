using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoneyChanger : MonoBehaviour
{
    [SerializeField] private Wallet _wallet;
    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.R))
        {
            _wallet.ChangeMoney(20f);
        }
        else if (Input.GetKeyDown(KeyCode.F))
        {
            _wallet.ChangeMoney(-6f);
        }
    }
}
