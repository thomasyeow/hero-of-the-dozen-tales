using UnityEngine;
using UnityEngine.EventSystems;

public class InventoryToolInfo : MonoBehaviour//, IPointerEnterHandler, IPointerExitHandler
{
    void Update()
    {
        if (EventSystem.current.IsPointerOverGameObject())
        {
            Debug.Log("Its over UI elements");
        }
        else
        {
            Debug.Log("Its NOT over UI elements");
        }
    }

}