using UnityEngine;
using UnityEngine.EventSystems;

public class ItemSlotDropScript : MonoBehaviour, IDropHandler
{
    //  private bool taken = false;
    public void OnDrop(PointerEventData eventData)
    {
        if (eventData.pointerDrag != null)
        {
            //   taken = true;
            gameObject.SetActive(false);
            Debug.Log("pulled");
            //     eventData.pointerDrag.GetComponent<DragDropInvScript>().isUsed = true;
            eventData.pointerDrag.GetComponent<Transform>().position = GetComponent<Transform>().position;
        }
    }
}
