﻿using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using TMPro;

public class PauseMenu : MonoBehaviour
{
    [HideInInspector]
    public List<Image> SongButtonImages;

    [SerializeField]
    private GameObject pauseMenu;

    [SerializeField]
    private GameObject musicMenu;

    [SerializeField]
    private GameObject statsMenu;

    [SerializeField]
    private GameObject questMenu;

    [SerializeField]
    private GameObject settingsMenu;

    [SerializeField]
    private GameObject itemsMenu;

    [SerializeField]
    private Button mainResumeButton;

    [SerializeField]
    private Button musicSaveButton;

    [SerializeField]
    private Button statsBackButton;

    [SerializeField]
    private Button questBackButton;

    [SerializeField]
    private Button settingsBackButton;

    [SerializeField]
    private Button itemsBackButton;

    [SerializeField]
    private TextMeshProUGUI playPauseText;

    [SerializeField]
    private Image playPauseImage;

    [SerializeField]
    private Sprite playSprite;

    [SerializeField]
    private Sprite pauseSprite;

    [SerializeField]
    private TextMeshProUGUI currentSongText;

    private MusicController musicController;

    private void Start()
    {
        musicController = GameController.Instance.MusicController;
    }

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
            else if (settingsMenu.activeSelf)
            {
                ExitSettingsMenu();
            }
            else if (itemsMenu.activeSelf)
            {
                ExitItemsMenu();
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
        currentSongText.SetText(musicController.Songs[musicController.CurrentSongIndex].Name);
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

    public void OpenSettingsMenu()
    {
        DeselectOtherButtons();
        pauseMenu.SetActive(false);
        settingsMenu.SetActive(true);
        settingsBackButton.Select();
    }

    public void ExitSettingsMenu()
    {
        DeselectOtherButtons();
        settingsMenu.SetActive(false);
        pauseMenu.SetActive(true);
        mainResumeButton.Select();
    }

    public void OpenItemsMenu()
    {
        DeselectOtherButtons();
        pauseMenu.SetActive(false);
        itemsMenu.SetActive(true);
        itemsBackButton.Select();
    }

    public void ExitItemsMenu()
    {
        DeselectOtherButtons();
        itemsMenu.SetActive(false);
        pauseMenu.SetActive(true);
        mainResumeButton.Select();
    }

    public void PressPlayPause()
    {
        musicController.ToggleMusicPlaying();
        UpdatePlayPauseGroup();
        SongButtonImages[musicController.CurrentSongIndex].sprite = musicController.AudioSource.isPlaying ? pauseSprite : playSprite;
    }

    public void PressNext()
    {
        musicController.NextSong();
        UpdatePlayPauseGroup();
    }

    public void PressPrevious()
    {
        musicController.PreviousSong();
        UpdatePlayPauseGroup();
    }

    public void UpdatePlayPauseGroup()
    {
        playPauseText.SetText(musicController.AudioSource.isPlaying ? Constants.String_Pause : Constants.String_Play);
        playPauseImage.sprite = musicController.AudioSource.isPlaying ? pauseSprite : playSprite;
        currentSongText.SetText(musicController.Songs[musicController.CurrentSongIndex].Name);
    }

    public void UpdateOldSongButton()
    {
        SongButtonImages[musicController.CurrentSongIndex].sprite = playSprite;
    }
}
