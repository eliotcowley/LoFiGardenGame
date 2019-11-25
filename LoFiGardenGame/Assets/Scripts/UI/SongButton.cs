using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class SongButton : EventTrigger
{
    [HideInInspector]
    public int Index;

    [HideInInspector]
    public Image PlayingImage;

    private GameObject buttonHighlight;
    private PauseMenu pauseMenu;
    private Button button;
    private Sprite pauseSprite;
    private Sprite playSprite;
    private MusicController musicController;

    private void Awake()
    {
        pauseMenu = GameController.Instance.Phone.GetComponent<PauseMenu>();
        button = GetComponent<Button>();
        buttonHighlight = UtilityFunctions.GetChildRecursively(transform, Constants.Tag_ButtonSelected);
        PlayingImage = UtilityFunctions.GetChildRecursively(transform, Constants.Tag_PlayingImage).GetComponent<Image>();
        pauseSprite = Resources.Load<Sprite>(Constants.Resource_Pause);
        playSprite = Resources.Load<Sprite>(Constants.Resource_Resume);
        musicController = GameController.Instance.MusicController;
    }

    public void PlaySong()
    {
        if (musicController.CurrentSongIndex != Index)
        {
            pauseMenu.UpdateOldSongButton();
            musicController.PlaySong(Index);
            pauseMenu.UpdatePlayPauseGroup();
            PlayingImage.sprite = pauseSprite;
        }
        else
        {
            musicController.ToggleMusicPlaying();
            pauseMenu.UpdatePlayPauseGroup();

            if (musicController.AudioSource.isPlaying)
            {
                PlayingImage.sprite = pauseSprite;
            }
            else
            {
                PlayingImage.sprite = playSprite;
            }
        }
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
