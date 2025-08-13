using Zenject;

public class MoneyQuest : Quest
{
    [Inject]
    private MoneyManager moneyManager;

    [Inject]
    private SaveManager saveManager;

    private void OnEnable()
    {
        moneyManager.OnMoneyChange += Execute;
        saveManager.OnLoadMoney += LoadMoney;
    }

    private void OnDisable()
    {
        moneyManager.OnMoneyChange -= Execute;
        saveManager.OnLoadMoney -= LoadMoney;
    }

    private void LoadMoney(int value)
    {
        goal.Execute(value);
    }

    private void Execute(int value)
    {
        if (goal.Execute(value))
            questManager.CompleteQuest(goal.Id);
    }
}
