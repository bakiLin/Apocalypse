using Zenject;

public class PurchaseQuest : Quest
{
    [Inject]
    private LampManager lampManager;

    private void OnEnable()
    {
        lampManager.OnPurchaseLamp += ExecuteGoal;
    }

    private void OnDisable()
    {
        lampManager.OnPurchaseLamp -= ExecuteGoal;
    }
}
