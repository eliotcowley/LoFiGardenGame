using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class GameController : MonoBehaviour
{
    public static GameController Instance;

    [HideInInspector]
    public bool IsPaused = false;

    [SerializeField]
    private GameObject phone;

    [SerializeField]
    private Button resumeButton;

    private void Start()
    {
        Instance = this;
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
}
