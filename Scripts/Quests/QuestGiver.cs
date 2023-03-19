using UnityEngine;

public class QuestGiver : MonoBehaviour
{
    public string questTitle;
    public string questVar;
    public string questVarValue;
    private void OnTriggerEnter(Collider other)
    {
        foreach (Quest q in QuestManager.GetInstance().activeQuests)
        {
            if (q.title == questTitle)
            {
                //Debug.Log("almost works");
                if (q.goal.isReached())
                {
                  //  Debug.Log("works");
                    GameObject.Find("SetDialogueGlobals").GetComponent<SetGlobals>().ChangeVariable(questVar, questVarValue);
                }
            }
        }
    }
}
