using TMPro;
using UnityEngine;
using UnityEngine.UI;

[ExecuteInEditMode()]
public class ToolTip : MonoBehaviour
{
    public TextMeshProUGUI headerField;                 //header of tooltip
    public TextMeshProUGUI contentField;                //text of tooltip
    public LayoutElement layoutElement;                 //used for resizing the tooltip
    public int characterWrapLimit;                      //how many character before we wrap our tooltip to another line

    public RectTransform rectTransform;

    private void Awake()
    {
        rectTransform = GetComponent<RectTransform>();
    }

    public void SetText(string content, string header = "")
    {
        if (string.IsNullOrEmpty(header))                   // if header is null, remove the header object
            headerField.gameObject.SetActive(false);
        else
        {
            headerField.gameObject.SetActive(true);         //set header to the one we want
            headerField.text = header;
        }
        contentField.text = content;                        //set content to the one we want

        int headerLength = headerField.text.Length;
        int contentLength = contentField.text.Length;
        layoutElement.enabled = (headerLength > characterWrapLimit || contentLength > characterWrapLimit) ? true : false;
    }

    private void Update()
    {
        if (Application.isEditor)                //so that you can see the tool tip change size in the inspector/editor
        {
            int headerLength = headerField.text.Length;
            int contentLength = contentField.text.Length;
            layoutElement.enabled = (headerLength > characterWrapLimit || contentLength > characterWrapLimit) ? true : false;
        }
        //here we move the tooltip based on where it is on the screen
        Vector2 position = Input.mousePosition;
        transform.position = position;

        float pivotX = position.x / Screen.width;
        float pivotY = position.y / Screen.height;

        rectTransform.pivot = new Vector2(pivotX, pivotY);
        transform.position = position;
    }

}
