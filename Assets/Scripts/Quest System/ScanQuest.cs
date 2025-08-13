using Zenject;

public class ScanQuest : Quest
{
    [Inject]
    private ChoiceManager choiceManager;

    [Inject]
    private SaveManager saveManager;

    private void OnEnable()
    {
        choiceManager.OnScan += ExecuteGoal;
        saveManager.OnLoadPeopleScanned += LoadPeopleScanned;
    }

    private void OnDisable()
    {
        choiceManager.OnScan -= ExecuteGoal;
        saveManager.OnLoadPeopleScanned -= LoadPeopleScanned;
    }

    private void LoadPeopleScanned(int value)
    {
        goal.Execute(value);
    }

    protected override void ExecuteGoal()
    {
        base.ExecuteGoal();
        saveManager.SetPeopleScanned(goal.CurrentAmount);
    }
}
