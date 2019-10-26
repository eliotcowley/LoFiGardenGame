using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Rendering.PostProcessing;
using UnityEngine.UI;

public class GameController : Singleton<GameController>
{
    [HideInInspector]
    public bool IsPaused = false;

    [HideInInspector]
    public MusicController MusicController;

    [SerializeField]
    private GameObject phone;

    [SerializeField]
    private Button resumeButton;

    private PostProcessVolume blurEffect;

    private void Awake()
    {
        FindOrCreateMusicController();
    }

    private void Start()
    {
        blurEffect = Camera.main.GetComponent<PostProcessVolume>();
    }

    private void Update()
    {
        if (Input.GetButtonDown(Constants.Input_Menu))
        {
            TogglePause();
        }
    }

    public void TogglePause()
    {
        phone.SetActive(!phone.activeSelf);
        IsPaused = phone.activeSelf;
        blurEffect.enabled = phone.activeSelf;

        if (phone.activeSelf)
        {
            EventSystem.current.SetSelectedGameObject(null);
            resumeButton.Select();
        }
    }

    public void Quit()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
        Application.Quit();
#endif
    }

    private void FindOrCreateMusicController()
    {
        if (MusicController == null)
        {
            GameObject musicControllerObject = GameObject.FindGameObjectWithTag(Constants.Tag_MusicController);

            if (musicControllerObject == null)
            {
                GameObject musicControllerPrefab = Resources.Load<GameObject>(Constants.Prefab_MusicController);
                musicControllerObject = Instantiate(musicControllerPrefab, gameObject.transform);
                MusicController = musicControllerObject.GetComponent<MusicController>();
            }
        }
    }
}
