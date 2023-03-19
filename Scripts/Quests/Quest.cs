using System.Collections.Generic;

[System.Serializable]
public class Quest
{
    public bool isActive;
    public string title;
    public string description;
    public string task;
    public QuestLocation goalLocation;
    public QuestLocation turnInLocation;
    public int goldReward;
    public List<GlobalRune.Type> runeRewards;
    public string questVar;
    public bool autoComplete;
    public bool returned;

    public QuestGoal goal;

}
