using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using TMPro;
using UnityEngine;

public class MoneyManager : MonoBehaviour
{
    public static MoneyManager Instance;
    private int balance = 5000;
    [SerializeField] private Sprite moneyIcon;
    [SerializeField] private TextMeshProUGUI moneyText;

    private void Awake()
    {
        moneyText.text = balance.ToString();
        if (Instance != null && Instance != this)
            Destroy(gameObject);
        else
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
    }

    public void AddMoney(int money)
    {
        ShowItemManager.Instance.ShowTakeItem(moneyIcon, "Δενόγθ", money);
        balance += money;
        StartCoroutine(Counting(money, true));
    }

    public void TakeMoney(int money)
    {
        ShowItemManager.Instance.ShowGiveItem(moneyIcon, "Δενόγθ", money);
        balance -= money;
        StartCoroutine(Counting(money, false));
    }

    IEnumerator Counting(int coins, bool isIncrease)
    {
        for (; coins > 0; coins--)
        {
            if (isIncrease)
                moneyText.text = (int.Parse(moneyText.text) + 1).ToString();
            else
                moneyText.text = (int.Parse(moneyText.text) - 1).ToString();

            yield return new WaitForSeconds(0.001f);
        }
    }

    public int GetBalance()
    {
        return balance;
    }
}
