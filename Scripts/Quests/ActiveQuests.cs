using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "SO", menuName = "ActiveQuestsSO")]
public class ActiveQuests : ScriptableObject
{
    public List<Quest> activeQuests = new List<Quest>();
    public Quest trackedQuest;
}
