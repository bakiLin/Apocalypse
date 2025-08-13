[System.Serializable]
public class Goal
{
    public int Id;
    public bool Completed;
    public int CurrentAmount;
    public int RequiredAmount;

    public bool Execute(int value = 1)
    {
        if (!Completed)
        {
            CurrentAmount += value;

            if (CurrentAmount >= RequiredAmount)
            {
                Completed = true;
                return true;
            }
        }

        return false;
    }
}
