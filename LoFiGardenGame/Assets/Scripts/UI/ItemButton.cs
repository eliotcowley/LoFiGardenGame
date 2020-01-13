using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ItemButton : EventTrigger
{
    private GameObject buttonHighlight;
    private PauseMenu pauseMenu;
    private Button button;

    private void Awake()
    {
        pauseMenu = GameController.Instance.Phone.GetComponent<PauseMenu>();
        button = GetComponent<Button>();
        buttonHighlight = UtilityFunctions.GetChildRecursively(transform, Constants.Tag_ButtonSelected);
    }

    public override void OnPointerEnter(PointerEventData eventData)
    {
        pauseMenu.DeselectOtherButtons();
        button.Select();
    }

    public override void OnPointerExit(PointerEventData eventData)
    {
        button.Select();
    }

    public override void OnSelect(BaseEventData eventData)
    {
        buttonHighlight.SetActive(true);
    }

    public override void OnDeselect(BaseEventData eventData)
    {
        buttonHighlight.SetActive(false);
    }
}
