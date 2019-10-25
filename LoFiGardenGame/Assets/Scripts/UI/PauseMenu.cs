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
    private GameObject statsMenu;

    [SerializeField]
    private GameObject questMenu;

    [SerializeField]
    private Button mainResumeButton;

    [SerializeField]
    private Button musicSaveButton;

    [SerializeField]
    private Button statsBackButton;

    [SerializeField]
    private Button questBackButton;

    private void Update()
    {
        if (Input.GetButtonDown(Constants.Input_Cancel))
        {
            if (pauseMenu.activeSelf)
            {
                GameController.Instance.TogglePause();
            }
            else if (musicMenu.activeSelf)
            {
                ExitMusicMenu();
            }
            else if (statsMenu.activeSelf)
            {
                ExitStatsMenu();
            }
            else if (questMenu.activeSelf)
            {
                ExitQuestMenu();
            }
        }
    }

    public void DeselectOtherButtons()
    {
        EventSystem.current.SetSelectedGameObject(null);
    }

    public void OpenMusicMenu()
    {
        DeselectOtherButtons();
        pauseMenu.SetActive(false);
        musicMenu.SetActive(true);
        musicSaveButton.Select();
    }

    public void ExitMusicMenu()
    {
        DeselectOtherButtons();
        musicMenu.SetActive(false);
        pauseMenu.SetActive(true);
        mainResumeButton.Select();
    }

    public void OpenStatsMenu()
    {
        DeselectOtherButtons();
        pauseMenu.SetActive(false);
        statsMenu.SetActive(true);
        statsBackButton.Select();
    }

    public void ExitStatsMenu()
    {
        DeselectOtherButtons();
        statsMenu.SetActive(false);
        pauseMenu.SetActive(true);
        mainResumeButton.Select();
    }

    public void OpenQuestMenu()
    {
        DeselectOtherButtons();
        pauseMenu.SetActive(false);
        questMenu.SetActive(true);
        questBackButton.Select();
    }

    public void ExitQuestMenu()
    {
        DeselectOtherButtons();
        questMenu.SetActive(false);
        pauseMenu.SetActive(true);
        mainResumeButton.Select();
    }
}
