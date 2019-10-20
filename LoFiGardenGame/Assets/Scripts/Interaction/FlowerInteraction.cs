using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(ParticleSystem))]
public class FlowerInteraction : Interaction
{
    private bool watered = false;
    private ParticleSystem ps;

    private void Start()
    {
        ps = GetComponent<ParticleSystem>();
    }

    public override void Interact()
    {
        if (!watered)
        {
            watered = true;
            CanInteract = false;
            ps.Play();
        }
    }
}
