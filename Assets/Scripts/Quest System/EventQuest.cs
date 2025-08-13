using Zenject;

public class EventQuest : Quest
{
    [Inject]
    private EventManager eventManager;

    private void OnEnable()
    {
        eventManager.OnEventEnd += ExecuteGoal;
    }

    private void OnDisable()
    {
        eventManager.OnEventEnd -= ExecuteGoal;
    }
}
