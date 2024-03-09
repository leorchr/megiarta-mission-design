using TMPro;
using UnityEngine;

public class MoneyUI : MonoBehaviour
{
    public static MoneyUI Instance;
    [SerializeField] private TextMeshProUGUI moneyText;
    void Awake()
    {
        if (Instance) Destroy(gameObject);
        else Instance = this;
    }

    public void UpdateMoney()
    {
        moneyText.text = Wallet.Instance.GetMoney().ToString();
    }
}