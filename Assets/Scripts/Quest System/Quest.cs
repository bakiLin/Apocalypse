using UnityEngine;
using Zenject;

public class Quest : MonoBehaviour
{
    [Inject]
    protected QuestManager questManager;

    [SerializeField]
    protected Goal goal;

    protected void Start()
    {
        if (goal.Completed)
            questManager.CompleteQuest(goal.Id);
    }

    protected virtual void ExecuteGoal()
    {
        if (goal.Execute())
            questManager.CompleteQuest(goal.Id);
    }
}