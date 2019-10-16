﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(Animator))]
public class PlayerController : MonoBehaviour
{
    public static PlayerController Instance;

    [SerializeField]
    private float moveSpeed = 10f;

    [SerializeField]
    private float turnSpeed = 10f;

    [SerializeField]
    private float moveSmooth = 10f;

    [SerializeField]
    private TextMeshProUGUI contextualText;

    [SerializeField]
    private GameObject buttonPrompt;

    private Rigidbody rb;
    private Animator animator;
    private RadioInteraction interaction;

    public void SetText(string text)
    {
        contextualText.SetText(text);
    }

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        animator = GetComponent<Animator>();
        contextualText.SetText("");

        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Debug.LogError("Instance of PlayerController already exists");
        }
    }

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

    private void FixedUpdate()
    {
        float horizontal = Input.GetAxis(Constants.Input_Horizontal) * Time.fixedDeltaTime * moveSpeed;
        float vertical = Input.GetAxis(Constants.Input_Vertical) * Time.fixedDeltaTime * moveSpeed;
        Vector3 moveVector = new Vector3(horizontal, 0f, vertical);
        Vector3 targetVector = new Vector3(horizontal, rb.velocity.y, vertical);
        rb.velocity = Vector3.Lerp(rb.velocity, targetVector, moveSmooth);

        if (moveVector.sqrMagnitude > 0.1f)
        {
            Quaternion targetRotation = Quaternion.LookRotation(moveVector, Vector3.up);
            rb.rotation = Quaternion.Lerp(rb.rotation, targetRotation, moveSmooth * turnSpeed * Time.fixedDeltaTime);
        }

        animator.SetFloat(Constants.Anim_Speed, moveVector.sqrMagnitude);
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
