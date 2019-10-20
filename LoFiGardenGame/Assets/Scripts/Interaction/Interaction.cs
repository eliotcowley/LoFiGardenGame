using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Interaction : MonoBehaviour
{
    [HideInInspector]
    public bool CanInteract = true;

    public abstract void Interact();
}
