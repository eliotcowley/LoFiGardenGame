using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class PauseMenu : MonoBehaviour
{
    [SerializeField]
    private GameObject pauseMenu;

    [SerializeField]
    private GameObject musicMenu;

    [SerializeField]
    private Button saveButton;

    [SerializeField]
    private Button resumeButton;

    public void DeselectOtherButtons()
    {
        EventSystem.current.SetSelectedGameObject(null);
    }

    public void OpenMusicMenu()
    {
        DeselectOtherButtons();
        pauseMenu.SetActive(false);
        musicMenu.SetActive(true);
        saveButton.Select();
    }

    public void ExitMusicMenu()
    {
        musicMenu.SetActive(false);
        pauseMenu.SetActive(true);
        DeselectOtherButtons();
        resumeButton.Select();
    }
}
