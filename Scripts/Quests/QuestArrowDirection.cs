using UnityEngine;

public class QuestArrowDirection : MonoBehaviour
{
    public Vector3 questLocation;
    public GameObject arrow;
    public float arrowRange;
    public bool tracking;

    private Vector3 direction;
    public GameObject questArea;
    private static QuestArrowDirection instance;
    private void Awake()
    {
        if (instance != null)
        {
            Debug.LogWarning("Found more than one QuestArrowDirection in the scene");
        }
        instance = this;
    }

    public static QuestArrowDirection GetInstance()
    {
        return instance;
    }
    void Update()
    {
        if (tracking)
        {
            if ((questLocation - transform.position).magnitude < arrowRange)
            {
                arrow.SetActive(false);
            }
            else if ((questLocation - transform.position).magnitude > arrowRange)
            {
                arrow.SetActive(true);
            }
            questLocation.y = transform.position.y;
            direction = (questLocation - transform.position).normalized;
            transform.rotation = Quaternion.LookRotation(direction);
        }
    }
    public void SetQuestLocation(QuestLocation location)
    {
        if (location.radius > 0)
        {
            questLocation.x = location.x;
            questLocation.z = location.z;
            arrow.SetActive(true);
            questArea.SetActive(true);
            questArea.transform.position = new Vector3(location.x, questArea.transform.position.y, location.z);
            questArea.transform.localScale = new Vector3(location.radius, location.radius, location.radius);
            tracking = true;
        }
        else
        {
            deactivateArea();
        }
    }
    public void deactivateArea()
    {
        arrow.SetActive(false);
        questArea.SetActive(false);
        tracking = false;
    }
}
