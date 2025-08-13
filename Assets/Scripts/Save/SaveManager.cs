using System;
using UnityEngine;

public class SaveManager : MonoBehaviour
{
    private JsonDataService dataService = new JsonDataService();

    private PlayerData playerData = new PlayerData();

    public Action<int> OnLoadMoney;
    public Action<bool> OnLoadPurchase;
    public Action<float> OnLoadHealth;
    public Action<bool[]> OnLoadQuestCompletion;
    public Action<int> OnLoadPeopleScanned;

    private void Awake()
    {
        playerData.QuestCompletion = new bool[5];

        for (int i = 0; i < playerData.QuestCompletion.Length; i++)
            playerData.QuestCompletion[i] = false;
    }

    private void Start()
    {
        LoadData();
    }

    private void SaveData()
    {
        dataService.SaveData("/save-data.json", playerData);
    }

    private void LoadData()
    {
        PlayerData data = dataService.LoadData<PlayerData>("/save-data.json");
        playerData = data;

        OnLoadMoney?.Invoke(data.Money);
        OnLoadPurchase?.Invoke(data.IsLampPurchased);
        OnLoadHealth?.Invoke(data.Health);
        OnLoadQuestCompletion?.Invoke(data.QuestCompletion);
        OnLoadPeopleScanned?.Invoke(data.PeopleScanned);
    }

    public void SetMoney(int money)
    {
        playerData.Money = money;
        SaveData();
    }

    public void PurchaseLamp()
    {
        playerData.IsLampPurchased = true;
        SaveData();
    }

    public void SetHealth(float health)
    {
        playerData.Health = health;
        SaveData();
    }

    public void SetQuestCompletion(int id)
    {
        playerData.QuestCompletion[id] = true;
        SaveData();
    }

    public void SetPeopleScanned(int number)
    {
        playerData.PeopleScanned = number;
        SaveData();
    }

    public void GameOver()
    {
        playerData.Money = 0;
        playerData.IsLampPurchased = false;
        playerData.Health = 1f;
        playerData.PeopleScanned = 0;

        for (int i = 0; i < playerData.QuestCompletion.Length; i++) 
            playerData.QuestCompletion[i] = false;

        SaveData();
    }
}
