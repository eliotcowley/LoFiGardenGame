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
        if (Input.GetButtonDown(Constants.Input_Submit))
        {
            if (interaction != null)
            {
                interaction.Interact();
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag(Constants.Tag_Interactable))
        {
            buttonPrompt.SetActive(true);
            interaction = other.gameObject.GetComponent<RadioInteraction>();
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
}
