using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

[RequireComponent(typeof(ScrollRect))]
public class ScrollWithKeyboard : MonoBehaviour
{
    [SerializeField]
    private Button defaultSelectedButton;

    private ScrollRect scrollRect;
    private float viewportHeight;
    private float viewPortMaxY;
    private float scrollRectHeight;
    private float scrollRectMaxY;
    private RectTransform scrollRectTransform;

    private void Start()
    {
        scrollRect = GetComponent<ScrollRect>();
        defaultSelectedButton.Select();
        viewportHeight = scrollRect.viewport.rect.height;
        viewPortMaxY = scrollRect.viewport.rect.yMax;
        scrollRectTransform = scrollRect.transform.parent.GetComponent<RectTransform>();
        scrollRectHeight = scrollRectTransform.rect.height;
        scrollRectMaxY = scrollRectTransform.rect.yMax;
    }

    private void Update()
    {
        //Debug.Log($"scrollRectHeight: {scrollRectHeight}; scrollRectMaxY: {scrollRectMaxY}");

        RectTransform selectedRect = EventSystem.current.currentSelectedGameObject.transform.parent.GetComponent<RectTransform>();
        //float selectedY = Mathf.Abs(selectedRect.anchoredPosition.y) + selectedRect.rect.height;
        float localY = selectedRect.localPosition.y;
        float selectedRectAnchorPositionY = Mathf.Abs(selectedRect.anchoredPosition.y);
        float selectedHeight = selectedRect.rect.height;

        float worldY = selectedRect.position.y;
        float scrollMaxY = scrollRectTransform.position.y - (scrollRectHeight / 4);
        //Debug.Log($"worldY: {worldY}; scrollRectY: {scrollRectTransform.position.y}; scrollMaxY: {scrollMaxY}");

        float selectedY = worldY - (selectedRect.rect.height / 2);

        //Debug.Log($"localY: {localY}");
        //Debug.Log($"selectedAnchorY: {selectedRectAnchorPositionY}; selectedHeight: {selectedHeight}");
        //Debug.Log($"selectedY: {selectedY}; scrollRectMaxY: {scrollRectMaxY}");

        Debug.Log($"selectedY: {selectedY}; scrollMaxY: {scrollMaxY}");

        if (selectedY < scrollMaxY)
        {
            float newY = Mathf.Abs(selectedY) - Mathf.Abs(scrollMaxY);
            //Debug.Log($"newY: {newY}");
            scrollRect.viewport.anchoredPosition = new Vector2(scrollRectTransform.anchoredPosition.x, newY);
            
        }
    }
}
