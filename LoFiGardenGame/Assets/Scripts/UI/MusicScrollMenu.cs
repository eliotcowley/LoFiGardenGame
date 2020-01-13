using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System.Collections.Generic;
using TMPro;

public class MusicScrollMenu : MonoBehaviour
{
    public Button[] PlaybackButtons;

    [HideInInspector]
    public List<Button> SongButtons;

    [SerializeField]
    private GameObject buttonPrefab;

    [SerializeField]
    private Button backButton;

    [SerializeField]
    private PauseMenu pauseMenu;

    private RectTransform scrollRectTransform;
    private RectTransform contentPanel;
    private RectTransform selectedRectTransform;
    private GameObject lastSelected;
    private MusicController musicController;
    private float buttonHeight;

    private void Start()
    {
        scrollRectTransform = GetComponent<RectTransform>();
        contentPanel = GetComponent<ScrollRect>().content;
        contentPanel.GetComponent<RectTransform>().sizeDelta = new Vector2(contentPanel.rect.width, contentPanel.transform.childCount * buttonHeight);

        musicController = GameController.Instance.MusicController;
        buttonHeight = buttonPrefab.GetComponent<RectTransform>().rect.height;
        List<Button> SongButtons = new List<Button>();

        for (int i = 0; i < musicController.Songs.Length; i++)
        {
            GameObject button = Instantiate(buttonPrefab, contentPanel.transform);
            button.GetComponent<RectTransform>().localPosition = new Vector3(0f, i * -buttonHeight);
            SongButtons.Add(button.GetComponent<Button>());
        }

        for (int i = 0; i < SongButtons.Count; i++)
        {
            Navigation navigation = new Navigation();
            navigation.mode = Navigation.Mode.Explicit;

            if (i > 0)
            {
                navigation.selectOnUp = SongButtons[i - 1];
            }
            
            if (i < (SongButtons.Count - 1))
            {
                navigation.selectOnDown = SongButtons[i + 1];
            }

            if (i == 0)
            {
                navigation.selectOnUp = PlaybackButtons[1];
            }

            SongButtons[i].navigation = navigation;
            SongButtons[i].GetComponentInChildren<TextMeshProUGUI>().SetText(musicController.Songs[i].Name);
            SongButton songButton = SongButtons[i].GetComponent<SongButton>();
            songButton.Index = i;
            pauseMenu.SongButtonImages.Add(songButton.PlayingImage);
        }

        for (int i = 0; i < PlaybackButtons.Length; i++)
        {
            Navigation navigation = new Navigation();
            navigation.mode = Navigation.Mode.Explicit;
            navigation.selectOnDown = SongButtons[0];
            navigation.selectOnUp = backButton;

            if (i < (PlaybackButtons.Length - 1))
            {
                navigation.selectOnRight = PlaybackButtons[i + 1];
            }

            if (i > 0)
            {
                navigation.selectOnLeft = PlaybackButtons[i - 1];
            }

            PlaybackButtons[i].navigation = navigation;
        }
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