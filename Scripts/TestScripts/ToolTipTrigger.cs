using System.Collections;
using UnityEngine;
using UnityEngine.EventSystems;

public class ToolTipTrigger : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    //the actual class that is used to create a tooltip; e.g. we add this script to the backpack object to create a tooltip to the backpack
    private Coroutine coroutine = null;
    [Multiline()]
    public string content;
    public string header;
    public void OnPointerEnter(PointerEventData eventData)
    {
        coroutine = StartCoroutine(timerCoroutine());       //Starts the coroutine (to show tooltip)   
    }
    public void OnPointerExit(PointerEventData eventData)
    {
        StopCoroutine(coroutine);                            //stop coroutine (so it doesnt show tooltip if mouse doesnt hover)
        ToolTipSystem.Hide();                                //hide tooltip if it is shown
    }

    //OnMouseEnter/Exit is the same as OnPointerEnter/Exit, the difference is that pointer is for UI, while mouse is for gameobjects
    public void OnMouseEnter()
    {
        coroutine = StartCoroutine(timerCoroutine());
    }
    public void OnMouseExit()
    {
        StopCoroutine(coroutine);
        ToolTipSystem.Hide();
    }


    IEnumerator timerCoroutine()            //after 1 second, the coroutine shows the tooltip
    {
        yield return new WaitForSecondsRealtime(1f);
        ToolTipSystem.Show(content, header);
    }

}
