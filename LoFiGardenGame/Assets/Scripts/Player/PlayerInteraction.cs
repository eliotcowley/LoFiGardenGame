using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInteraction : MonoBehaviour
{
    [SerializeField]
    private GameObject buttonPrompt;

    private Interaction interaction;

    private void Update()
    {
        if (!GameController.Instance.IsPaused)
        {
            if (Input.GetButtonDown(Constants.Input_Submit))
            {
                if ((interaction != null) && interaction.CanInteract)
                {
                    interaction.Interact();
                }
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag(Constants.Tag_Interactable))
        {
            buttonPrompt.SetActive(true);
            interaction = other.gameObject.GetComponent<Interaction>();
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag(Constants.Tag_Interactable))
        {
            buttonPrompt.SetActive(false);
            interaction = null;
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag(Constants.Tag_Interactable))
        {
            if ((interaction != null) && !interaction.CanInteract)
            {
                buttonPrompt.SetActive(false);
            }
        }
    }
}
