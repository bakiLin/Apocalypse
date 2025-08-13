using Cysharp.Threading.Tasks;
using System;
using UnityEngine;
using Zenject;

public class LampManager : MonoBehaviour
{
    [Inject]
    private MoneyManager moneyManager;

    [Inject]
    private HpManager hpManager;

    [Inject]
    private AudioManager audioManager;

    [Inject]
    private SaveManager saveManager;

    [SerializeField]
    private GameObject lamp, price;

    [SerializeField]
    private MeshRenderer lampLight;

    [SerializeField]
    private Material lighted, noLight;

    [SerializeField]
    private int lampCooldown;

    private int lampUseCount;

    public Action OnPurchaseLamp;

    private void OnEnable()
    {
        saveManager.OnLoadPurchase += LoadLampPurchase;
    }

    private void OnDisable()
    {
        saveManager.OnLoadPurchase -= LoadLampPurchase;
    }

    private void LoadLampPurchase(bool state)
    {
        if (state)
        {
            lamp.SetActive(true);
            price.SetActive(false);
        }
    }

    public void BuyLamp()
    {
        if (moneyManager.Money >= 200 && !lamp.activeSelf)
        {
            lamp.SetActive(true);
            price.SetActive(false);
            moneyManager.AddMoney(-200);
            saveManager.PurchaseLamp();
            OnPurchaseLamp?.Invoke();
        }
    }

    public void TurnOn()
    {
        if (lampUseCount < 3 && lamp.activeSelf)
        {
            lampUseCount++;

            if (hpManager.IsInfected) audioManager.Play("lamp bad");
            else audioManager.Play("lamp good");

            if (lampUseCount == 3) LampCooldownAsync();
        }
    }

    private async void LampCooldownAsync()
    {
        lampLight.material = noLight;
        await UniTask.Delay(lampCooldown * 1000);
        lampUseCount = 0;
        lampLight.material = lighted;
    }
}
