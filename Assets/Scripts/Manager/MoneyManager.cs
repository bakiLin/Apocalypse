using System;
using TMPro;
using UnityEngine;
using Zenject;

public class MoneyManager : MonoBehaviour
{
    [Inject]
    private SaveManager saveManager;

    [SerializeField]
    private TextMeshProUGUI moneyText;

    [SerializeField]
    private int correctAnswerMoney;

    public int Money { get; private set; }

    public Action<int> OnMoneyChange;

    private void OnEnable()
    {
        saveManager.OnLoadMoney += LoadMoney;
    }

    private void OnDisable()
    {
        saveManager.OnLoadMoney -= LoadMoney;
    }

    private void LoadMoney(int money)
    {
        Money = money;
        moneyText.text = $"${Money}";
    }

    public void CorrectAnswer()
    {
        AddMoney(correctAnswerMoney);
    }

    public void AddMoney(int value)
    {
        Money += value;
        moneyText.text = $"${Money}";
        saveManager.SetMoney(Money);
        OnMoneyChange?.Invoke(Money);
    }
}
