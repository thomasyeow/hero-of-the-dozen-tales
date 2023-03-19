[System.Serializable]
public class QuestGoal
{
    public GoalType goalType;
    public KillType killType;
    public GatherType gatherType;

    public bool playerInLocation = true;

    public int requiredAmount;
    public int currentAmount;

    public bool isReached()
    {
        return currentAmount >= requiredAmount;
    }
    public void EnemyKilled(KillType kt)
    {
        if (!isReached())
        {
            if (goalType == GoalType.Kill && killType == kt && playerInLocation)
            {
                currentAmount++;
            }
        }
    }
    public void ItemCollected(GatherType gt)
    {
        if (!isReached())
        {
            if (goalType == GoalType.Gathering && gatherType == gt)
            {
                currentAmount++;
            }
        }
    }
}

public enum GoalType
{
    Kill,
    Gathering
}

public enum KillType
{
    Skeleton,
    RuneColossus,
    Bandit
}

public enum GatherType
{
    Wood,
    Stone,
    Metal
}
