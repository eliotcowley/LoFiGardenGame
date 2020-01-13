using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System.Collections.Generic;
using TMPro;

public class ItemsScrollMenu : MonoBehaviour
{
    [HideInInspector]
    public List<Button> SongButtons;

    [SerializeField]
    private GameObject buttonPrefab;

    [SerializeField]
    private Button backButton;

    private RectTransform scrollRectTransform;
    private RectTransform contentPanel;
    private RectTransform selectedRectTransform;
    private GameObject lastSelected;
    private float buttonHeight;

    private void Start()
    {
        scrollRectTransform = GetComponent<RectTransform>();
        contentPanel = GetComponent<ScrollRect>().content;
        

        buttonHeight = buttonPrefab.GetComponent<RectTransform>().rect.height;
        List<Button> ItemButtons = new List<Button>();

        for (int i = 0; i < GameController.Instance.Items.Count; i++)
        {
            GameObject button = Instantiate(buttonPrefab, contentPanel.transform);
            button.GetComponent<RectTransform>().localPosition = new Vector3(0f, i * -buttonHeight);
            ItemButtons.Add(button.GetComponent<Button>());
        }

        List<string> items = new List<string>();
        foreach (var item in GameController.Instance.Items.Keys)
        {
            items.Add(item);
        }

        for (int i = 0; i < ItemButtons.Count; i++)
        {
            Navigation navigation = new Navigation();
            navigation.mode = Navigation.Mode.Explicit;

            if (i > 0)
            {
                navigation.selectOnUp = ItemButtons[i - 1];
            }
            
            if (i < (ItemButtons.Count - 1))
            {
                navigation.selectOnDown = ItemButtons[i + 1];
            }

            if (i == 0)
            {
                navigation.selectOnUp = backButton;
            }

            ItemButtons[i].navigation = navigation;
            UtilityFunctions.GetChildRecursively(ItemButtons[i].transform, Constants.Tag_ItemName).GetComponent<TextMeshProUGUI>().SetText(items[i]);
            UtilityFunctions.GetChildRecursively(ItemButtons[i].transform, Constants.Tag_ItemCount).GetComponent<TextMeshProUGUI>().SetText($"x{GameController.Instance.Items[items[i]].ToString()}");
        }

        Navigation backButtonNavigation = new Navigation();
        backButtonNavigation.mode = Navigation.Mode.Explicit;
        backButtonNavigation.selectOnDown = ItemButtons[0];
        backButton.navigation = backButtonNavigation;

        contentPanel.GetComponent<RectTransform>().sizeDelta = new Vector2(contentPanel.rect.width, contentPanel.transform.childCount * buttonHeight);
    }

    private void Update()
    {
        // Get the currently selected UI element from the event system.
        GameObject selected = EventSystem.current.currentSelectedGameObject;

        // Return if there are none.
        if (selected == null)
        {
            Debug.Log("Nothing selected");
            return;
        }
        // Return if the selected game object is not inside the scroll rect.
        if (selected.transform.parent != contentPanel.transform)
        {
            Debug.Log("Selected object not in content");
            return;
        }
        // Return if the selected game object is the same as it was last frame,
        // meaning we haven't moved.
        if (selected == lastSelected)
        {
            //Debug.Log("Same as before");
            return;
        }

        // Get the rect tranform for the selected game object.
        selectedRectTransform = selected.GetComponent<RectTransform>();
        // The position of the selected UI element is the absolute anchor position,
        // ie. the local position within the scroll rect + its height if we're
        // scrolling down. If we're scrolling up it's just the absolute anchor position.
        float selectedPositionY = Mathf.Abs(selectedRectTransform.anchoredPosition.y) + selectedRectTransform.rect.height;

        // The upper bound of the scroll view is the anchor position of the content we're scrolling.
        float scrollViewMinY = contentPanel.anchoredPosition.y;
        // The lower bound is the anchor position + the height of the scroll rect.
        float scrollViewMaxY = contentPanel.anchoredPosition.y + scrollRectTransform.rect.height;

        //Debug.Log($"selectedPositionY: {selectedPositionY}; scrollViewMaxY: {scrollViewMaxY}");
        Debug.Log($"Mathf.Abs(selectedRectTransform.anchoredPosition.y): {Mathf.Abs(selectedRectTransform.anchoredPosition.y)}");

        // If the selected position is below the current lower bound of the scroll view we scroll down.
        Debug.Log($"selectedPositionY: {selectedPositionY}");
        Debug.Log($"scrollViewMaxY: {scrollViewMaxY}");
        if (selectedPositionY > scrollViewMaxY)
        {
            float newY = selectedPositionY - scrollRectTransform.rect.height;
            contentPanel.anchoredPosition = new Vector2(contentPanel.anchoredPosition.x, newY);
        }
        // If the selected position is above the current upper bound of the scroll view we scroll up.
        else if (Mathf.Abs(selectedRectTransform.anchoredPosition.y) < scrollViewMinY)
        {
            contentPanel.anchoredPosition = new Vector2(contentPanel.anchoredPosition.x, Mathf.Abs(selectedRectTransform.anchoredPosition.y));
        }

        lastSelected = selected;
    }
}