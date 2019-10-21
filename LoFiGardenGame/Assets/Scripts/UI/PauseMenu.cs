using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class PauseMenu : MonoBehaviour
{
    public void DeselectOtherButtons()
    {
        EventSystem.current.SetSelectedGameObject(null);
    }
}
