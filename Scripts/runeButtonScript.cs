using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class runeButtonScript : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public Button button;
    private Vector3 originalScale;


    void Start()
    {
        originalScale = transform.localScale;
    }
    public void OnPointerEnter(PointerEventData eventData)
    {

        transform.localScale = transform.localScale * 1.3f;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        transform.localScale = originalScale;


    }
}
