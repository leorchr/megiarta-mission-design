using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wallet : MonoBehaviour
{
    public static Wallet Instance;
    [SerializeField] private int money;
    void Awake()
    {
        if (Instance)
        {
            Destroy(this);
        }
        else
        {
            Instance = this;
        }
    }

    private void Start()
    {
        if (MoneyUI.Instance) MoneyUI.Instance.UpdateMoney();
        else Debug.LogWarning("Pas d'UI pour l'argent");
    }


    public int GetMoney()
    {
        return money;
    }

    public void EarnMoney(int amount)
    {
        money += amount;
        if (MoneyUI.Instance) MoneyUI.Instance.UpdateMoney();
        else Debug.LogWarning("Pas d'UI pour l'argent");
    }
}
