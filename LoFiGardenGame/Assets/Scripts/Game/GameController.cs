using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    public static GameController Instance;

    [HideInInspector]
    public bool IsPaused = false;

    [SerializeField]
    private GameObject phone;

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
    }
}
