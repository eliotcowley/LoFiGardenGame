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

    public GameObject Phone;
    public Dictionary<string, int> Items;
    public GameGrid GameGrid;

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
        Items = new Dictionary<string, int>();
        Items.Add("Test", 99);
        Items.Add("Foo", 12);
        Items.Add("Bar", 34);
        Items.Add("Abc", 56);
        Items.Add("Def", 78);
        Items.Add("Ghi", 90);
        Items.Add("Jkl", 99);
        Items.Add("Mno", 99);
        Items.Add("Pqr", 99);
        Items.Add("Stu", 99);
    }

    private void Update()
    {
        if (Input.GetButtonDown(Constants.Input_Menu) && (!Phone.activeSelf))
        {
            TogglePause();
        }
    }

    public void TogglePause()
    {
        Phone.SetActive(!Phone.activeSelf);
        IsPaused = Phone.activeSelf;
        blurEffect.enabled = Phone.activeSelf;

        if (Phone.activeSelf)
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
